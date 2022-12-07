using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHitmarkHelper : MonoBehaviour
{
    public GameObject Bullet_Mark;

    private List<GameObject> hitmarks;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FullFillHitmarks(Vector3 startPosition, Vector3 endPosition, Vector3 normal,
        Transform hittedObject)
    {
        float count = Vector3.Distance(startPosition, endPosition);

        for (int i = 0; i < 20; i++)
        {
            SpawnHitmark(Vector3.Lerp(startPosition, endPosition, i), normal, hittedObject);
        }
    }

    private void SpawnHitmark(Vector3 position, Vector3 normal, Transform hittedObject)
    {
        var hitMark = Instantiate(Bullet_Mark, position, Quaternion.LookRotation(normal));
        hitMark.transform.Rotate(Vector3.up * 180);
        hitMark.transform.Translate(Vector3.back * .01f);

        hitMark.GetComponent<HitmarkController>().beamHitmarkHelper = this;
        hitMark.transform.SetParent(hittedObject);
            
        hitmarks.Add(hitMark);
    }

    public void SpawnHitMark(Vector3 position, Vector3 normal, Transform hittedObject)
    {

        if (hitmarks.Count > 0)
        {
            FullFillHitmarks( hitmarks[hitmarks.Count].transform.position, position, normal, hittedObject);
        }
        else
        {
            SpawnHitmark(position, normal, hittedObject);
        }

    }

    public void RemoveHitmarkFromList(GameObject hitmark)
    {
        hitmarks.Remove(hitmark);
    }
}