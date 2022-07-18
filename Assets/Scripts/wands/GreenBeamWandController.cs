using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class GreenBeamWandController : MonoBehaviour
{
    public Transform startBeam;
    public Transform endBeam;
    public GameObject endWandMaterial;
    public GameObject raycastPointer;
    
    public Transform startTarget;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private Vector3 collision = Vector3.zero;

    public GameObject lastHit;
    
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("shoot", changeWandStatus);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnRecognizedSpeech;
        keywordRecognizer.Start();

    }

    void FixedUpdate()
    {
        startBeam.position = startTarget.position;

        var ray = new Ray(raycastPointer.transform.position, raycastPointer.transform.forward);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1000))
        {
            lastHit = hit.transform.gameObject;
            endBeam.position = hit.point;
            collision = hit.point;
        }
    }

    private void OnRecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void changeWandStatus()
    {
        //pointerLight.SetActive(!pointerLight.activeSelf);
        endWandMaterial.SetActive(!endWandMaterial.activeSelf);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(collision, 1.0f);
    }
}
