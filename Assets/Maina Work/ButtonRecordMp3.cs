using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Threading.Tasks;

public class ButtonRecordMp3 : MonoBehaviour
{
    AudioClip myAudioClip;
    public int toggle = 1;
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


    public void RecordingToggle()
    {
        if(toggle == 1)
        {
            print("Inizio clip");
            myAudioClip = Microphone.Start(null, false, 10, 44100);
            toggle = 0;
        }
        else
        {
            print("Fineclip -- crea Wav");
            SavWav.Save("VoiceMp3", myAudioClip);
            print("Generato Wav");
            StartCoroutine(afterRecord());
            toggle = 1;
        }
    }

    public IEnumerator afterRecord()
    {
        print("Iniziato afterRecord");

        var token = "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"; // sostituisci con il tuo token
        var filePath = "C:\\Users\\andre\\Desktop\\asset\\VoiceMp3.wav"; // sostituisci con il percorso del tuo file
        var model = "whisper-1";
        var url = "https://api.openai.com/v1/audio/transcriptions";
        print("Settati parametri");

        var form = new WWWForm();
        var fileContent = File.ReadAllBytes(filePath);
        form.AddBinaryData("file", fileContent, Path.GetFileName(filePath), "audio/mpeg");
        form.AddField("model", model);

        var request = UnityWebRequest.Post(url, form);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            print(request.downloadHandler.text);
        }
        else
        {
            print("Error: " + request.error);
        }
    }

}