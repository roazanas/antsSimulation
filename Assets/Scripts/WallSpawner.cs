using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public float wallThickness = 0.1f;

    private void Start()
    {
        CreateWalls();
    }

    private void CreateWalls()
    {
        Camera camera = Camera.main;
        float verticalSize = camera.orthographicSize * 2;
        float horizontalSize = verticalSize * camera.aspect;

        Vector3 topWallPosition = new(0, camera.orthographicSize - wallThickness / 2, 0);
        Vector3 bottomWallPosition = new(0, -camera.orthographicSize + wallThickness / 2, 0);
        Vector3 leftWallPosition = new(-horizontalSize / 2 + wallThickness / 2, 0, 0);
        Vector3 rightWallPosition = new(horizontalSize / 2 - wallThickness / 2, 0, 0);

        CreateWall(topWallPosition, new(horizontalSize, wallThickness, 1));
        CreateWall(bottomWallPosition, new(horizontalSize, wallThickness, 1));
        CreateWall(leftWallPosition, new(wallThickness, verticalSize, 1));
        CreateWall(rightWallPosition, new(wallThickness, verticalSize, 1));
    }

    private void CreateWall(Vector3 position, Vector3 size)
    {
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
        wall.transform.localScale = size;
        wall.transform.parent = transform;
        wall.layer = 7;
    }
}
