public class RoleMoveToRipeBerryAction : RoleActionBase
{
    private GOAPRunState runState;
    private BerryController berry;
    public override void OnStart()
    {
        // 占有一个成熟浆果，避免大家一起去采摘同一棵
        berry = MapManager.Instance.RoleTryGetRipeBerry();
        // 前往浆果
        RoleMoveState moveState = role.stateMachine.ChangeState<RoleMoveState>();
        moveState.SetDestination(berry.transform.position);
        moveState.SetCallback(OnMoveEnd, 1.5f);
        runState = GOAPRunState.Runing;
    }

    private void OnMoveEnd()
    {
        if (berry.IsRipe)
        {
            agent.states.GetState<UnityObjectState>("成熟浆果").SetValue(berry);
        }
        berry = null;
        runState = GOAPRunState.Succeed;
    }

    public override GOAPRunState OnUpdate()
    {
        return runState;
    }
    public override void OnStop()
    {
        if (berry != null)
        {
            berry.CheckRipeState();
            berry = null;
        }
    }
    public override void OnDestroy()
    {
        OnStop();
    }
}
