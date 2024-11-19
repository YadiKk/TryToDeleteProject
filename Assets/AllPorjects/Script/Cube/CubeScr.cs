using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScr : MonoBehaviour
{
    [Header("Sphere Settings")]
    [SerializeField] float sphereRadius;


    [Header("Color Settings")]
    [Range(1, 3)] public int SelectColorCount = 2;
    [SerializeField] Image RedImage, BlueImage, GreenImage;

    [Header("Cube Settings")]
    private Dictionary<string, List<GameObject>> colorCubes;


    public int totalCubeCount;
    public int activeCubeCount;

    private void Awake()
    {
        colorCubes = new Dictionary<string, List<GameObject>>
        {
            { "Red", new List<GameObject>() },
            { "Green", new List<GameObject>() },
            { "Blue", new List<GameObject>() }
        };

        InitializeCubes();
    }

    private void Update()
    {
        HandleColorInput();
        TracksDeleteCube();
    }

    public void InitializeCubes()
    {
        AddCubesToCategory("Red", "1");
        AddCubesToCategory("Green", "2");
        AddCubesToCategory("Blue", "3");

        totalCubeCount = colorCubes["Red"].Count + colorCubes["Green"].Count + colorCubes["Blue"].Count;
        activeCubeCount = totalCubeCount;

        

        UpdateActiveCubes();
    }
   

    void TracksDeleteCube()
    {
        Vector3 origin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(origin, sphereRadius);
        foreach (Collider collider in colliders)
        {
            if (collider != null) {
                Destroy(collider.gameObject);
                activeCubeCount--;
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(origin, sphereRadius);

        Collider[] colliders = Physics.OverlapSphere(origin, sphereRadius);
        Gizmos.color = colliders.Length > 0 ? Color.red : Color.green;
        Gizmos.DrawWireSphere(origin, sphereRadius);
    }


    private void AddCubesToCategory(string category, string tag)
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag(tag);
        foreach (var cube in cubes)
        {
            colorCubes[category].Add(cube);
            cube.gameObject.name = tag;
            switch (tag) {
                case "1":
                    cube.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case "2":
                    cube.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case "3":
                    cube.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    break;

            }
            
            
        }
    }

    public void UpdateActiveCubes()
    {
        EnableCubesByCategory("Red", SelectColorCount == 1);
        EnableCubesByCategory("Green", SelectColorCount == 2);
        EnableCubesByCategory("Blue", SelectColorCount == 3);

        UpdateOutlines();
    }

    private void EnableCubesByCategory(string category, bool isActive)
    {
        foreach (var cube in colorCubes[category])
        {
            if (cube != null) cube.SetActive(isActive);
        }
    }

    private void UpdateOutlines()
    {
        RedImage.fillCenter = SelectColorCount == 1;
        GreenImage.fillCenter = SelectColorCount == 2;
        BlueImage.fillCenter = SelectColorCount == 3;
    }

    private void HandleColorInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectColorCount = Math.Clamp(SelectColorCount + 1, 1, 3);
            UpdateActiveCubes();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectColorCount = Math.Clamp(SelectColorCount - 1, 1, 3);
            UpdateActiveCubes();
        }
    }

  
}
