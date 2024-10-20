public class RoleHPUpGoalChecker : IGOAPGoalChecker
{
    public void Update(GOAPGoals.Goal item, GOAPAgent agent, IGOAPOwner owner)
    {
        RoleController role = (RoleController)owner;
        float current = agent.states.GetState<FloatState>("饥饿值").value;
        float critical = RoleController.maxHP - RoleController.foodEffect;
        if (current > critical) item.runtimePiority = 0;
        else item.runtimePiority = (1 - (current / critical));
    }
}