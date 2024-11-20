using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveCube : MonoBehaviour
{
    [Header("Cube Settings")]
    [SerializeField] float speed;
    [SerializeField] float lerptime;
    public Vector3 NowmoveCubePos;
    [SerializeField] Vector3 rot;
    public CubeStatus status;
    
    [Space(6)]
    [Header("Steps Settings")]
    public int NowstepCount;
    public int MaxStepCount;
    [SerializeField] int addStepCount;
    int saveStepCount;

    [Space(12)]
    [Header("Raycast Settings")]
    [SerializeField] float Hitdistance;
    public int Dir_Check;
    [SerializeField] bool up, down, right, left, forwrd, back;
    
    [Space(12)]
    [Header("Sphere Settings")]
    [SerializeField] float sphereRadius;

    [Space(12)]
    [Header("Mobile settings")]
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    CubeScr cubeScrScript;

    private void Start()
    {
        if (cubeScrScript == null) cubeScrScript = GameObject.FindAnyObjectByType<CubeScr>();
        MaxStepCount = cubeScrScript.activeCubeCount + addStepCount;
        saveStepCount = MaxStepCount;
        NowstepCount = MaxStepCount;

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }
    private void Update()
    {
        environmentSyncControl();

        mobileControl();
        pcControl();

        NowmoveCubePos = this.transform.position;
       
    }

    void mobileControl()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            moveCube(check_way(Vector3.right), Vector3.right);
                            Debug.Log("Right Swipe");
                        }
                        else
                        {   //Left swipe
                            moveCube(check_way(Vector3.left), Vector3.left);
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            moveCube(check_way(Vector3.up), Vector3.up);
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            moveCube(check_way(Vector3.down), Vector3.down);
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }



    void pcControl()
    {
        //Up
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveCube(check_way(Vector3.up), Vector3.up);
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveCube(check_way(Vector3.down), Vector3.down);
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveCube(check_way(Vector3.right), Vector3.right);
        }
        //left
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveCube(check_way(Vector3.left), Vector3.left);
        }
    }

    int check_way(Vector3 direction)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Hitdistance))
        {
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "1":
                        Debug.Log("Did Hit");
                        status = CubeStatus.CanMove;
                        return 1;
                       
                    case "2":
                        Debug.Log("Did Hit");
                        status = CubeStatus.CanMove;
                        return 2;
                       
                    case "3":
                        //Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                        status = CubeStatus.CanMove;
                        return 3;
                    default:           
                        return 4;

                }
            }

        }
        return 0;
    }

    public void environmentSyncControl()
    {
        // Initialize flags for each direction
        bool[] directionFlags = new bool[6];

        // Check each direction
        directionFlags[0] = check_way(Vector3.up) > 0;      // up
        directionFlags[1] = check_way(Vector3.down) > 0;    // down
        directionFlags[2] = check_way(Vector3.right) > 0;   // right
        directionFlags[3] = check_way(Vector3.left) > 0;    // left
        directionFlags[4] = check_way(Vector3.forward) > 0; // forward
        directionFlags[5] = check_way(Vector3.back) > 0;    // back

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
            status = CubeStatus.CantMoveNotfinish;
            Debug.LogError("NO WAY!");
        }
    }


    // Dictionary for rotation changes
    private Dictionary<Vector3, Vector3> rotationChanges = new Dictionary<Vector3, Vector3>
{
    { Vector3.right,    new Vector3(0f, -90f, 00f) },     // Rotate on x-axis upward
    { Vector3.left, new Vector3(0f, 90f, 0f) },  // Rotate on x-axis downward
    { Vector3.up, new Vector3(90f, 0f, 0f) },  // Rotate on y-axis right
    { Vector3.down, new Vector3(-90f, 0f, 0f) }   // Rotate on y-axis left
};

    void moveCube(int CheckDir, Vector3 dir)
    {
        // If CheckDir is 0 and dir exists in the dictionary
        if (CheckDir == 0 && rotationChanges.ContainsKey(dir))
        {
            // Apply rotation change
            rot += rotationChanges[dir];
            // Smooth rotation
            this.gameObject.transform.eulerAngles = Vector3.Lerp(transform.localEulerAngles, rot, lerptime);
        }
        else
        {
            
            switch (CheckDir)
            {
                case 1:
                    this.gameObject.transform.Translate(dir * speed);
                    NowstepCount--;
                    Debug.Log("1");
                    break;
                case 2:
                    this.gameObject.transform.Translate(dir * speed);
                    NowstepCount--;
                    Debug.Log("2");
                    break;
                case 3:
                    this.gameObject.transform.Translate(dir * speed);
                    NowstepCount--;
                    Debug.Log("3");
                    break;
                default:
                    Debug.Log("Incorrect Cube");
                    break;
            }   
        }
    }

    
}
