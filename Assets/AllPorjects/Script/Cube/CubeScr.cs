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
    [SerializeField] GameObject Crice;
    [SerializeField] float rotLerp;
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

    public void LeftChangeBTN()
    {
        SelectColorCount = Math.Clamp(SelectColorCount - 1, 1, 3);
        UpdateActiveCubes();
    }
    public void ChangeBTN()
    {
        SelectColorCount = SelectColorCount == 3 ? 1 : SelectColorCount + 1;
        UpdateActiveCubes();
    }
    public void RightChangeBTN()
    {
        SelectColorCount = Math.Clamp(SelectColorCount + 1, 1, 3);
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

    public void UpdateOutlinesRed()
    {
        SelectColorCount = 1;
        LeanTween.rotateZ(Crice, -60f, rotLerp)
                 .setEase(LeanTweenType.easeOutQuad);
        UpdateActiveCubes();
        UpdateOutlines();
    }

    public void UpdateOutlinesBlue() {
        SelectColorCount = 2;
        LeanTween.rotateZ(Crice, 60f, rotLerp)
                 .setEase(LeanTweenType.easeOutQuad);
        UpdateActiveCubes();
        UpdateOutlines();
    }

    public void UpdateOutlinesGreen() {
        SelectColorCount = 3;
        LeanTween.
            rotateZ(Crice, 
          180f, 
          rotLerp)
                .setEase(LeanTweenType.easeOutQuad);
        UpdateActiveCubes();
        UpdateOutlines();
    }

    private void UpdateOutlines()
    {
        RedImage.fillCenter = SelectColorCount == 1;
        BlueImage.fillCenter = SelectColorCount == 2;
        GreenImage.fillCenter = SelectColorCount == 3;
    }

    private void HandleColorInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cycle increment logic: If at max (3), loop back to 1
            SelectColorCount = SelectColorCount == 3 ? 1 : SelectColorCount + 1;
            UpdateActiveCubes();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Cycle decrement logic: If at min (1), loop back to 3
            SelectColorCount = SelectColorCount == 1 ? 3 : SelectColorCount - 1;
            UpdateActiveCubes();
        }
    }

    



}
