using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurnState : State
{
    private List<Enemy> enemies = new List<Enemy>();
    public override void InitState(FSM fsm)
    {
        base.InitState(fsm);
        enemies.Clear();
        enemies = FindObjectsOfType<Enemy>().ToList();
    }

    public override void OnEnter()
    {
        Debug.Log("Enemy Turn Started");
        StartCoroutine(DoEnemyMovement());
    }

    public override void OnExit()
    {
        enemies.ForEach(x => x.ResetStunned());
        Debug.Log("Enemy Turn Ended");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }

    IEnumerator DoEnemyMovement()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy.IsStunned || enemy.IsDead) { continue; }
            enemy.DoAction();
            yield return new WaitForSeconds(1f);
        }

        GameController.Instance.EndEnemyTurn();
    }
}
