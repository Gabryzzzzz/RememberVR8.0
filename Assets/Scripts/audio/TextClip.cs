using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextClip : MonoBehaviour
{
    int index;
    VoiceRecorder parentScript;
    AudioClip clip;
    [SerializeField] Color buttonTextColor;

    private void Awake()
    {
        index = transform.GetSiblingIndex();
        Transform parentTransform = transform.parent;
        parentScript = parentTransform.gameObject.GetComponent<VoiceRecorder>();
    }

    public void PlayClipWithButton()
    {
        print(index);
        clip = parentScript.aud[index];
        parentScript.goAudioSource.clip = clip;
        //parentScript.goAudioSource.clip = parentScript.aud[index];
        parentScript.goAudioSource.Play();
        StartCoroutine(StopClipAfterSeconds());
    }

    public void ChangeTextColor(Color color)
    {
        GetComponentInChildren<TMP_Text>().color = color;
    }

    public IEnumerator StopClipAfterSeconds()
    {
        ChangeTextColor(buttonTextColor);
        yield return new WaitForSeconds(parentScript.aud_lenght[parentScript.i]);
        parentScript.goAudioSource.Stop();
        ChangeTextColor(Color.black);

    }
}
