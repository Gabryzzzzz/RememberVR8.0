using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRegisterStop : MonoBehaviour
{
    [SerializeField] VoiceRecorder voiceRecorder;
    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Microfono non Grabbato");
        StartCoroutine("stopClip");
    }
   

    public IEnumerator stopClip()
    {
        voiceRecorder.stopAudio();
        yield return null;
    }
}
