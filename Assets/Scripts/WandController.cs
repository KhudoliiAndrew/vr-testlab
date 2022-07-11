using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class WandController : MonoBehaviour
{

    public GameObject pointerLight;
    public GameObject endWandMaterial;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
  
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("light", changeWandStatus);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnRecognizedSpeech;
        keywordRecognizer.Start();

    }


    private void OnRecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void changeWandStatus()
    {
        Debug.Log("i'm here");

        pointerLight.SetActive(!pointerLight.activeSelf);
        endWandMaterial.SetActive(!endWandMaterial.activeSelf);
    }
}
