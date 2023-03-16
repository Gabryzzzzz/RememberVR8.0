using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using UnityEditor;
using UnityEngine.UI;

public class ButtonVrProjectorManager : MonoBehaviour
{
    public TextMesh VC;
    public DictationRecognizer dictationRecognizer;
    public int toggle = 0;
    public string testo = "";
    public GameObject button;
    public ProjectorManager projectorManager;
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

    public void projectorStartToggle()
    {
        projectorManager.startVideo();
    }

}