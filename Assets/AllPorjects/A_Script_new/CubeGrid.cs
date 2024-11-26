using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour
{
    public HashSet<Vector3> CubePos = new HashSet<Vector3>();
    private Dictionary<string, List<GameObject>> colorCubes;

    public int Cubetotalcout;
    public int Now_Cubetotalcout;

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

    public void InitializeCubes()
    {
        AddCubesToCategory("Red", "1");
        AddCubesToCategory("Green", "2");
        AddCubesToCategory("Blue", "3");    

        Cubetotalcout = colorCubes["Red"].Count + colorCubes["Green"].Count + colorCubes["Blue"].Count;
        Now_Cubetotalcout = Cubetotalcout;


        Invoke("UpdateActiveCubes", FindAnyObjectByType<GameStatus>().loadingValueTime);

    }

    private void AddCubesToCategory(string category, string tag)
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag(tag);
        foreach (var cube in cubes)
        {
            colorCubes[category].Add(cube);

            cube.gameObject.name = tag;
            Vector3 position = cube.transform.position;
            CubePos.Add(position);
            switch (tag)
            {
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

   
}
