using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnOffLight : MonoBehaviour
{

    public Light projector_light;
    public Light projector_light_trail;
    public GameObject light_trail;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            projector_light.enabled = !projector_light.enabled;
            projector_light_trail.enabled = !projector_light_trail.enabled;
            light_trail.SetActive(projector_light.enabled);
        }


    }
}
