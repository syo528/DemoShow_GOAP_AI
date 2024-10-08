public class GOAPPlan
{
    public GOAPPlanNode startNode; // 最终完成目标效果的节点
    public GOAPPlanNode runingNode;// 运行中的节点
    public string goalName;         // 目标

    public void SetStartNode(GOAPPlanNode startNode)
    {
        this.startNode = startNode;
    }

    public void StartRun()
    {
        // TODO:找到整个树结构最下层的节点
        runingNode = startNode;
        runingNode.Start();
    }
}
