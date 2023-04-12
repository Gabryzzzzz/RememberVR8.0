using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRecordClip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] VoiceRecorder voiceRecorder;
    public void isGrabbed()
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
