using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSpawner : MonoBehaviour
{
    public Vector3 limit;
    
    public GameObject prefab;
    public int numberOfObjects = 4;

    private List<Vector3> _usedPositions;
    public float minDistance = 1.0f;
    
    void Awake()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        _usedPositions = new List<Vector3>();

        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 prefabSize = prefab.GetComponent<Renderer>().bounds.size;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3? nullablePosition = GetUniquePosition(prefabSize);

            if (nullablePosition == null) return;

            Vector3 position = nullablePosition.Value;
            Instantiate(prefab, position, rotation);
        }
    }

    private Vector3? GetUniquePosition(Vector3 scale)
    {
        Vector3 position;
        int attempts = 0;
        
        do
        {
            position = transform.position + new Vector3(
                Random.Range(-limit.x / 2 + scale.x / 2, limit.x / 2 - scale.x / 2),
                Random.Range(-limit.y / 2 + scale.y, limit.y / 2 - scale.y / 2),
                Random.Range(-limit.z / 2 + scale.z / 2, limit.z / 2 - scale.z / 2)
            );

            attempts++;
            if (attempts >= 1000)
                return null;
        } while (IsTooClose(position));

        _usedPositions.Add(position);
        return position;
    }

    private bool IsTooClose(Vector3 position)
    {
        foreach (Vector3 usedPosition in _usedPositions)
        {
            if (Vector3.Distance(position, usedPosition) < minDistance)
                return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = new Color(1, 1, 1, 0.01f);
        Gizmos.DrawCube(position, limit);
        Gizmos.color = new Color(1, 1, 1, 0.4f);
        Gizmos.DrawWireCube(position, limit);
    }
}