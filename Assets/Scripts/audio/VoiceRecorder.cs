using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

    
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
    public TMP_Text testo;

    public Stopwatch timer = new();

    //A handle to the attached AudioSource    
    private AudioSource goAudioSource;

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

    private IEnumerator StopClipAfterSeconds() {
        yield return new WaitForSeconds(aud_lenght[i]);
        goAudioSource.Stop();
        //testo.text = "";
    }

    void Update()
    {
        //If there is a microphone    
        if (micConnected)
        {
            //If the audio from any microphone isn't being captured    
            if (!Microphone.IsRecording(null))
            {
                //Case the 'Record' button gets pressed    
                if (Input.GetKeyDown(KeyCode.G))
                {
                    //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource    
                    //goAudioSource.clip = Microphone.Start(null, true, 20, maxFreq);

                    StartCoroutine(StartRecording());
                    aud_lenght.Add(0);
                    timer.Start();
                }
            }
            else //Recording is in progress    
            {
                testo.text = "Recording in progress...";
                //Case the 'Stop and Play' button gets pressed    
                if (Input.GetKeyDown(KeyCode.H))
                {
                    Microphone.End(null); //Stop the audio recording    
                                          //goAudioSource.Play(); //Playback the recorded audio    

                    testo.text = "Recording ended";
                    timer.Stop();
                    aud_lenght[aud.Count - 1] = (float)timer.Elapsed.TotalSeconds;
                    timer.Reset();
                }

                //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 25, 200, 50), "Recording in progress...");
            }

            if (Input.GetKeyDown(KeyCode.X) && aud.Count > 0 && !goAudioSource.isPlaying)
            {
                if (i == aud.Count - 1)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }

                goAudioSource.clip = aud[i];
                goAudioSource.Play();
                testo.text = "Playing clip:" +(i+1);
                StartCoroutine(StopClipAfterSeconds());
                //Debug.Log("aud[i] = " + aud[i].length);
                //Debug.Log("print i: " + i);
                //Debug.Log("aud.Count: " + aud.Count);
            }

            if (Input.GetKeyDown(KeyCode.Z) && aud.Count > 0 && !goAudioSource.isPlaying)
            {
                if (i == 0)
                {
                    i = aud.Count - 1;
                }
                else
                {
                    i--;
                }
                goAudioSource.clip = aud[i];
                goAudioSource.Play();
                testo.text = "Playing clip:" +(i+1);

                StartCoroutine(StopClipAfterSeconds());

            }
        }
        else // No microphone    
        {

            testo.text = "Microphone not connected!";
            //Print a red "Microphone not connected!" message at the center of the screen    
            //GUI.contentColor = Color.red;
            //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Microphone not connected!");
        }

    }

    private IEnumerator StartRecording() {
        aud.Add(Microphone.Start(null, true, 20, maxFreq));
        yield return null;
    }



}
