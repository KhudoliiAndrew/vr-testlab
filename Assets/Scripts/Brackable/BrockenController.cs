using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrockenController : MonoBehaviour
{
    private float _timer = 5f;

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 5f)
        {
            float scale = Mathf.Lerp(0, 1, _timer / 2f);
            ScaleObject(transform, scale);
        }

        if (_timer <= 0f)
            Destroy(gameObject);
        
    }
    
    private void ScaleObject(Transform obj, float scale)
    {
        foreach (Transform child in obj)
        {
            if (child.localScale.x < .01f || child.localScale.y < .01f || child.localScale.z < .01f)
                return;
            
            child.localScale *= scale;
        }
    }
}
