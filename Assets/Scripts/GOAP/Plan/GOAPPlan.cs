using System.Collections.Generic;

public class GOAPPlan
{
    public GOAPPlanNode startNode; // 最终完成目标效果的节点
    public GOAPPlanNode runingNode;// 运行中的节点
    public string goalName;         // 目标
}

public class GOAPPlanNode
{
    public GOAPActionBase action;    // 自身acion
    public GOAPPlanNode parent;      // 父节点
    public List<GOAPPlanNode> preconditions = new List<GOAPPlanNode>();   // 前置节点，其实就是子节点
    public int indexAtParent;       // 自身是父节点的第几个
    public void Destroy()
    {
        if (action == null) return;
        action = null;
        parent?.Destroy();
        parent = null;
        foreach (GOAPPlanNode item in preconditions)
        {
            item.Destroy();
        }
        preconditions.Clear();
        GOAPObjectPool.Recycle(this);
    }
}