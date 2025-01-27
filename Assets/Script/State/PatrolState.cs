using UnityEngine;

public class PatrolState : State
{
    public Vector3[] waypoints;
    [SerializeField]
    private int _currentWaypointIndex;
    [SerializeField]
    private Vector3 _currentWaypoint;
    [SerializeField]
    private bool _justInitialized;
    // animation to play when the AI is in this state
    public string animationName;
    // next state to transition to if the AI sees the player
    public State nextState;
    public float detectionRange;
    public float waypointRadius = 0.5f;


    public override void Enter()
    {
        // Debug.Log("Patrol State");
        _justInitialized = true;
        GetComponent<AniController>().ChangeAnimationState(animationName);
    }

    public override void Exit()
    {
        // Debug.Log("Exit Patrol State");
        GetComponent<MonsterUtil>().StopMoving();
    }

    public override void Tick()
    {
        // if the player is within the detection range, transition to the next state
        if (Vector3.Distance(transform.position, GetComponent<MonsterUtil>().player.transform.position) < detectionRange && !_justInitialized)
        {
            _stateMachine.ChangeState(nextState.GetType());
        }

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
            GetComponent<MonsterUtil>().SetMovingPoint(_currentWaypoint);
        }



        // Debug.Log(Vector3.Distance(transform.position, _currentWaypoint));
        // if the AI is close to the current waypoint, set the next waypoint
        if (Vector3.Distance(transform.position, _currentWaypoint) < waypointRadius)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
            _currentWaypoint = waypoints[_currentWaypointIndex];
            // move the AI towards the current waypoint
            GetComponent<MonsterUtil>().SetMovingPoint(_currentWaypoint);
        }




        

  
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
