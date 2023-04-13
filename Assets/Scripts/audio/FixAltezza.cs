using UnityEngine;

public class FixAltezza : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //translate player y
        player.transform.position = new Vector3(player.transform.position.x, 10f, player.transform.position.z);
    }
}
