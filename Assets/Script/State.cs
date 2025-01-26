using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateMachine _stateMachine;
    public bool canExit;

    protected virtual void Awake()
    {
        canExit = true;
        _stateMachine = GetComponent<StateMachine>();
    }

    public abstract void Enter();


    public abstract void Tick();


    public abstract void Exit();

}
