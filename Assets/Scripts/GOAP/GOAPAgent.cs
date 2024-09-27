using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
public class GOAPAgent : SerializedMonoBehaviour
{
    [LabelText("目标")] public GOAPGoals goals;
    [LabelText("局部状态")] public GOAPStates states;
    public IGOAPOwner owner { get; private set; }
    public void Init(IGOAPOwner owner)
    {
        goals.Init(this, owner);
    }
    public void OnUpdate()
    {
        Debug.Log(goals.UpdateGoals().First().Key);
    }
}
