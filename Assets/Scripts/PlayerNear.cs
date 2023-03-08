using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNear : MonoBehaviour
{

    public GameObject game_object;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(game_object.transform.position, player.transform.position) < 2.5f) 
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity))
            {
                var obj = hit.collider.gameObject;

                Debug.Log($"looking at {obj.name}", this);
            }
            Debug.Log("Player is near");
        }
    }
}
