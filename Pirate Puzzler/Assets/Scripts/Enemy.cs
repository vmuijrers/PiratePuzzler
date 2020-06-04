using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Unit
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        animator.SetTrigger("Die");
    }

    public void DoAction()
    {
        if (IsNearPlayer())
        {
            StartCoroutine(AttackPlayer());
        }
        else
        {
            MoveToPlayer();
        }
    }

    public bool IsNearPlayer()
    {
        if(CurrentTile == null) { return false; }
        if(CurrentTile.GetNeighbours().Length == 0) { return false; }
        return CurrentTile.GetNeighbours().Any(x => x == GameController.Instance.Level.GetPlayerTile());
    }

    public void MoveToPlayer()
    {
        Tile bestTile = CurrentTile.GetNeighbours().ToList().FindAll(x => x.IsFree()).OrderBy(x => Vector3Int.Distance(x.Position, GameController.Instance.Player.CurrentTile.Position)).FirstOrDefault();
        if(bestTile != null)
        {
            MoveToTile(bestTile);
        }
    }

    public void ResetStunned()
    {
        IsStunned = false;
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

    IEnumerator AttackPlayer()
    {
        Tile PlayerTile = GameController.Instance.Player.CurrentTile;
        LookAtPosition(PlayerTile.Position);
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger(Utility.Choose("Attack 01", "Attack 02"));
        yield return animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        GameController.Instance.Player.Die();
    }
}
