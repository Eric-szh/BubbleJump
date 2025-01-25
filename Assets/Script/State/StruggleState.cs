using UnityEngine;

public class StruggleState : State
{
    float struggleTime = 2f;
    public string struggleAnimation;
    public GameObject struggleEffect;
    public bool floatUp = false;
    public float floatSpeed = 1;
    private float originalGravity;
    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState(struggleAnimation);
        struggleEffect.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("Leave", struggleTime);
        if (floatUp)
        {
            originalGravity = GetComponent<Rigidbody2D>().gravityScale;
            GetComponent<Rigidbody2D>().gravityScale = -floatSpeed;
        }
    }

    public override void Exit()
    {
        struggleEffect.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = originalGravity;
        CancelInvoke("Leave");
    }

    public override void Tick()
    {
        if (floatUp)
        {
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        }
    }

    public void Leave()
    {
        GetComponent<StateMachine>().ChangeState(typeof(PatrolState));
    }

}
