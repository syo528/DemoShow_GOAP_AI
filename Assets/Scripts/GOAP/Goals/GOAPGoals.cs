using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GOAPGoals
{
    public class Item
    {
        [LabelText("目标状态")] public GOAPStateType targetState;
        [LabelText("目标趋势")] public GOAPStateComparer targetValue;
        [LabelText("优先级系数"), HorizontalGroup("1")] public float priorityMultiply;
        [LabelText("实时优先级"), HorizontalGroup("1")] public float runtimePiority;
        [LabelText("最终优先级"), ShowInInspector, ReadOnly, HorizontalGroup("1")] public float piority => priorityMultiply * runtimePiority;
        [LabelText("目标检查器")] public IGOAPGoalChecker checker;
    }

    private class SortedGoalComparer : IComparer<string>
    {
        public Dictionary<string, Item> dic;

        public SortedGoalComparer(Dictionary<string, Item> dic)
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

    public Dictionary<string, Item> dic = new Dictionary<string, Item>();
    private SortedList<string, Item> sortedList;
    public void Init()
    {
        sortedList = new SortedList<string, Item>(dic.Count, new SortedGoalComparer(dic));
    }

    public SortedList<string, Item> UpdateGoals()
    {
        if (dic == null) return null;
        sortedList.Clear();
        foreach (KeyValuePair<string, Item> item in dic)
        {
            if (item.Value.checker != null)
            {
                item.Value.checker.Update(item.Value);
            }
            sortedList.Add(item.Key, item.Value);
        }
        return sortedList;
    }

#if UNITY_EDITOR
    [Button("检查目标状态类型")]
    public void CheckGoalsTargetValueType()
    {
        List<string> createList = new List<string>();
        foreach (KeyValuePair<string, Item> item in dic)
        {
            if (item.Value == null || item.Value.targetValue == null
                || item.Value.targetValue.GetType() != GOAPGlobalConfig.GetStateValueType(item.Value.targetState))
            {
                createList.Add(item.Key);
            }
        }
        foreach (string goalName in createList)
        {
            Item item = dic[goalName];
            if (item == null) continue;
            item.targetValue = GOAPGlobalConfig.CopyState(item.targetState).GetComparer();
        }
    }
#endif
}
