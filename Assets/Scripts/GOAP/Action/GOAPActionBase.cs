using Sirenix.OdinInspector;
using System.Collections.Generic;

public abstract class GOAPActionBase
{
    [LabelText("前提")] public List<GOAPTypeAndComparer> preconditions = new List<GOAPTypeAndComparer>();
    [LabelText("效果")] public List<GOAPTypeAndComparer> effects = new List<GOAPTypeAndComparer>();
    [LabelText("代价值")] public float costValue;
    [LabelText("效果值")] public float effectValue;
    [LabelText("优先级"), ReadOnly] public virtual float priority => effectValue - costValue;
    protected GOAPAgent agent;
    public virtual void Init(GOAPAgent agent, IGOAPOwner owner)
    {
        this.agent = agent;
    }

    public virtual bool CheckPrecondition()
    {
        foreach (GOAPTypeAndComparer item in preconditions)
        {
            if (!agent.CheckStateForPrecondition(item.stateType, item.stateComparer))
            {
                return false;
            }
        }
        return true;
    }
    public virtual bool CheckEffect()
    {
        foreach (GOAPTypeAndComparer item in effects)
        {
            if (!agent.CheckStateForEffect(item.stateType, item.stateComparer))
            {
                return false;
            }
        }
        return true;
    }

    public virtual GOAPRunState StartRun()
    {
        if (CheckEffect())
        {
            return GOAPRunState.Succeed;
        }
        else if (CheckPrecondition())
        {
            OnStart();
            return GOAPRunState.Runing;
        }
        else
        {
            return GOAPRunState.Failed;
        }
    }
    public virtual void OnStart() { }
    public virtual GOAPRunState OnUpdate() { return default; }
    public virtual void OnStop() { }
    public virtual void OnDestroy() { }

    public void ApplyEffect()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            GOAPTypeAndComparer effect = effects[i];
            if (GOAPGlobal.instance.TryGetGlobalState(effect.stateType, out GOAPStateBase state))
            {
                state.ApplyEffect(effect.stateComparer);
            }
            else
            {
                agent.ApplyEffect(effect);
            }
        }
    }

    public virtual void UpadatePriority() { }

}
public class GOAPTypeAndComparer
{
    [OnValueChanged("CheckState")] public GOAPStateType stateType;
    public GOAPStateComparer stateComparer;
#if UNITY_EDITOR
    public void CheckState()
    {
        if (GOAPEditorUtility.global != null && GOAPEditorUtility.global.TryGetGlobalState(stateType, out GOAPStateBase state)
            && (stateComparer == null || stateComparer.GetType() != state.GetGetComparerType()))
        {
            stateComparer = state.GetComparer();
        }
        else if (GOAPEditorUtility.agent != null && GOAPEditorUtility.agent.states.TryGetState(stateType, out state)
                 && (stateComparer == null || stateComparer.GetType() != state.GetGetComparerType()))
        {
            stateComparer = state.GetComparer();
        }
    }
#endif
}
