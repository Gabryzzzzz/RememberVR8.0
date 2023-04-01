using System.Collections;
using UnityEngine;
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


    private IEnumerator TrackAnimationDuration()
    {
        animation_running = true;
        yield return new WaitForSeconds(4);
        animation_running = false;
    }

    private void OpenAll()
    {
        if (!window_telo_Animator.GetCurrentAnimatorStateInfo(0).IsName("close_window"))
        {
            window_state.text = "Window is open";
            Debug.Log("m_Animator.SetTrigger(\"open\");");
            window_telo_Animator.SetTrigger("open");
            soffitto_telo_Animator.SetTrigger("open");
            StartCoroutine(nameof(TrackAnimationDuration));
        }
    }

    private void CloseAll()
    {
        if (!window_telo_Animator.GetCurrentAnimatorStateInfo(0).IsName("open_window"))
        {
            window_state.text = "Window is closed";
            Debug.Log("m_Animator.SetTrigger(\"close\");");
            window_telo_Animator.SetTrigger("close");
            soffitto_telo_Animator.SetTrigger("close");
            StartCoroutine(nameof(TrackAnimationDuration));
        }
    }

    private bool animation_running = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !animation_running)
        {
            OpenAll();
        }

        if (Input.GetKeyDown(KeyCode.V) && !animation_running)
        {
            CloseAll();
        }
    }

    public void openCloseToggle()
    {
        if (!animation_running)
        {
            if (isOpen == true)
            {
                isOpen = false;
                OpenAll();
            }
            else
            {
                isOpen = true;
                CloseAll();
            }
        }

    }


}
