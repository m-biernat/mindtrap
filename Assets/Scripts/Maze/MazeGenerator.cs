using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public float tileRadius;
    public Transform mazeAnchor;
    public Transform endPointAnchor;

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

        for (int i = 0; i < maze.rows; i++)
        {
            for (int j = 0; j < maze.cols; j++)
            {
                if (maze.grid[i, j] == Maze.visited)
                {
                    Vector3 position = new Vector3(positionOffset * j, 0f,
                                                   positionOffset * i);

                    Instantiate(tilePrefab, position + mazeAnchor.position,
                                tilePrefab.transform.rotation, transform);
                }
            }
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
