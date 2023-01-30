using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectsSpawner : MonoBehaviour
{
    public Vector3 scale;

    public GameObject objectToBeSpawned;
    // Start is called before the first frame update
    void Awake()
    {
        SpawnObjects();
        SpawnObjects();
        SpawnObjects();
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObjects()
    {
        Instantiate(objectToBeSpawned);
    }
    
    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawWireCube(transform.position, scale);
    }
}
