using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickTV : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void OnMouseDown()
    {
        panel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            panel.SetActive(true);
        }
    }
}
