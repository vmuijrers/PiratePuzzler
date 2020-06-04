using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : Unit
{
    public UnityEvent OnPlayerDiedEvent;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Reset()
    {
        IsDead = false;
        IsStunned = false;
        animator.SetBool("Dead", false);
    }

    public override void Die()
    {
        base.Die();
        animator.SetBool("Dead", true);
        OnPlayerDiedEvent?.Invoke();
    }

    public override void GetStunned()
    {
        base.GetStunned();
        animator.SetTrigger("GetHit");
    }

    protected override IEnumerator MoveToPosition(Vector3Int targetPosition, System.Action onDone = null)
    {
        animator.SetBool("Run", true);
        yield return base.MoveToPosition(targetPosition, onDone);
        animator.SetBool("Run", false);
    }

    public void Attack()
    {
        animator.SetTrigger(Utility.Choose("Attack 01", "Attack 02"));
    }

}
