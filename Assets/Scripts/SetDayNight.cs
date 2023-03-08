using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDayNight : MonoBehaviour
{

    public Material night_skybox;
    public Material day_skybox;
    public string default_skybox = "day";

    // Start is called before the first frame update
    void Start()
    {
        if (default_skybox == "day")
        {
            RenderSettings.skybox = day_skybox;
        }
        else { 
            RenderSettings.skybox = night_skybox;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //camera_player.GetComponent<Skybox>().enabled = false;
            RenderSettings.skybox = night_skybox;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //camera_player.GetComponent<Skybox>().enabled = true;
            RenderSettings.skybox = day_skybox;

        }
    }
}
