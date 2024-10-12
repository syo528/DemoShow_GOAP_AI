/// <summary>
/// 状态机基类
/// </summary>
public abstract class FSMStateBase
{
    public virtual void Init(IStateMachineOwner owner) { }
    public virtual void UInit() { }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
