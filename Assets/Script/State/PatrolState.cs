using UnityEngine;

public class PatrolState : State
{
    public Vector3[] waypoints;
    private int _currentWaypointIndex;
    private Vector3 _currentWaypoint;
    private bool _justInitialized;
    // animation to play when the AI is in this state
    public string animationName;
    // next state to transition to if the AI sees the player
    public State nextState;
    public float detectionRange;


    public override void Enter()
    {
        _justInitialized = true;
        GetComponent<AniController>().ChangeAnimationState(animationName);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
       // if just initialized, set the first waypoint to be closet to the AI
        if (_justInitialized)
        {
            _justInitialized = false;
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (i == 0)
                {
                    _currentWaypoint = waypoints[i];
                    _currentWaypointIndex = i;
                }
                else
                {
                    if (Vector3.Distance(transform.position, waypoints[i]) < Vector3.Distance(transform.position, _currentWaypoint))
                    {
                        _currentWaypoint = waypoints[i];
                        _currentWaypointIndex = i;
                    }
                }
            }
        }

        // if the AI is close to the current waypoint, set the next waypoint
        if (Vector3.Distance(transform.position, _currentWaypoint) < 0.5f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
            _currentWaypoint = waypoints[_currentWaypointIndex];
        }

        // if the player is within the detection range, transition to the next state
        if (Vector3.Distance(transform.position, GetComponent<MonsterUtil>().player.transform.position) < detectionRange)
        {
            _stateMachine.ChangeState(nextState.GetType());
        }

        // move the AI towards the current waypoint
        GetComponent<MonsterUtil>().SetMovingPoint(_currentWaypoint);

  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i], 0.5f);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
