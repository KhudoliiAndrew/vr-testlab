using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkController : MonoBehaviour
{
    public BeamHitmarkHelper beamHitmarkHelper;
    
    [HideInInspector] public String id = Guid.NewGuid().ToString();
    
    void Start()
    {
          Destroy(this.gameObject, 1.5f);
    }

    private void OnDestroy()
    {
        beamHitmarkHelper.RemoveHitmarkFromList(gameObject);
    }
}
