using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action>  actions = new Dictionary<string, Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("forward", OnAction);        
        actions.Add("left", OnAction);        
        actions.Add("right", OnAction);        
        actions.Add("back", OnAction);
        actions.Add("pick up", OnAction);
        actions.Add("put down", OnAction);
        
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnRecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void OnRecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void OnAction()
    {
        
    }
}
