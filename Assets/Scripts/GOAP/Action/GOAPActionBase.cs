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
            if (!agent.states.CheckState(item.stateType, item.stateComparer))
            {
                return false;
            }
        }
        return true;
    }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnStop() { }
    public virtual void OnDestroy() { }

    public void ApplyEffect()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            GOAPTypeAndComparer effect = effects[i];
            if (GOAPGlobalConfig.IsGlobalState(effect.stateType))
            {
                GOAPGlobal.instance.GlobalStates.ApplyEffect(effect);
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
    public GOAPStateType stateType;
    public GOAPStateComparer stateComparer;
}
