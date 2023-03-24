using System.Collections;
using System.Collections.Generic;
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
       /* if (Vector3.Distance(game_object.transform.position, player.transform.position) < 2.5f)
        {
            Debug.Log("Player is near");
            text_test.text = "Player is near";
        }
        else 
        {
            text_test.text = "Player is not near";
        } */

    }
}
