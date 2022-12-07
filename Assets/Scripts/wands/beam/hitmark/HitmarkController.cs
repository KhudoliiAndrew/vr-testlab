using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkController : MonoBehaviour
{
    [HideInInspector] public BeamHitmarkHelper beamHitmarkHelper;
    
    void Start()
    {
        Destroy(this, 1.5f);
    }

    private void OnDestroy()
    {
        beamHitmarkHelper.RemoveHitmarkFromList(gameObject);
    }
}
