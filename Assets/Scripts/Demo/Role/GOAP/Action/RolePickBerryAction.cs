public class RolePickBerryAction : RoleActionBase
{
    private GOAPRunState runState;
    private BerryController berry;
    private UnityObjectState ripeBerryState;
    public override void Init(GOAPAgent agent, IGOAPOwner owner)
    {
        base.Init(agent, owner);
        ripeBerryState = agent.states.GetState<UnityObjectState>("成熟浆果");
    }
    public override void OnStart()
    {
        // 1.移动到现有的浆果附近
        runState = GOAPRunState.Runing;
        berry = (BerryController)ripeBerryState.value;
        RoleMoveState moveState = role.stateMachine.ChangeState<RoleMoveState>();
        moveState.SetDestination(berry.transform.position);
        moveState.SetCallback(OnMoveToBerry, 1.5f);
        runState = GOAPRunState.Runing;
    }

    private void OnMoveToBerry()
    {
        // 2.采摘
        ripeBerryState.value = null; // 离开了这个成熟浆果
        RolePerformState performState = role.stateMachine.ChangeState<RolePerformState>();
        performState.PlayAnimation("Work", 1f, OnWorked);
    }

    private void OnWorked()
    {
        // 3.开始回家
        RoleMoveState moveState = role.stateMachine.ChangeState<RoleMoveState>();
        moveState.SetDestination(MapManager.Instance.CampfirePoint.position);
        moveState.SetCallback(OnMoveToHome, 1.5f);
        berry.OnPick();
        berry = null;
    }

    private void OnMoveToHome()
    {
        // 4.到家
        role.stateMachine.ChangeState<RoleIdleState>();
        MapManager.Instance.AddFood(1);
        runState = GOAPRunState.Succeed;
    }

    public override GOAPRunState OnUpdate()
    {
        return runState;
    }

    public override void OnStop()
    {
        berry = null;
    }
}
