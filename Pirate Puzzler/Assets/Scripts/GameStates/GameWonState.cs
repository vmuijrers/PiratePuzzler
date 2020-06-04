using UnityEngine;

public class GameWonState : State
{
    public Animator UIAnimator;
    public override void OnEnter()
    {
        //Yay we won!
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

