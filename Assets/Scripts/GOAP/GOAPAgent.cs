using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
public class GOAPAgent : SerializedMonoBehaviour
{
    [LabelText("目标")] public GOAPGoals goals;
    [LabelText("局部状态")] public GOAPStates states;

    private void Update()
    {
        goals.UpdateGoals();
    }
}
