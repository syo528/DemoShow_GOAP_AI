public class RoleEatAction : RoleActionBase
{
    private GOAPRunState runState;
    public override void OnStart()
    {
        // 先去篝火
        RoleMoveState moveState = role.stateMachine.ChangeState<RoleMoveState>();
        moveState.SetDestination(MapManager.Instance.CampfirePoint.position);
        moveState.SetCallback(OnMoveEnd, 2);
        runState = GOAPRunState.Runing;
    }

    private void OnMoveEnd()
    {
        // 到达篝火处，开始吃
        RolePerformState performState = role.stateMachine.ChangeState<RolePerformState>();
        performState.PlayAnimation("Eat", 1f, OnEatEnd);
    }

    private void OnEatEnd()
    {
        MapManager.Instance.OnRoleEat();
        role.stateMachine.ChangeState<RoleIdleState>();
        ApplyEffect();
        runState = GOAPRunState.Succeed;
    }

    public override GOAPRunState OnUpdate()
    {
        return runState;
    }
}
