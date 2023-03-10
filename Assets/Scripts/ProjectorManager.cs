using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class ProjectorManager : MonoBehaviour
{

    public Light projector_light;
    public Light projector_light_trail;
    public GameObject light_trail;
    public VideoPlayer player;
    public VideoClip[] vids;

    public AudioSource source_audio_projectore;

    // Start is called before the first frame update
    void Start()
    {
    }


    IEnumerator StopAfter()
    {
        yield return new WaitForSeconds(5);
        source_audio_projectore.Stop();
    }

    int current_clip = 0;
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            projector_light.enabled = !projector_light.enabled;
            projector_light_trail.enabled = !projector_light_trail.enabled;
            light_trail.SetActive(projector_light.enabled);
            if (projector_light.enabled)
            {
                source_audio_projectore.time = 2.5f;
                source_audio_projectore.Play();
                player.Play();
            }
            else {
                source_audio_projectore.time = 26f;
                player.Stop(); // or beep.Stop()
                //StartCoroutine(nameof(StopAfter));
            }
        }

        if (projector_light.enabled) { 
            float audio_time = source_audio_projectore.time;
            if (audio_time > 22f && audio_time < 50f) {
                source_audio_projectore.time = 5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (current_clip == 0)
            {
                current_clip = vids.Length - 1;
            }
            else 
            {
                current_clip -= 1;
            }
            player.clip = vids[current_clip];
            if (projector_light.enabled)
            {
                player.Play();
            }
            else
            {
                player.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (current_clip == vids.Length - 1)
            {
                current_clip = 0;
            }
            else
            {
                current_clip += 1;
            }
            player.clip = vids[current_clip];
            if (projector_light.enabled)
            {
                player.Play();
            }
            else
            {
                player.Stop();
            }
        }


    }
}
