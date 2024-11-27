using GG.Infrastructure.Utils.Swipe;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections.Generic;
using UnityEngine;

public class CubeGridMove : MonoBehaviour
{
    public CubeGrid CubeGridScrpt;
    [Header("Cube Settings")]
    public GameObject CubePlayer;
    public Vector3 CubePlayerPos;
    [SerializeField] Vector3 rot;

    public float _SPEED_OnGridMove;
    [SerializeField] private float lerpTime;
    [SerializeField] private float Rotlerptime;

    [Header("Steps")]
    public int currentStepCount;

    [Header("Swipe Settings")]
    [SerializeField] private SwipeListener swipeListener;


    public bool up, down, right, left, forwrd, back;




    private void Start()
    {
        CubePlayerPos = CubePlayer.transform.position;
    }
    private void Update()
    {
        CubePlayer.transform.position = Vector3.Lerp(CubePlayer.transform.position, CubePlayerPos, lerpTime * Time.deltaTime);
        
        environmentSyncControl(CubePlayerPos);
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
            case "Forward": direction = Vector3.forward; break; // Change to back for 3D down
            case "Back": direction = Vector3.back; break; // Change to back for 3D down
        }
        MoveToDirection(direction);
    }


    // Dictionary for rotation changes
    private Dictionary<Vector3, Vector3> rotationChanges = new Dictionary<Vector3, Vector3>
{
    { Vector3.right,    new Vector3(0f, -90f, 00f) },     // Rotate on x-axis upward
    { Vector3.left, new Vector3(0f, 90f, 0f) },  // Rotate on x-axis downward
    { Vector3.up, new Vector3(90f, 0f, 0f) },  // Rotate on y-axis right
    { Vector3.down, new Vector3(-90f, 0f, 0f) }   // Rotate on y-axis left
};
    private void MoveToDirection(Vector3 direction)
    {
        
        
           
        
        Vector3 targetPosition = CubePlayerPos + (direction * _SPEED_OnGridMove);
        
        // Example boundary check: Ensure movement is within a certain range
        if (IsValidPosition(targetPosition))
        {
            CubePlayerPos = targetPosition;
            deleteCubeGrid(CubePlayerPos);
            currentStepCount--;
        }
        else if(!IsValidPosition(targetPosition) && rotationChanges.ContainsKey(direction))
        {
            // Apply rotation change
            rot += rotationChanges[direction];
            // Smooth rotation
            CubePlayer.gameObject.transform.eulerAngles = Vector3.Lerp(CubePlayer.transform.localEulerAngles, rot, Rotlerptime);
        }
    }

    private bool IsValidPosition(Vector3 targetPosition)
    {
        if (CubeGridScrpt.cubeDictionary.ContainsKey(targetPosition))
        {
            
            return true;
        }
        else { return false; }
        // Add any boundary or collision checks here if needed
        // Example: return targetPosition.x >= 0 && targetPosition.z >= 0;
         // Default to always valid
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    //void deleteCubeGrid(Vector3 playerPos)
    //{
    //    if (CubeGridScrpt.CubePos.Contains(playerPos))
    //    {
    //        CubeGridScrpt.CubePos.Remove(playerPos);
    //        Destroy(CubeGridScrpt.colorCubes)
    //    }

    //}
    void deleteCubeGrid(Vector3 playerPos)
    {
        if (CubeGridScrpt.cubeDictionary.ContainsKey(playerPos))
        {
            GameObject cubeToRemove = CubeGridScrpt.cubeDictionary[playerPos];


            CubeGridScrpt.cubeDictionary.Remove(playerPos);

            foreach (var category in CubeGridScrpt.colorCubes)
            {
                if (category.Value.Remove(cubeToRemove))  
                {
                    break;
                }
            }

            
            Destroy(cubeToRemove);
            Debug.Log($"Cube removed at position {playerPos}");
        }
        else
        {
            Debug.Log("No cube found at the specified position.");
        }
    }

    public void environmentSyncControl(Vector3 targetPosition)
    {
        // Initialize flags for each direction
        bool[] directionFlags = new bool[6];

        // Check each direction
        directionFlags[0] = IsValidPosition(Vector3.up + CubePlayerPos);      // up
        directionFlags[1] = IsValidPosition(Vector3.down + CubePlayerPos);    // down
        directionFlags[2] = IsValidPosition(Vector3.right + CubePlayerPos);   // right
        directionFlags[3] = IsValidPosition(Vector3.left + CubePlayerPos);    // left
        directionFlags[4] = IsValidPosition(Vector3.forward + CubePlayerPos); // forward
        directionFlags[5] = IsValidPosition(Vector3.back + CubePlayerPos);    // back

        // Assign the flags to the respective variables
        up = directionFlags[0];
        down = directionFlags[1];
        right = directionFlags[2];
        left = directionFlags[3];
        forwrd = directionFlags[4];
        back = directionFlags[5];

        // Check if there is any available path
        if (up || down || left || right || forwrd || back)
        {
            Debug.LogError("WAY!");
        }
        else
        {
            
            Debug.LogError("NO WAY!");
        }
    }
}
