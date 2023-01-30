using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Brackable : MonoBehaviour
{
    public GameObject brokenObject;

    public void Broke()
    {
        Instantiate(brokenObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 4)
            Broke();
    }
}
