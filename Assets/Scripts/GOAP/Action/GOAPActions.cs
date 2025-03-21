﻿using Sirenix.OdinInspector;
using System.Collections.Generic;
public class GOAPActions
{
    public List<GOAPActionBase> actions = new List<GOAPActionBase>();
    // Value是可以满足GOAPStateType的行为列表
    public Dictionary<GOAPStateType, List<GOAPActionBase>> actionEffectDic { get; set; }
    public void Init(GOAPAgent agent, IGOAPOwner owner)
    {
        actionEffectDic = new Dictionary<GOAPStateType, List<GOAPActionBase>>();
        foreach (GOAPActionBase action in actions)
        {
            action.Init(agent, owner);
            foreach (GOAPTypeAndComparer effect in action.effects)
            {
                AddActionEffect(effect.stateType, action);
            }
        }
    }

    private void AddActionEffect(GOAPStateType stateType, GOAPActionBase action)
    {
        if (!actionEffectDic.TryGetValue(stateType, out List<GOAPActionBase> actions))
        {
            actions = new List<GOAPActionBase>();
            actionEffectDic.Add(stateType, actions);
        }
        actions.Add(action);
    }
#if UNITY_EDITOR
    [Button("检查所有行为状态类型")]
    public void CheckAllActionState()
    {
        foreach (GOAPActionBase item in actions)
        {
            foreach (GOAPTypeAndComparer pre in item.preconditions)
            {
                pre.CheckState();
            }
            foreach (GOAPTypeAndComparer effect in item.effects)
            {
                effect.CheckState();
            }
        }
    }
#endif
}
