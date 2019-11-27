using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject wallPrefab;
    public float tileRadius;
    
    public Transform mazeAnchor;
    public Transform endPointAnchor;
    
    public bool fillTileSpaces;

    [Space]
    public int width = 11;
    public int height = 7;

    public int initialX = -1, initialY = 1;

    public int seed = 10;

    [Space]
    public int positionOffset = 3;

    private void Start()
    {
        SetAnchorOffset();

        GenerateMaze();
    }

    private void GenerateMaze()
    {
        Maze maze = new Maze(width, height, initialX, initialY, seed);

        Vector3 position;

        for (int i = 0; i < maze.rows; i++)
        {
            for (int j = 0; j < maze.cols; j++)
            {
                position = new Vector3(positionOffset * j, 0f,
                                       positionOffset * i);

                if (maze.grid[i, j] == Maze.visited)
                {
                    InstantiateTile(tilePrefab, position);

                    if (fillTileSpaces)
                    {
                        if (j + 1 < maze.cols && maze.grid[i, j + 1] == Maze.visited)
                        {
                            position = new Vector3(positionOffset * j + 2 * tileRadius, 0f,
                                                   positionOffset * i);

                            InstantiateTile(tilePrefab, position);
                        }

                        if (i + 1 < maze.rows && maze.grid[i + 1, j] == Maze.visited)
                        {
                            position = new Vector3(positionOffset * j, 0f,
                                                   positionOffset * i + 2 * tileRadius);

                            InstantiateTile(tilePrefab, position);
                        }
                    }
                }
                else if (maze.grid[i, j] == Maze.wall)
                {
                    InstantiateTile(wallPrefab, position);
                }
            }
        }
    }

    private void InstantiateTile(GameObject prefab, Vector3 position)
    {
        if (prefab != null)
        {
            Instantiate(prefab, position + mazeAnchor.position,
            prefab.transform.rotation, transform);
        }
    }

    private void SetAnchorOffset()
    {
        Vector3 anchorPos;

        anchorPos = mazeAnchor.position;
        anchorPos.x = -(2 * width * positionOffset) / 2;
        mazeAnchor.position = anchorPos;

        anchorPos = endPointAnchor.position;
        anchorPos.z = (2 * height * positionOffset) + mazeAnchor.position.z + tileRadius;
        endPointAnchor.position = anchorPos;
    }
}
