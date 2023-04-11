using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provagrabbed : MonoBehaviour
{
    [SerializeField] VoiceRecorder voiceRecorder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void isGrabbed()
    {
        Debug.Log("Microfono Grabbato");
        voiceRecorder.recordAudio();
    }
    public void isNotGrabbed()
    {
        Debug.Log("Microfono non Grabbato");
        voiceRecorder.stopAudio();
    }
}
