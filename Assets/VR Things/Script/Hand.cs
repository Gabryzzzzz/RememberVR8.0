using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float currentTrigger;
    private float currentGrip;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }
    void AnimateHand()
    {
        if (currentGrip!=gripTarget)
        {
            currentGrip = Mathf.MoveTowards(currentGrip, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip",currentGrip);
        } if (currentTrigger!=triggerTarget)
        {
            currentTrigger = Mathf.MoveTowards(currentTrigger, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat("Trigger",currentTrigger);
        }


    }
}
