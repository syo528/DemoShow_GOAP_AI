using System.Collections.Generic;

public class GOAPPlan
{
    public GOAPPlaneNode startNode; // 最终完成目标效果的节点
    public GOAPPlaneNode runingNode;// 运行中的节点
    public string goalName;         // 目标
}

public class GOAPPlaneNode
{
    public GOAPActionBase action;   // 自身acion
    public GOAPPlaneNode parent;    // 父节点
    public List<GOAPPlaneNode> preconditions;   // 前置节点，其实就是子节点
}