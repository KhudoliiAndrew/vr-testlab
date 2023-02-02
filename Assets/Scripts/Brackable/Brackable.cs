using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Brackable : MonoBehaviour
{
    public GameObject brokenObject;
    
    public void Broke()
    {
        var spawnedObject = Instantiate(brokenObject, transform.position, transform.rotation);
        spawnedObject.AddComponent<BrockenController>();
        
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 8)
            Broke();
    }
}
