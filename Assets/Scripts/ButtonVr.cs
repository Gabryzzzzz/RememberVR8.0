using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class ButtonVr : MonoBehaviour
{

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    [SerializeField] bool isPressed;
    public int layer;
    public VideoPlayer video;
    bool isPlaying;
    public VideoClip[] clips;
    int i = 0;

    void Start()
    {
        isPressed = false;
        video.clip = clips[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.gameObject.layer==layer)
        {
            button.transform.localPosition = new Vector3(0, 0.03f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.09f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 0, 8);
        sphere.AddComponent<Rigidbody>();
    }

    public void startSao()
    {
        if(!isPlaying)
        {
            video.Play();
            isPlaying = true;
        }
        else
        {
            video.Pause();
            isPlaying = false;
        }
    }

    public void nextClip()
    {
        if(i==clips.Length-1)
        {
            i = 0;
        }
        else
        {
            i++;
        }
        video.clip = clips[i];
        video.Play();
    }

    public void previousClip()
    {
        if(i==0)
        {
            i=clips.Length - 1;
        }
        else
        {
            i--;
        }
        video.clip = clips[i];
        video.Play();
    }

    void Update()
    {

    }

}
