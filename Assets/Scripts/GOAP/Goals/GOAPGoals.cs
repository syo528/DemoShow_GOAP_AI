using Sirenix.OdinInspector;
using System.Collections.Generic;


public class GOAPGoals
{
    public class Item
    {
        [LabelText("目标状态")] public GOAPStateType targetState;
        [LabelText("目标趋势")] public GOAPStateComparer targetValue;
        [LabelText("优先级系数"), HorizontalGroup("1")] public float priorityMultiply;
        [LabelText("实时优先级"), HorizontalGroup("1"), ReadOnly] public float runtimePiority;
    }

    public Dictionary<string, Item> dic = new Dictionary<string, Item>();

    public void Init()
    {

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
