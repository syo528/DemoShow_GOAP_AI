public class RoleStateBase : FSMStateBase
{
    protected RoleController role;
    public override void Init(IStateMachineOwner owner)
    {
        role = (RoleController)owner;
    }
}
