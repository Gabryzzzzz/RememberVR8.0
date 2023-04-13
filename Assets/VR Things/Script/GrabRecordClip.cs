using System.Collections;
using UnityEngine;

public class GrabRecordClip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] VoiceRecorder voiceRecorder;
    public void isGrabbed()
    {
        Debug.Log("Microfono Grabbato");
        StartCoroutine("startClip");
    }
    public void isNotGrabbed()
    {
        Debug.Log("Microfono non Grabbato");
        StartCoroutine("stopClip");
    }

    public IEnumerator startClip()
    {
        voiceRecorder.recordAudio();
        yield return null;
    }

    public IEnumerator stopClip()
    {
        voiceRecorder.stopAudio();
        yield return null;
    }
}
