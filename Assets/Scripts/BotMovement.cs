using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public List<GameObject> waypoints = new();
    int current_waypoint = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("FPS Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[current_waypoint].transform.position);
        current_waypoint++;
    }

    bool just_interacted = false;

    // Update is called once per frame
    void Update()
    {
        float player_distance = Vector3.Distance(transform.position, player.position);
        if (player_distance < 5f && player_distance > 3f)
        {
            just_interacted = true;
            Debug.Log("Chase player");
            ChasePlayer();
        }
        else if (player_distance < 3f)
        {
            just_interacted = true;
            stop_move();
        }
        else
        {
            if (just_interacted == true)
            {
                stop_move();
                StartCoroutine(nameof(pause_before_patrol));
            }
            else
            {
                StopCoroutine(nameof(pause_before_patrol));
                Debug.Log("Patroling");
                Patroling();
                just_interacted = false;
            }
        }
    }

    private IEnumerator pause_before_patrol()
    {
        yield return new WaitForSeconds(5);
        just_interacted = false;
    }

    private void Patroling()
    {
        if (waypoints.Count == 0) return;
        if (Vector3.Distance(waypoints[current_waypoint].transform.position, transform.position) < 2f)
        {
            current_waypoint++;
            if (current_waypoint >= waypoints.Count)
            {
                current_waypoint = 0;
            }
        }

        agent.SetDestination(waypoints[current_waypoint].transform.position);
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position + player.forward * 1.5f);
    }

    private void stop_move()
    {
        agent.SetDestination(transform.position);
        good_look_at();
    }

    private void good_look_at()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);
    }

}
