using System.Collections;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TextFollowPlayer : MonoBehaviour
{

    public string text_to_display = "Placeholder";
    public float y_offset = 0.9f;
    public float x_offset = 0f;
    public float z_offset = 0f;
    public float bubble_size = 1f;
    public float trigger_distance = 4f;

    //-------------------------------------------------------
    //Questa roba funziona ma è da sistemare, è poco elegante
    //-------------------------------------------------------

    private GameObject player;
    private GameObject text_helper;

    // Start is called before the first frame update
    void Start()
    {
        //if (GameObject.Find("").active)
        //{
        //    player = GameObject.Find("FPS Player");
        //}
        //else
        //{
        //}
        player = GameObject.Find("FPS Player");

    }

    public float destroy_time = 3;
    private bool is_destroying = false;
    //IEnumerator that wait for 3 second
    public IEnumerator DestoryAfter()
    {
        is_destroying = true;
        yield return new WaitForSeconds(destroy_time);
        Destroy(text_helper);
        is_destroying = false;
    }

    public void StickToDad()
    {
        text_helper.transform.position = new Vector3(transform.position.x + x_offset, transform.position.y + y_offset, transform.position.z + z_offset);
    }

    // Update is called once per frame
    void Update()
    {
        if (UtilsGabryzzzzz.two_object_near(transform, player.transform, trigger_distance) && player != null)
        {
            //Debug.Log("Player near to " + transform.name);
            //Intanciate a prefab from assets
            if (text_helper == null)
            {
                text_helper = Instantiate(Resources.Load("BubbleComplete", typeof(GameObject))) as GameObject;
                text_helper.GetNamedChild("Text").GetComponent<TextMeshPro>().text = text_to_display;
                text_helper.GetComponent<BubbleTextScript>().popup_size = bubble_size;
                //get the script inside texthelper and pass a value
            }
            else
            {
                if (is_destroying)
                {
                    StopCoroutine(nameof(DestoryAfter));
                    is_destroying = false;
                }

                UtilsGabryzzzzz.good_look_at(text_helper.transform, player, 3f);
                StickToDad();
            }
        }
        else
        {
            if (text_helper != null && !is_destroying && UtilsGabryzzzzz.two_object_near(transform, player.transform, trigger_distance + 1f))
            {
                //call the Destory after coroutine
                StartCoroutine(nameof(DestoryAfter));
            }
            else if (text_helper != null && is_destroying)
            {
                UtilsGabryzzzzz.good_look_at(text_helper.transform, player, 3f);
                StickToDad();
            }
        }
    }

}
