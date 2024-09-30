using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;
public class GOAPAgent : SerializedMonoBehaviour
{
    [LabelText("目标")] public GOAPGoals goals;
    [LabelText("局部状态")] public GOAPStates states;
    [LabelText("全部行为")] public GOAPActions actions;
    [LabelText("计划")] public GOAPPlan plan;

    public IGOAPOwner owner { get; private set; }
    public void Init(IGOAPOwner owner)
    {
        goals.Init(this, owner);
    }
    public void OnUpdate()
    {
        Debug.Log(goals.UpdateGoals().First().Key);
    }

    public void ApplyEffect(GOAPTypeAndComparer effect)
    {
        states.ApplyEffect(effect);
    }
}
