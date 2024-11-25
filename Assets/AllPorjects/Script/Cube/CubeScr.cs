using System.Collections.Generic;
using UnityEngine;

public class CubeScr : MonoBehaviour
{
    [Header("Sphere Settings")]
    [SerializeField] float sphereRadius = 1f; // Çevre kontrol yarıçapı

    private HashSet<Vector3> cubePositions = new HashSet<Vector3>(); // Küp pozisyonları
    private Vector3[] directions = new Vector3[]
    {
        Vector3.up,
        Vector3.down,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    public int totalCubeCount;
    public int activeCubeCount;

    public enum CubeStatus { CanMove, CantMoveNotFinish, LevelComplete }
    public CubeStatus status;


    [Header("Grid Settings")]
    public Vector3 gridPosition; // Küpün mevcut grid konumu
    public float gridSize = 1f; // Her adımın boyutu

    [Header("Boundaries")]
    public float minX = -5f;
    public float maxX = 5f;
    public float minZ = -5f;
    public float maxZ = 5f;

    // Yeni konumu sınırlarla kontrol et
    public bool IsValidPosition(Vector3 targetPosition)
    {
        return targetPosition.x >= minX && targetPosition.x <= maxX &&
               targetPosition.z >= minZ && targetPosition.z <= maxZ;
    }


    private void Start()
    {
        InitializeCubes();
    }

    void InitializeCubes()
    {
        // Tüm küpleri sahneden bul ve pozisyonlarını kaydet
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube"); // Küp tag'i kullandım, senin tag'lara göre düzenle
        foreach (var cube in cubes)
        {
            cubePositions.Add(cube.transform.position);
        }
        totalCubeCount = cubePositions.Count;
        activeCubeCount = totalCubeCount;
    }

    private void Update()
    {
        EnvironmentSyncControl();
    }

    public void EnvironmentSyncControl()
    {
        bool hasPath = false;

        // Player'ın etrafındaki 6 yönü kontrol et
        foreach (var dir in directions)
        {
            Vector3 targetPosition = transform.position + dir;
            if (cubePositions.Contains(targetPosition))
            {
                hasPath = true;
                break; // Bir yön bulunursa döngüden çık
            }
        }

        // Kalan küp kontrolü
        if (activeCubeCount == 0)
        {
            status = CubeStatus.LevelComplete;
            Debug.Log("LEVEL COMPLETE!");
            // Geri sayım veya level bitiş işlemleri burada başlatılabilir
        }
        else if (hasPath)
        {
            status = CubeStatus.CanMove;
            Debug.Log("WAY FOUND!");
        }
        else
        {
            status = CubeStatus.CantMoveNotFinish;
            Debug.Log("NO WAY!");
        }
    }

  
   

    // Küp pozisyonlarını görselleştirmek için
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(origin, sphereRadius);
    }
}
