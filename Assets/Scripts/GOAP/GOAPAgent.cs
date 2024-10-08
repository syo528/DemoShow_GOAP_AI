using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
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
        actions.Init(this, owner);
        goals.Init(this, owner);
    }
    public void OnUpdate()
    {
        if (owner == null) return;
        // TODO:计划在执行就不需要去构建任务了
        SortedList<string, GOAPGoals.Item> sortedGoals = goals.UpdateGoals();
        foreach (KeyValuePair<string, GOAPGoals.Item> item in sortedGoals)
        {
            // 优先级不是负数，同时可以基于这个目标生成计划
            if (item.Value.piority > 0 && GeneratePlan(item.Key))
            {
                Debug.Log("任务构建成功" + item.Key);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        plan.OnDestroy();
    }

    public void ApplyEffect(GOAPTypeAndComparer effect)
    {
        states.ApplyEffect(effect);
    }

    public bool CheckState(GOAPStateType stateType, GOAPStateComparer stateComparer)
    {
        if (GOAPGlobalConfig.IsGlobalState(stateType))
        {
            return GOAPGlobal.instance.GlobalStates.CheckState(stateType, stateComparer);
        }
        else
        {
            return states.CheckState(stateType, stateComparer);
        }
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

    private void RecycleNodeSortedSet(SortedSet<GOAPPlanNode> nodes)
    {
        foreach (GOAPPlanNode item in nodes)
        {
            item.Destroy();
        }
        nodes.Clear();
        GOAPObjectPool.Recycle(nodes);
    }


    // 找到符合某个效果的所有行为并形成计划节点
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

    // 基于一个源头构建计划路径
    // 失败的可能性：某个环节中无法达成某个前置条件
    private bool TryBuildPlanPath(GOAPPlanNode startNode)
    {
        // 遍历所有条件，必须全部满足才可以进行构建成功
        foreach (GOAPTypeAndComparer pre in startNode.action.preconditions)
        {
            // 当前状态的满足情况
            bool check = CheckState(pre.stateType, pre.stateComparer);
            if (!check) // 当前状态不满足，需要寻找可以满足的其他Action作为子节点
            {
                SortedSet<GOAPPlanNode> preNodes = GetPlanNodesByeEffectStateType(pre.stateType, pre.stateComparer);
                GOAPPlanNode targetNode = null;

                foreach (GOAPPlanNode preItemNode in preNodes)
                {
                    if (preItemNode != startNode && TryBuildPlanPath(preItemNode)) // preItemNode!=startNode:避免自己是是自己的前提
                    {
                        targetNode = preItemNode;
                        preItemNode.parent = startNode;
                        preItemNode.indexAtParent = startNode.preconditions.Count;
                        startNode.preconditions.Add(preItemNode);
                        check = true;
                        break;
                    }
                }
                if (targetNode != null) preNodes.Remove(targetNode);
                RecycleNodeSortedSet(preNodes);
                if (!check) // 意味着当前无法满足条件
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool GeneratePlan(string goalName)
    {
        bool success = false;
        GOAPGoals.Item goas = goals.dic[goalName];
        GOAPStateType targetStateType = goas.targetState;
        // 获取符合效果的全部Action以此尝试构建计划，成功的作为初始Action
        SortedSet<GOAPPlanNode> nodes = GetPlanNodesByeEffectStateType(targetStateType, goas.targetValue);
        GOAPPlanNode targetNode = null;
        foreach (GOAPPlanNode node in nodes)
        {
            if (TryBuildPlanPath(node))
            {
                targetNode = node;
                node.parent = null;
                node.indexAtParent = 0;
                plan.goalName = goalName;
                plan.SetStartNode(node);
                success = true;
                break;
            }
        }
        if (targetNode != null) nodes.Remove(targetNode);
        RecycleNodeSortedSet(nodes);
        return success;
    }

    #endregion
}
