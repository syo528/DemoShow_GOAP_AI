using UnityEngine;
// 食物和人口的比列关系，食物越少权重越高
public class RoleReserveFoodGoalCheckr : IGOAPGoalChecker
{
    public float maxPriority = 3;
    public void Update(GOAPGoals.Goal item, GOAPAgent agent, IGOAPOwner owner)
    {
        int foodCount = MapManager.Instance.ReserveFoodCount;
        if (foodCount == 0)
        {
            item.runtimePiority = maxPriority;
            return;
        }
        float piority = MapManager.Instance.RoleCount / foodCount;
        item.runtimePiority = Mathf.Clamp(piority, 0, maxPriority);
    }
}
