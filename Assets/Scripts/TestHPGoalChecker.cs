using UnityEngine;

public class TestHPGoalChecker : IGOAPGoalChecker
{
    public void Update(GOAPGoals.Item item)
    {
        item.runtimePiority += Time.deltaTime;
    }
}

