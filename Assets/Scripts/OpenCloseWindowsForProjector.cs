using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseWindowsForProjector : MonoBehaviour
{

    public GameObject window_telo;
    public GameObject soffitto_telo;
    Animator window_telo_Animator;
    Animator soffitto_telo_Animator;

    // Start is called before the first frame update
    void Start()
    {
        window_telo_Animator = window_telo.GetComponent<Animator>();
        soffitto_telo_Animator = soffitto_telo.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("m_Animator.SetTrigger(\"open\");");
            window_telo_Animator.SetTrigger("open");
            soffitto_telo_Animator.SetTrigger("open");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("m_Animator.SetTrigger(\"close\");");

            window_telo_Animator.SetTrigger("close");
            soffitto_telo_Animator.SetTrigger("close");
        }
    }
}
