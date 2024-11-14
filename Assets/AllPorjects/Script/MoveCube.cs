using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveCube : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float Hitdistance;
    public Vector3 NowmoveCubePos;
    [SerializeField] Vector3 rot;
    [SerializeField] float lerptime;
    public int Dir_Check;

    private void Update()
    {
        //Vector3.forward-- right
        //Vector3.back -- Left
        //Vector3.Up -- Up
        //Vector3.Down -- Down

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
        NowmoveCubePos = this.transform.position;
       
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
                        Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                        return 1;
                       
                    case "2":
                        Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                        return 2;
                       
                    case "3":
                        Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                        return 3;
                    default:
                        return 4;

                }
            }

        }
        return 0;
    }

    // Dictionary for rotation changes
    private Dictionary<Vector3, Vector3> rotationChanges = new Dictionary<Vector3, Vector3>
{
    { Vector3.right, new Vector3(0f, -90f, 00f) },     // Rotate on x-axis upward
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
                    Debug.Log("1");
                    break;
                case 2:
                    this.gameObject.transform.Translate(dir * speed);
                    Debug.Log("2");
                    break;
                case 3:
                    this.gameObject.transform.Translate(dir * speed);
                    Debug.Log("3");
                    break;
                default:
                    Debug.Log("Incorrect Cube");
                    break;
            }
            
        }

       
       
    }

    

}
