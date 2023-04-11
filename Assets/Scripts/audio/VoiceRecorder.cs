using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

    
[RequireComponent(typeof(AudioSource))]
public class VoiceRecorder : MonoBehaviour
{
    // Boolean flags shows if the microphone is connected   
    private bool micConnected = false;

    //The maximum and minimum available recording frequencies    
    private int minFreq;
    private int maxFreq;
    public List <AudioClip> aud;
    public List <float> aud_lenght;
    public int i = 0;
    public int clipNumber = 0;
    [SerializeField] GameObject content;
    [SerializeField] Button audioList;
    [SerializeField] TMP_Text textMessage;

    public Stopwatch timer = new();

    //A handle to the attached AudioSource    
    public AudioSource goAudioSource;



    void Start()
    {

        //Check if there is at least one microphone connected    
        if (Microphone.devices.Length <= 0)
        {
            //Throw a warning message at the console if there isn't    
            //Debug.LogWarning("Microphone not connected!");
        }
        else //At least one microphone is present    
        {
            //Set our flag 'micConnected' to true    
            micConnected = true;

            //Get the default microphone recording capabilities    
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...    
            if (minFreq == 0 && maxFreq == 0)
            {
                //...meaning 44100 Hz can be used as the recording sampling rate    
                maxFreq = 44100;
            }

            //Get the attached AudioSource component    
            goAudioSource = this.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        //If there is a microphone    
        if (micConnected)
        {
            //If the audio from any microphone isn't being captured    
            if (!Microphone.IsRecording(null))
            {
                textMessage.color = Color.white;
                textMessage.text = "Try to record a new audio ";
                //Case the 'Record' button gets pressed    
                if (Input.GetKeyDown(KeyCode.G))
                {
                    //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource    
                    recordAudio();
                }
            }
            else //Recording is in progress    
            {
                //Case the 'Stop and Play' button gets pressed    
                if (Input.GetKeyDown(KeyCode.H))
                {
                    stopAudio();
                }
            }
        }
        else // No microphone    
        {
            textMessage.color = Color.red;
            textMessage.text = "Microphone not connected!";
            //Print a red "Microphone not connected!" message at the center of the screen
        }

    }

    private void StartRecording() {
        aud.Add(Microphone.Start(null, true, 20, maxFreq));
    }
    public void addClipInList()
    {
        Button AudioListClone = Instantiate(audioList, content.transform);
        AudioListClone.GetComponentInChildren<TMP_Text>().text = "Clip: " + clipNumber;
    }

    public void recordAudio()
    {
        textMessage.text = "";
        StartRecording();
        aud_lenght.Add(0);
        timer.Start();
    }
    public void stopAudio()
    {
        Microphone.End(null); //Stop the audio recording    
                              //goAudioSource.Play(); //Playback the recorded audio    

        timer.Stop();
        aud_lenght[aud.Count - 1] = (float)timer.Elapsed.TotalSeconds;
        timer.Reset();
        clipNumber++;
        addClipInList();
    }
}
