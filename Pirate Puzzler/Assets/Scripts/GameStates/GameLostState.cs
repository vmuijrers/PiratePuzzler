﻿using UnityEngine;

public class GameLostState : State
{
    public Animator UIAnimator;
    public override void OnEnter()
    {
        //Sad, we lost
        UIAnimator?.SetBool("Trigger", true);
    }

    public override void OnExit()
    {
        UIAnimator?.SetBool("Trigger", false);
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameController.Instance.GameStart();
        }
    }
}