using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject mazePrefab;
    public Transform anchor;

    [Space]
    public int width = 11;
    public int height = 7;

    public int initialX = -1, initialY = 1;

    public int seed = 10;

    [Space]
    public int positionOffset = 3;

    void Start()
    {
        Maze maze = new Maze(width, height, initialX, initialY, seed);

        for (int i = 0; i < maze.rows; i++)
        {
            for (int j = 0; j < maze.cols; j++)
            {
                if (maze.grid[i, j] == Maze.visited)
                {
                    Vector3 position = new Vector3(positionOffset * j, 0f, 
                                                   positionOffset * i);

                    Instantiate(mazePrefab, position + anchor.position, 
                                mazePrefab.transform.rotation, transform);
                }
            }
        }
    }
}
