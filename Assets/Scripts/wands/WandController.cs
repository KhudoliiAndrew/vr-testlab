using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using BNG;

public class WandController : GrabbableEvents
{
    public GameObject pointerLight;
    public GameObject endWandMaterial;

    public override void OnTriggerDown()
    {
        ChangeWandStatus();

        base.OnTriggerDown();
    }

    private void ChangeWandStatus()
    {
        pointerLight.SetActive(!pointerLight.activeSelf);
        endWandMaterial.SetActive(!endWandMaterial.activeSelf);
    }
}
