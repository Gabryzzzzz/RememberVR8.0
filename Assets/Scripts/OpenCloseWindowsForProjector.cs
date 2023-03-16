using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenCloseWindowsForProjector : MonoBehaviour
{

    public GameObject window_telo;
    public GameObject soffitto_telo;
    Animator window_telo_Animator;
    Animator soffitto_telo_Animator;
    public Text window_state;
    bool isOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        window_telo_Animator = window_telo.GetComponent<Animator>();
        soffitto_telo_Animator = soffitto_telo.GetComponent<Animator>();
        window_state.text = "Window is closed";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            window_state.text = "Window is open";
            Debug.Log("m_Animator.SetTrigger(\"open\");");
            window_telo_Animator.SetTrigger("open");
            soffitto_telo_Animator.SetTrigger("open");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            window_state.text = "Window is closed";
            Debug.Log("m_Animator.SetTrigger(\"close\");");
            window_telo_Animator.SetTrigger("close");
            soffitto_telo_Animator.SetTrigger("close");
        }
    }

    public void openCloseToggle()
    {
        if(isOpen == true)
        {
            isOpen = false;
            closeWindow();
        }
        else
        {
            isOpen = true;
            openWindow();
        }
    }

    public void closeWindow()
    {
        window_state.text = "Window is closed";
        Debug.Log("m_Animator.SetTrigger(\"close\");");
        window_telo_Animator.SetTrigger("close");
        soffitto_telo_Animator.SetTrigger("close");
    }

    public void openWindow()
    {
        window_state.text = "Window is open";
        Debug.Log("m_Animator.SetTrigger(\"open\");");
        window_telo_Animator.SetTrigger("open");
        soffitto_telo_Animator.SetTrigger("open");
    }
}
