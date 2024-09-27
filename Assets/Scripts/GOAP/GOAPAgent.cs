using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
public class GOAPAgent : SerializedMonoBehaviour
{
    [LabelText("目标")] public GOAPGoals goals;
    [LabelText("局部状态")] public GOAPStates states;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        goals.Init();
    }
    private void Update()
    {
        Debug.Log(goals.UpdateGoals().First().Key);
    }
}
