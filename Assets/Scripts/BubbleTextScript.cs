using TMPro;
using UnityEngine;

public class BubbleTextScript : MonoBehaviour
{

    //get the ref to chatBubble
    private GameObject chatBubble;
    //get textmesh pro component
    private TextMeshPro textMeshPro;

    public float popup_size = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //get the chatBubble from is child
        chatBubble = transform.GetChild(0).gameObject;
        //get the textmesh pro component from is child
        textMeshPro = transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();

        SetupBubbleSize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //the object is made from 3 parts, when the central parte increase is scale i the outside part shift position
    private void SetupBubbleSize()
    {
        transform.localScale = new Vector3(popup_size * 0.1f, popup_size * 0.1f, popup_size * 0.1f);
    }


}
