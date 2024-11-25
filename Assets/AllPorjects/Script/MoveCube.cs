using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class MoveCube : MonoBehaviour
{
    [Header("Cube Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float lerpTime;
    public Vector3 currentCubePos;

    [Header("Grid Settings")]
    public Vector3 gridPosition; // Current position on the grid
    public float gridSize = 1f; // Size of each grid step

    [Header("Steps Settings")]
    public int currentStepCount;
    public int maxStepCount;

    [Header("Swipe Settings")]
    [SerializeField] private SwipeListener swipeListener;
    private CubeScr cubeScr; // CubeScr referansı

    private void Start()
    {
        currentCubePos = transform.position;
        gridPosition = currentCubePos;
        maxStepCount = 10; // Example step count, customize as needed
        currentStepCount = maxStepCount;

        // CubeScr component'ini al
        cubeScr = GetComponent<CubeScr>();
        currentCubePos = transform.position;
        gridPosition = currentCubePos;
    }
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, gridPosition, lerpTime * Time.deltaTime);
    }

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        if (currentStepCount <= 0) return; // Stop moving if steps are exhausted

        Vector3 direction = Vector3.zero;
        switch (swipe)
        {
            case "Right": direction = Vector3.right; break;
            case "Left": direction = Vector3.left; break;
            case "Up": direction = Vector3.up; break; // Change to forward for 3D up
            case "Down": direction = Vector3.down; break; // Change to back for 3D down
        }
        MoveToDirection(direction);
    }

    private void MoveToDirection(Vector3 direction)
    {
        Vector3 targetPosition = gridPosition + (direction * gridSize);

        // Example boundary check: Ensure movement is within a certain range
        if (IsValidPosition(targetPosition))
        {
            gridPosition = targetPosition;
            currentStepCount--;
        }
    }

    private bool IsValidPosition(Vector3 targetPosition)
    {
        // CubeScr sınıfındaki sınır kontrolünü kullan
        return cubeScr.IsValidPosition(targetPosition);
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
}
