using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSpawner : MonoBehaviour
{
    public Vector3 limit;
    
    public GameObject prefab;
    public int numberOfObjects = 4;

    private List<Transform> _usedPositions;
    public float minDistance = 1.0f;
    
    void Awake()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        _usedPositions = new List<Transform>();

        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 prefabSize = prefab.GetComponent<Renderer>().bounds.size;

        for (int i = 0; i < numberOfObjects; i++)
        {
            var newObject = Instantiate(prefab, GetRandomPosition(prefabSize), rotation);

            if (!SetUniquePosition(newObject.transform, prefabSize))
                DestroyImmediate(newObject);

            _usedPositions.Add(newObject.transform);
        }
    }

    private bool SetUniquePosition(Transform newObject, Vector3 scale)
    {
        int attempts = 0;
        
        do
        {
            newObject.position = GetRandomPosition(scale);

            attempts++;
            if (attempts >= 1000)
                return false;
        } while (IsTooClose(newObject));

        return true;
    }

    private bool IsTooClose(Transform position)
    {
        foreach (Transform usedPosition in _usedPositions)
        {
            if (ObjectDistance.GetClosestDistance(usedPosition, position) < minDistance)
                return true;
        }

        return false;
    }

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        return transform.position + new Vector3(
            Random.Range(-limit.x / 2 + scale.x / 2, limit.x / 2 - scale.x / 2),
            Random.Range(-limit.y / 2 + scale.y, limit.y / 2 - scale.y / 2),
            Random.Range(-limit.z / 2 + scale.z / 2, limit.z / 2 - scale.z / 2)
        );
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