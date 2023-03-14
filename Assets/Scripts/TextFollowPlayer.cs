using TMPro;
using UnityEngine;

public class TextFollowPlayer : MonoBehaviour
{

    public Transform text_helper;
    public GameObject player;
    public TextMeshPro text_mesh_pro;
    public string text_to_display = "Placeholder";
    public float y_offset = 0.9f;
    public float x_offset = 0f;
    public float z_offset = 0f;
    public float text_size = 2;

    //-------------------------------------------------------
    //Questa roba funziona ma è da sistemare, è poco elegante
    //-------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        text_helper.transform.position = Vector3.zero;
        text_mesh_pro.text = text_to_display;
        text_mesh_pro.fontSize = text_size;
    }

    // Update is called once per frame
    void Update()
    {
        if (UtilsGabryzzzzz.two_object_near(transform, player.transform, 4f))
        {
            text_helper.transform.position = new Vector3(transform.position.x + x_offset, transform.position.y + y_offset, transform.position.z + z_offset);
            UtilsGabryzzzzz.good_look_at(text_helper, player, 3f);
        }
        else
        {
            if (text_helper != null)
            {
                text_helper.transform.position = Vector3.zero;
            }
        }
    }
}
