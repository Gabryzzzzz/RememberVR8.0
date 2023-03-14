using UnityEngine;
using UnityEngine.UI;

public class PlayerNear : MonoBehaviour
{

    public GameObject game_object;
    public GameObject player;
    public Text text_test;

    // Start is called before the first frame update
    void Start()
    {
        text_test.text = "Player is not near";
    }

    // Update is called once per frame
    void Update()
    {


    }
}
