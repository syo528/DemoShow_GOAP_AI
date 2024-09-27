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
}
