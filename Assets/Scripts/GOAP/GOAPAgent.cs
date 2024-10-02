using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class GOAPAgent : SerializedMonoBehaviour
{
    [LabelText("目标")] public GOAPGoals goals;
    [LabelText("局部状态")] public GOAPStates states;
    [LabelText("全部行为")] public GOAPActions actions;
    [LabelText("计划")] public GOAPPlan plan;

    public IGOAPOwner owner { get; private set; }
    public void Init(IGOAPOwner owner)
    {
        this.owner = owner;
        goals.Init(this, owner);
        actions.Init(this, owner);
    }
    public void OnUpdate()
    {
        Debug.Log(goals.UpdateGoals().First().Key);
    }

    public void ApplyEffect(GOAPTypeAndComparer effect)
    {
        states.ApplyEffect(effect);
    }

    #region 生成计划
    private class PlanNodePriorityComparer : IComparer<GOAPPlanNode>
    {
        public int Compare(GOAPPlanNode x, GOAPPlanNode y)
        {
            return y.action.priority.CompareTo(x.action.priority);
        }
    }

    private SortedSet<GOAPPlanNode> GetNodeSortedSet()
    {
        SortedSet<GOAPPlanNode> nodes = GOAPObjectPool.Get<SortedSet<GOAPPlanNode>>();
        if (nodes == null) nodes = new SortedSet<GOAPPlanNode>(new PlanNodePriorityComparer());
        return nodes;
    }

    private SortedSet<GOAPPlanNode> GetPlanNodesByeEffectStateType(GOAPStateType targetStateType, GOAPStateComparer comparer)
    {
        SortedSet<GOAPPlanNode> stateTypeNodes = GetNodeSortedSet();
        if (actions.actionEffectDic.TryGetValue(targetStateType, out List<GOAPActionBase> actionList))
        {
            foreach (GOAPActionBase action in actionList)
            {
                foreach (GOAPTypeAndComparer effect in action.effects)
                {
                    if (effect.stateType == targetStateType && effect.stateComparer.EqualsComparer(comparer))
                    {
                        action.UpadatePriority();
                        GOAPPlanNode node = GOAPObjectPool.GetOrNew<GOAPPlanNode>();
                        node.action = action;
                        stateTypeNodes.Add(node);
                        break;
                    }
                }
            }
        }
        return stateTypeNodes;
    }


    #endregion
}
