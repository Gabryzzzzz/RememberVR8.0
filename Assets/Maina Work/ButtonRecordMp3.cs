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
using SimpleJSON;
using System.Text.RegularExpressions;
using System;


public class ButtonRecordMp3 : MonoBehaviour
{
    AudioClip myAudioClip;
    public AudioSource sourceAud;
    public int toggle = 1;
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    [SerializeField] bool isPressed;
    public int layer;
    public bool toggleRec=false;
    public bool startedRec = false;
    public bool hasEnded = true;
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
            print("PRESSED!");
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
        StartCoroutine(RecordingWavToggle());
    }

    public IEnumerator RecordingWavToggle()
    {
        if(hasEnded == true)
        {
            if (toggleRec == false)
            {
                print("Inizio clip");
                yield return new WaitForSeconds(1);
                startedRec = true;
                myAudioClip = Microphone.Start(null, false, 10, 44100);
                toggleRec = true;
            }
            else if (startedRec = true)
            {
                print("Fineclip -- crea Wav");
                UnityEngine.Microphone.End(null);
                SavWav.Save("VoiceMp3", myAudioClip);
                print("Generato Wav");
                StartCoroutine(afterRecord());
                toggleRec = false;
                startedRec = false;
                yield return null;
            }
        }
    }

    public IEnumerator afterRecord()
    {
        hasEnded = false;
        print("Iniziato afterRecord");

<<<<<<< Updated upstream
        var token = "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"; // sostituisci con il tuo token
        var filePath = "C:\\Users\\andre\\Desktop\\asset\\VoiceMp3.wav"; // sostituisci con il percorso del tuo file
=======
        var token = "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"; // sostituisci con il tuo token
        var filePath = "C:\\Users\\andrea.mainardi\\Desktop\\AudioUnity\\VoiceMp3.wav"; // sostituisci con il percorso del tuo file
>>>>>>> Stashed changes
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
            StartCoroutine(chatGptCon(request.downloadHandler.text));
        }
        else
        {
            print("Error: " + request.error);
        }
    }

    public IEnumerator chatGptCon(string vcInput)
    {
        string openAIURL = "https://api.openai.com/v1/chat/completions";
<<<<<<< Updated upstream
        string openAIKey = "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";
=======
        string openAIKey = "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";
>>>>>>> Stashed changes
        string openAIModel = "gpt-3.5-turbo";
        float temperature = 0.7f;
        string message = vcInput;
        string role = "user";

        // create JSON payload
        JSONNode json = JSON.Parse("{}");
        json.Add("model", openAIModel);
        JSONNode messageJson = JSON.Parse("{}");
        messageJson.Add("role", role);
        messageJson.Add("content", message);
        JSONArray messagesJson = new JSONArray();
        messagesJson.Add(messageJson);
        json.Add("messages", messagesJson);
        json.Add("temperature", temperature);

        // create UnityWebRequest and set headers
        UnityWebRequest request = UnityWebRequest.Post(openAIURL, UnityWebRequest.kHttpVerbPOST);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + openAIKey);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json.ToString());
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // send request and wait for response
        yield return request.SendWebRequest();

        // check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            print(request.error);
            hasEnded = true;
        }
        else
        {
            // log response
            JSONNode responseJson = JSON.Parse(request.downloadHandler.text);

            JSONNode choiceJson = responseJson["choices"][0]["message"];
            string content = choiceJson["content"];

            print(request.downloadHandler.text);
            print(content);


            StartCoroutine(GetTTS(content));

            hasEnded = true;
        }
    }

    public IEnumerator GetTTS(string TTSwords)
    {
<<<<<<< Updated upstream
        // Remove the "spaces" in excess
        Regex rgx = new Regex("\\s+");
        // Replace the "spaces" with "% 20" for the link Can be interpreted
        var result = rgx.Replace(TTSwords, "%20");
        Debug.Log(result);
        var url = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=it&q=" + result;
        //var request = UnityWebRequest.Post(url);
        //UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
=======
        var json = "{\"text\":\" " + TTSwords + "\"}";
        var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest www = UnityWebRequest.Post("https://localhost:7054/TTS/Create", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "text/plain");

>>>>>>> Stashed changes
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
<<<<<<< Updated upstream
                sourceAud.clip = DownloadHandlerAudioClip.GetContent(www);
                sourceAud.Play();
            }
        }

=======
                var contenuto = www.downloadHandler;



                var audioBytes = Convert.FromBase64String(www.downloadHandler.text);
                var tempPath = "C:\\Users\\andrea.mainardi\\Desktop\\Asset\\" + "tmpMP3Base64.wav";
                File.WriteAllBytes(tempPath, audioBytes);
                //sourceAud.clip = tempPath;
                //sourceAud.Play();
                yield return new WaitForSeconds(4);
                string audioToLoad = string.Format("C:\\Users\\andrea.mainardi\\Desktop\\Asset\\" + "{0}", "tmpMP3Base64.wav");
                using (WWW request = new WWW(audioToLoad))
                {
                    sourceAud.clip = request.GetAudioClip();
                    sourceAud.Play();
                }
                //audioClip = request.GetAudioClip();
                //Debug.log(contenuto);
                //sourceAud.clip = DownloadHandlerAudioClip.GetContent(www);
                //Debug.Log(www.downloadHandler.text);
            }
        }
>>>>>>> Stashed changes
    }

   
        /*    string audioToLoad = string.Format("C:\\Users\\andrea.mainardi\\Desktop\\Asset\\" + "{0}", "tmpMP3Base64.wav");
            WWW request = new WWW(audioToLoad);
            //audioClip = request.GetAudioClip();
            sourceAud.clip = request.GetAudioClip();
            sourceAud.Play();
        yield return null; */

        //audioClip.name = name;
        //clips.Add(audioClip);
        //return request;

        /* UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("C:\\Users\\andrea.mainardi\\Desktop\\Asset\\tmpMP3Base64.mp3", AudioType.MPEG);
            yield return request.SendWebRequest();
            if (request.result.Equals(UnityWebRequest.Result.ConnectionError))
                Debug.LogError(request.error);
            else
            {
                sourceAud.clip = DownloadHandlerAudioClip.GetContent(request);
                sourceAud.Play();
            } */
        //File.Delete(tempPath); */
   


    // Remove the "spaces" in excess
    //Regex rgx = new Regex("\\s+");
    // Replace the "spaces" with "% 20" for the link Can be interpreted
    //var result = rgx.Replace(TTSwords, "%20");
    // Debug.Log(result);
    // var url = "https://localhost:7054/TTS/Create";

    //var request = UnityWebRequest.Post(url);
    //UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
    /* using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
     {
         yield return www.SendWebRequest();
         if (www.result == UnityWebRequest.Result.ConnectionError)
         {
             Debug.Log(www.error);
         }
         else
         {
             sourceAud.clip = DownloadHandlerAudioClip.GetContent(www);
             sourceAud.Play();
         }
     } */

    /*
            string connUrl = "https://localhost:7054/TTS/Create";
            string text = TTSwords;

            // create UnityWebRequest and set headers
            UnityWebRequest request = UnityWebRequest.Post(connUrl, UnityWebRequest.kHttpVerbPOST);
            request.SetRequestHeader("Content-Type", "application/json");
            // request.SetRequestHeader("Authorization", "Bearer " + openAIKey);
            JSONNode json = JSON.Parse("{"+ text +"}");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.SetRequestHeader("Accept", " text/plain");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                print(request.error);
            }
            else
            {
                // log response
                sourceAud.clip = DownloadHandlerAudioClip.GetContent(request);
            }   */

}

