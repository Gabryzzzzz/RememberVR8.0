using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject player;
    public List<GameObject> waypoints = new();
    int current_waypoint = 0;
    Animator bot_animator;

    public float chase_distance = 6f;
    public float stop_distance = 4f;


    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[current_waypoint].transform.position);
        current_waypoint++;
        bot_animator = transform.GetComponent<Animator>();

        StartCoroutine(nameof(log_every_5_seconds));
    }

    bool just_interacted = false;


    private void StartBotAnimation()
    {
        bot_animator.SetFloat("Speed_f", 0.26f);

    }

    private void StopBotAnimation()
    {
        bot_animator.SetFloat("Speed_f", 0f);
    }

    private int log_count = 0;
    //log every 5 secons
    private IEnumerator log_every_5_seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("near_player " + log_count + " " + near_player);
            Debug.Log("very_near " + log_count + " " + very_near);
            Debug.Log("i_can_feel_it " + log_count + " " + i_can_feel_it);
            log_count++;
        }
    }


    private bool near_player;
    private bool very_near;
    private bool i_can_feel_it;
    private bool far_from_player;
    // Update is called once per frame
    void Update()
    {

        near_player = UtilsGabryzzzzz.two_object_near(transform, player.transform, chase_distance);
        far_from_player = !UtilsGabryzzzzz.two_object_near(transform, player.transform, chase_distance + 2f);
        if (near_player)
        {

            i_can_feel_it = UtilsGabryzzzzz.two_object_near(transform, player.transform, stop_distance);
            very_near = UtilsGabryzzzzz.two_object_near(transform, player.transform, chase_distance - 1f);

            if (i_can_feel_it && very_near)
            {
                just_interacted = true;
                stop_move();
                StopBotAnimation();
            }
            else if (!very_near && !i_can_feel_it)
            {
                just_interacted = true;
                //Debug.Log("Chase player");
                ChasePlayer();
                StartBotAnimation();
            }
            UtilsGabryzzzzz.good_look_at(transform, player, 3f);
        }
        else if (far_from_player)
        {
            if (just_interacted == true)
            {
                stop_move();
                StopBotAnimation();
                StartCoroutine(nameof(pause_before_patrol));
            }
            else
            {
                StopCoroutine(nameof(pause_before_patrol));
                //Debug.Log("Patroling");
                Patroling();
                StartBotAnimation();
                just_interacted = false;
            }
        }
    }

    private IEnumerator pause_before_patrol()
    {
        yield return new WaitForSeconds(3);
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
        agent.SetDestination(player.transform.position + player.transform.forward * 1.5f);
        UtilsGabryzzzzz.good_look_at(transform, player, 3f);
    }

    private void stop_move()
    {
        agent.SetDestination(transform.position);
        UtilsGabryzzzzz.good_look_at(transform, player, 3f);
    }

}
