using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Player Player { get; private set; }
    public Level Level { get; private set; }

    private FSM fsm;

    private void Awake()
    {
        Instance = this;
        Level = FindObjectOfType<Level>();
        fsm = GetComponent<FSM>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        Level.Init();
        Player = FindObjectOfType<Player>();
        Player.Reset();
        fsm.Init();
        fsm.SwitchState(typeof(PlayerTurnState));
    }

    // Update is called once per frame
    private void Update()
    {
        fsm.OnUpdate();
    }

    private void FixedUpdate()
    {
        fsm.OnFixedUpdate();
    }

    public void WinGame()
    {
        Debug.Log("Game Won!");
        fsm.SwitchState(typeof(GameWonState));
    }

    public void LoseGame()
    {
        Debug.Log("Game Over!");
        fsm.SwitchState(typeof(GameLostState));
    }

    public void EndPlayerTurn()
    {
        Debug.Log("Player Turn Ended!");
        fsm.SwitchState(typeof(EnemyTurnState));
    }

    public void EndEnemyTurn()
    {
        if (Player.IsDead) { LoseGame(); return; }
        fsm.SwitchState(typeof(PlayerTurnState));
    }
}
