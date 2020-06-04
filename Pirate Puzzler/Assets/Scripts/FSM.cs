using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    private State currentState;
    private Dictionary<System.Type, State> states = new Dictionary<System.Type, State>();

    public void Init()
    {
        states.Clear();
        State[] allStates = GetComponents<State>();
        for (int i = 0; i < allStates.Length; i++)
        {
            allStates[i].InitState(this);
            Debug.Log("State found: " + allStates[i].GetType());
            states.Add(allStates[i].GetType(), allStates[i]);
        }
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }

    public void SwitchState(System.Type type)
    {
        currentState?.OnExit();
        if (states.ContainsKey(type))
        {
            currentState = states[type];
        }
        currentState?.OnEnter();
    }
}

public abstract class State : MonoBehaviour
{
    protected FSM fsm;
    public virtual void InitState(FSM fsm)
    {
        this.fsm = fsm;
    }
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();
}