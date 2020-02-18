using UnityEngine;
using System.Collections.Generic;
public class Maze
{
    private int width, height;

    public readonly int cols, rows;
    public readonly int[,] grid;

    private readonly int x = 1, y = 0;
    public static readonly int wall = 0, empty = 1, visited = 2;

    private int initialX, initialY;
    private int[] curr, next;

    private Stack<int[]> availableNeighbors;

    public Maze(int width, int height, int initialX, int initialY, int seed)
    {
        this.width = width;
        this.height = height;
        this.initialX = initialX;
        this.initialY = initialY;

        Random.InitState(seed);
        availableNeighbors = new Stack<int[]>();

        cols = (2 * width) + 1;
        rows = (2 * height) + 1;

        grid = new int[rows, cols];

        Generate();
    }

    private void Generate()
    {
        if (width % 2 == 0 || height % 2 == 0) return;

        GridInit();

        SetInitialCell();

        curr = new int[] { initialY, initialX };
        next = curr;

        do
        {
            while (next[x] != -1)
            {
                grid[curr[y], curr[x]] = visited;
                SelectNeighbor();
                RemoveWall();

                curr = next;
            }

            curr = availableNeighbors.Pop();
            next = curr;
        }
        while (availableNeighbors.Count != 0);

        grid[0, width] = visited;
        grid[rows - 1, width] = visited;
    }

    private void GridInit()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i % 2 == 1 && j % 2 == 1)
                {
                    grid[i, j] = empty;
                }
                else
                {
                    grid[i, j] = wall;
                }
            }
        }
    }

    private void SetInitialCell()
    {
        if (initialX == -1)
        {
            initialX = width;
        }

        if (initialY == -1)
        {
            initialY = height;
        }
    }

    private void SelectNeighbor()
    {
        List<int[]> neighbors = new List<int[]>();

        // North of current (x, y-2)
        if (curr[y] - 2 >= 0 && grid[curr[y] - 2, curr[x]] == empty)
        {
            neighbors.Add(new int[] { curr[y] - 2, curr[x] });
        }

        // East of current (x+2, y)
        if (curr[x] + 2 < grid.GetLength(x) && grid[curr[y], curr[x] + 2] == empty)
        {
            neighbors.Add(new int[] { curr[y], curr[x] + 2 });
        }

        // South of current (x, y+2)
        if (curr[y] + 2 < grid.GetLength(y) && grid[curr[y] + 2, curr[x]] == empty)
        {
            neighbors.Add(new int[] { curr[y] + 2, curr[x] });
        }

        // West of current (x-2, y)
        if (curr[x] - 2 >= 0 && grid[curr[y], curr[x] - 2] == empty)
        {
            neighbors.Add(new int[] { curr[y], curr[x] - 2 });
        }

        if (neighbors.Count > 0)
        {
            next = neighbors[Random.Range(0, neighbors.Count)];

            if (neighbors.Count > 1)
            {
                availableNeighbors.Push(curr);
            }
        }
        else
        {
            next[x] = -1;
        }
    }

    private void RemoveWall()
    {
        if (next[x] != -1)
        {
            if (curr[x] == next[x])
            {
                int yIndex = (curr[y] + next[y]) / 2;
                grid[yIndex, curr[x]] = visited;
                return;
            }

            if (curr[y] == next[y])
            {
                int xIndex = (curr[x] + next[x]) / 2;
                grid[curr[y], xIndex] = visited;
                return;
            }
        }
    }
}
