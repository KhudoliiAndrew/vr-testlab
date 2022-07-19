using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class GreenBeamWandController : MonoBehaviour
{
    public GameObject beam;

    public Transform startBeam;
    public Transform beam2;
    public Transform beam3;
    public Transform endBeam;
    public GameObject endWandMaterial;
    public GameObject raycastPointer;

    public Transform startTarget;

    private KeywordRecognizer _keywordRecognizer;
    private Dictionary<string, Action> _actions = new Dictionary<string, Action>();

    private Vector3 _collision = Vector3.zero;

    public GameObject lastHit;

    private GameObject lastHittedObject;
    // Start is called before the first frame update
    void Start()
    {
        _actions.Add("shoot", ChangeWandStatus);

        _keywordRecognizer = new KeywordRecognizer(_actions.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += OnRecognizedSpeech;
        _keywordRecognizer.Start();
    }

    void FixedUpdate()
    {
        beam2.transform.Rotate(0, 40 * Time.deltaTime, 0);
        beam3.transform.Rotate(0, 40 * Time.deltaTime, 0);

        startBeam.position = startTarget.position;

        var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            var o = hit.transform.gameObject;

            if (o != lastHittedObject)
            {
                if (lastHittedObject != null && lastHittedObject.GetComponent<Rigidbody>() != null)
                {
                    lastHittedObject.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            
            endBeam.position = hit.point;
            var position = raycastPointer.transform.position;
            beam2.position = (position + hit.point) * 0.3f;
            beam3.position = (position + hit.point) * 0.6f;
            _collision = hit.point;

            hit.transform.gameObject.transform.position = Vector3.Lerp(o.transform.position,
                hit.point, Time.deltaTime * 1.5f);

            if (o.GetComponent<Rigidbody>() != null)
            {
                o.GetComponent<Rigidbody>().useGravity = false;
            }

            lastHittedObject = o;
        }
    }

    private void OnRecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        _actions[speech.text].Invoke();
    }

    private void ChangeWandStatus()
    {
        beam.SetActive(!beam.activeSelf);
        endWandMaterial.SetActive(!endWandMaterial.activeSelf);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_collision, 1.0f);
    }
}