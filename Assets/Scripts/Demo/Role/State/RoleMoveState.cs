using System;
using UnityEngine;

public class RoleMoveState : RoleStateBase
{
    private Action onEnd;
    private float stopDistance;
    public override void OnEnter()
    {
        role.PlayAniamtion("Move");
        role.StartMove();
    }
    public void SetDestination(Vector3 pos)
    {
        role.SetDestination(pos);
    }
    public void SetCallback(Action onEnd, float stopDistance)
    {
        this.onEnd = onEnd;
        this.stopDistance = stopDistance < 0 ? 0 : stopDistance;
    }
    public override void OnUpdate()
    {
        if (!role.navMeshAgent.isStopped && role.navMeshAgent.remainingDistance < stopDistance)
        {
            onEnd?.Invoke();
            onEnd = null;
        }
    }
    public override void OnExit()
    {
        role.StopMove();
        onEnd = null;
        stopDistance = 0;
    }
}
