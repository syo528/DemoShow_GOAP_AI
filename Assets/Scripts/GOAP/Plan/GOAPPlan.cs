using System.Collections.Generic;

public class GOAPPlan
{
    public GOAPPlanNode startNode; // 最终完成目标效果的节点
    public GOAPPlanNode runingNode;// 运行中的节点
    public string goalName;         // 目标
}

public class GOAPPlanNode
{
    public GOAPActionBase action;   // 自身acion
    public GOAPPlanNode parent;    // 父节点
    public List<GOAPPlanNode> preconditions;   // 前置节点，其实就是子节点
}