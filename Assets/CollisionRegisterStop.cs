using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRegisterStop : MonoBehaviour
{
    [SerializeField] VoiceRecorder voiceRecorder;
    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        voiceRecorder.stopAudio();
    }
}
