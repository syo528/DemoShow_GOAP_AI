using UnityEngine;
public class RoleGrowBerryAction : RoleActionBase
{
    private Vector2Int berryCoord;
    private GOAPRunState runState;
    public override void OnStart()
    {
        // 1.移动到种植位置
        berryCoord = MapManager.Instance.GetNextBuildCoord();
        Vector3 berryWorldPos = MapManager.Instance.GetCellPosition(berryCoord);
        RoleMoveState moveState = role.stateMachine.ChangeState<RoleMoveState>();
        moveState.SetDestination(berryWorldPos);
        moveState.SetCallback(OnMoveEnd, 1.5f);
        runState = GOAPRunState.Runing;
    }
    public override GOAPRunState OnUpdate()
    {
        return runState;
    }
    private void OnMoveEnd()
    {
        // 2.演出动画
        RolePerformState performState = role.stateMachine.ChangeState<RolePerformState>();
        performState.PlayAnimation("Work", 0.95f, OnPerformEnd);
    }
    private void OnPerformEnd()
    {
        // 3.处于成熟浆果边
        role.stateMachine.ChangeState<RoleIdleState>();
        runState = GOAPRunState.Succeed;
        BerryController berry = MapManager.Instance.SpawnBerry(berryCoord);
        if (berry.IsRipe)
        {
            agent.states.GetState<UnityObjectState>("成熟浆果").SetValue(berry);
        }
    }
}
