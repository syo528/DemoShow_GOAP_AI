using UnityEngine;

public class TestHPGoalChecker : IGOAPGoalChecker
{
    public void Update(GOAPGoals.Item item, GOAPAgent agent, IGOAPOwner owner)
    {
        item.runtimePiority += Time.deltaTime;
    }
}

