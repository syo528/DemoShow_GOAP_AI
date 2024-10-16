﻿using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GOAPGoals
{
    public class Goal
    {
        [LabelText("目标状态"), OnValueChanged("CheckState")] public GOAPStateType targetState;
        [LabelText("目标趋势")] public GOAPStateComparer targetValue;
        [LabelText("优先级系数"), HorizontalGroup("1")] public float priorityMultiply;
        [LabelText("实时优先级"), HorizontalGroup("1")] public float runtimePiority;
        [LabelText("可以中断其他目标")] public bool canBreak;
        [LabelText("可以被其他目标中断")] public bool canBeBreak;
        [LabelText("最终优先级"), ShowInInspector, ReadOnly, HorizontalGroup("1")] public float piority => priorityMultiply * runtimePiority;
        [LabelText("目标检查器")] public IGOAPGoalChecker checker;
#if UNITY_EDITOR
        public void CheckState()
        {
            if (GOAPEditorUtility.global != null && GOAPEditorUtility.global.TryGetGlobalState(targetState, out GOAPStateBase state)
                && (targetValue == null || targetValue.GetType() != state.GetGetComparerType()))
            {
                targetValue = state.GetComparer();
            }
            else if (GOAPEditorUtility.agent != null && GOAPEditorUtility.agent.states.TryGetState(targetState, out state)
                      && (targetValue == null || targetValue.GetType() != state.GetGetComparerType()))
            {
                targetValue = state.GetComparer();
            }
        }
#endif
    }

    private class SortedGoalComparer : IComparer<string>
    {
        public Dictionary<string, Goal> dic;

        public SortedGoalComparer(Dictionary<string, Goal> dic)
        {
            this.dic = dic;
        }

        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            int com = dic[y].piority.CompareTo(dic[x].piority);
            if (com == 0) return -1; // 同名同优先级被去重了
            else return com;
        }
    }

    public Dictionary<string, Goal> dic = new Dictionary<string, Goal>();
    private SortedList<string, Goal> sortedList;

    private GOAPAgent agent;
    private IGOAPOwner owner;
    public void Init(GOAPAgent agent, IGOAPOwner owner)
    {
        this.agent = agent;
        this.owner = owner;
        sortedList = new SortedList<string, Goal>(dic.Count, new SortedGoalComparer(dic));
    }

    public SortedList<string, Goal> UpdateGoals()
    {
        if (dic == null) return null;
        sortedList.Clear();
        foreach (KeyValuePair<string, Goal> item in dic)
        {
            if (item.Value.checker != null)
            {
                item.Value.checker.Update(item.Value, agent, owner);
            }
            sortedList.Add(item.Key, item.Value);
        }
        return sortedList;
    }

#if UNITY_EDITOR
    [Button("检查目标状态类型")]
    public void CheckGoalsTargetValueType()
    {
        foreach (KeyValuePair<string, Goal> item in dic)
        {
            item.Value.CheckState();
        }
    }
#endif
}
