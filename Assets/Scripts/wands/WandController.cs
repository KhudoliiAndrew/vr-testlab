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

    private KeywordRecognizer _keywordRecognizer;
    private Dictionary<string, Action> _actions = new Dictionary<string, Action>();
  
    // Start is called before the first frame update
    void Start()
    {
        _actions.Add("light", ChangeWandStatus);

        _keywordRecognizer = new KeywordRecognizer(_actions.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += OnRecognizedSpeech;
        _keywordRecognizer.Start();

    }


    private void OnRecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        _actions[speech.text].Invoke();
    }

    private void ChangeWandStatus()
    {
        pointerLight.SetActive(!pointerLight.activeSelf);
        endWandMaterial.SetActive(!endWandMaterial.activeSelf);
    }
}
