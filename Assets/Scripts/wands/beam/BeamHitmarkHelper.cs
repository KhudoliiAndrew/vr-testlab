using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeamHitmarkHelper : MonoBehaviour
{
    public GameObject Bullet_Mark;

    private List<GameObject> hitmarks = new();

    private void FullFillHitmarks(GameObject previousHitmark, Vector3 endPosition, Vector3 normal, Transform hittedObject)
    {
        if (previousHitmark.transform.parent != hittedObject) return;

        float count = Vector3.Distance(previousHitmark.transform.position, endPosition) * 10;

        for (float i = 0; i < 1; i += 1 / count)
        {
            SpawnHitmark(Vector3.Lerp(previousHitmark.transform.position, endPosition, i), normal, hittedObject);
        }
    }

    private void SpawnHitmark(Vector3 position, Vector3 normal, Transform hittedObject)
    {
        var hitMark = Instantiate(Bullet_Mark, position, Quaternion.LookRotation(normal));
        hitMark.transform.Rotate(Vector3.up * 180);
        hitMark.transform.Rotate(Vector3.forward * Random.Range(.0f, 180.0f));
        
        var scaleValue = Random.Range(-.2f, .1f);
        hitMark.transform.localScale += new Vector3(scaleValue, scaleValue, 0);

        hitMark.GetComponent<HitmarkController>().beamHitmarkHelper = this;
        hitMark.transform.SetParent(hittedObject);

        hitmarks.Add(hitMark);
    }

    public void CreateHitmarks(Vector3 position, Vector3 normal, Transform hittedObject)
    {
        SpawnHitmark(position, normal, hittedObject);

        if (hitmarks.Count > 1)
        {
            FullFillHitmarks(hitmarks[hitmarks.Count - 2], position, normal, hittedObject);
        }
        else
        {
            SpawnHitmark(position, normal, hittedObject);
        }
    }

    public void RemoveHitmarkFromList(GameObject hitmark)
    {
        for (var i = 0; i < hitmarks.Count; i++)
        {
            if (hitmarks[i].GetComponent<HitmarkController>().id == hitmark.GetComponent<HitmarkController>().id)
            {
                hitmarks.RemoveAt(i);
                break;
            }
        }
    }
}