using SimpleJSON;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


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
    public bool toggleRec = false;
    public bool startedRec = false;
    public bool hasEnded = true;

    private readonly string token = "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";

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
        if (hasEnded == true)
        {
            if (toggleRec == false)
            {
                print("Inizio clip");
                yield return new WaitForSeconds(1);
                startedRec = true;
                myAudioClip = Microphone.Start(null, false, 10, 44100);
                toggleRec = true;
            }
            else if (startedRec == true)
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


        var filePath = "C:\\Users\\gabri\\Desktop\\AudioUnity\\VoiceMp3.wav"; // sostituisci con il percorso del tuo file
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

        string openAIKey = token;
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

    public GameObject audio_source;

    public IEnumerator GetTTS(string TTSwords)
    {
        var json = "{\"text\":\" " + TTSwords + "\"}";
        var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:5088/TTS/Create", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "text/plain");

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {

                var contenuto = www.downloadHandler;

                var audioBytes = Convert.FromBase64String(www.downloadHandler.text);
                var tempPath = "C:\\Users\\gabri\\Desktop\\Asset\\" + "tmpMP3Base64.wav";
                File.WriteAllBytes(tempPath, audioBytes);
                //sourceAud.clip = tempPath;
                //sourceAud.Play();
                Debug.Log("MAO");
                Debug.Log("DIOCANEBASTARDOSTRONZOMANNAGGIAPUTTANA");
                string audioToLoad = string.Format("C:\\Users\\gabri\\Desktop\\Asset\\" + "{0}", "tmpMP3Base64.wav");
                yield return new WaitForSeconds(1);

                using (WWW request = new WWW(audioToLoad))
                {
                    var gameobject_audio = Instantiate(audio_source, new Vector3(0, 0, 0), Quaternion.identity);

                    Debug.Log("MAOZEDONG");

                    gameobject_audio.name = "ODAIOUHDWIOAUI)AWDHJd oiwaghidohowaDIHAWODHIOawd";
                    AudioClip clip = request.GetAudioClip();
                    gameobject_audio.GetComponent<AudioSource>().clip = clip;
                    gameobject_audio.GetComponent<AudioSource>().Play();

                    yield return new WaitForSeconds(clip.length);

                    Destroy(gameobject_audio);
                    Debug.Log("MAOZEDONG 2");


                }
                //audioClip = request.GetAudioClip();
                //Debug.log(contenuto);
                //sourceAud.clip = DownloadHandlerAudioClip.GetContent(www);
                //Debug.Log(www.downloadHandler.text);
            }
        }
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

