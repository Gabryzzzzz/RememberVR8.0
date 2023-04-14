using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRegister : MonoBehaviour
{
    [SerializeField] VoiceRecorder voiceRecorder;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        voiceRecorder.recordAudio();
    }
}
