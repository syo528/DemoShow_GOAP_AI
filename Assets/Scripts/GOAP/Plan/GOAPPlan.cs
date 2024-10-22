using Sirenix.OdinInspector;
using UnityEngine;

public class GOAPPlan
{
    public GOAPPlanNode startNode; // 最终完成目标效果的节点
    public GOAPPlanNode runingNode;// 运行中的节点
    public string goalName;         // 目标
    [ShowInInspector, ReadOnly] public bool runing { get; private set; }
    public GOAPPlanNode stageNode => runingNode.parent;
    public int runingNodeChildIndex => runingNode.indexAtParent;

    public void StartRun(string goalName, GOAPPlanNode targetNode)
    {
        this.goalName = goalName;
        this.startNode = targetNode;
        // 找到整个树结构最下层的节点
        StartRunNode(GetDeepestNode(startNode));
    }

    public void Stop()
    {
        RecycleNodes(startNode);
        startNode = null;
        runing = false;
        goalName = null;
    }

    private GOAPPlanNode GetDeepestNode(GOAPPlanNode startNode)
    {
        if (startNode.preconditions.Count == 0) return startNode;
        GOAPPlanNode tempNode = startNode.preconditions[0];
        return GetDeepestNode(tempNode);
    }
    public void OnUpdate()
    {
        GOAPRunState nodeState = runingNode.Update();
        if (nodeState == GOAPRunState.Succeed) // 执行下一个
        {
            runingNode.Stop();
            // 如果完成的是startNode，代表计划完成
            if (runingNode == startNode)
            {
                Debug.Log("任务全部完成");
                Stop();
                return;
            }
            // 有同层可以执行则运行同层的下一个节点
            if (runingNodeChildIndex < stageNode.preconditions.Count - 1)
            {
                StartRunNode(stageNode.preconditions[runingNodeChildIndex + 1]);
            }
            // 不存在同层下一个节点，则运行父节点
            else
            {
                StartRunNode(stageNode);
            }
        }
        else if (nodeState == GOAPRunState.Failed)
        {
            Stop();
        }
        // 执行中就不用处理
    }

    private void RecycleNodes(GOAPPlanNode node)
    {
        if (node != null)
        {
            foreach (GOAPPlanNode item in node.preconditions)
            {
                RecycleNodes(item);
            }
            node.action = null;
            node.parent = null;
            node.indexAtParent = 0;
            node.preconditions.Clear();
        }
    }

    private void StartRunNode(GOAPPlanNode node)
    {
        runingNode = node;
        runing = runingNode.Start() == GOAPRunState.Runing;
        if (!runing)
        {
            RecycleNodes(startNode);
        }
    }

    public void OnDestroy()
    {
        if (runingNode != null)
        {
            runingNode.action?.OnDestroy();
        }
        if (startNode != null)
        {
            RecycleNodes(startNode);
        }
    }
}
