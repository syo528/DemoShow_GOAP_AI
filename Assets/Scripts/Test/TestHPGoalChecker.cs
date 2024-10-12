using UnityEngine;

public class TestHPGoalChecker : IGOAPGoalChecker
{
    public void Update(GOAPGoals.Goal item, GOAPAgent agent, IGOAPOwner owner)
    {
        item.runtimePiority += Time.deltaTime;
    }
}

