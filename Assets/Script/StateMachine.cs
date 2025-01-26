using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public State CurrentState => _currentState;
    [SerializeField]
    private State _currentState;
    public State startState;

    protected bool _isTransitioning;

    private void Start()
    {
        ChangeState(startState.GetType());
    }

    public void ChangeState(Type stateType)
    {
        State newState = (State)GetComponent(stateType);
        if (newState == null)
        {
            Debug.LogError("New state is null");
            return;
        }

        if (_currentState == null || _currentState.canExit)
        {
            InitNewState(newState);
        }
        else
        {
            Debug.LogError("Cannot transition to a new state.");
        }
    }


    void InitNewState(State targetState)
    {
        if (_currentState != targetState && !_isTransitioning)
        {
            CallNewState(targetState);
        }
        else
        {
            Debug.LogWarning("State is already transitioning from " + _currentState + " to " + targetState);
        }
    }

    void CallNewState(State newState)
    {
        _isTransitioning = true;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();

        _isTransitioning = false;
    }

    private void Update()
    {
        if (_currentState != null && !_isTransitioning)
        {
            _currentState.Tick();
        }
    }

}
