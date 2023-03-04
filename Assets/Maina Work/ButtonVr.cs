using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using UnityEditor;
using UnityEngine.UI;

public class ButtonVr : MonoBehaviour
{
    public TextMesh VC;
    public DictationRecognizer dictationRecognizer;
    public int toggle = 0;
    public string testo = "";
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    [SerializeField] bool isPressed;
    public int layer;
    void Start()
    {
        isPressed = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.gameObject.layer == layer)
        {
            button.transform.localPosition = new Vector3(0, 0.03f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.09f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    private void updateUi(string testo)
    {
        VC.text = testo;
    }

    public void SpawnSphere()
    {
        /*GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 0, 8);
        sphere.AddComponent<Rigidbody>();*/
        if(toggle == 0)
        {
            dictationRecognizer = new DictationRecognizer();
            dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
            dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
            dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
            dictationRecognizer.DictationError += DictationRecognizer_DictationError;
            dictationRecognizer.Start();
            toggle = 1;
        }
        else
        {
            dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
            dictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
            dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
            dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
            updateUi(testo);
            print(testo);
            testo = "";
            toggle = 0;
        }
        
    }


    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        /*
         print(text);
         print(confidence); */
        testo = testo + text;
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        // print("RISUTLATO:");
        //  print(text);
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        // do something
    }

    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        // do something
    }

}