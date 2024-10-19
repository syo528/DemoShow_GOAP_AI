using UnityEditor;
using UnityEngine;


public class GOAPPlanWindow : EditorWindow
{
    [MenuItem("GOAP/GOAPPlanWindow")]
    static void OpenWindow()
    {
        GetWindow<GOAPPlanWindow>();
    }

    private GOAPPlan plan;
    private Vector2 scrollPosition;
    private void OnGUI()
    {
        //Test();
        //return;
        if (Selection.gameObjects.Length == 0) return;
        GameObject go = Selection.gameObjects[0];
        if (go == null) return;
        GOAPAgent agent = go.GetComponent<GOAPAgent>();
        if (agent == null) return;
        plan = agent.plan;
        if (plan == null || plan.startNode == null || plan.goalName == null) return;
        EditorGUILayout.LabelField($"计划:{plan.goalName}");
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GOAPPlanNode startNode = plan.startNode;
        Color oldColor = GUI.color;
        PrintNode(startNode);
        GUI.color = oldColor;
        GUILayout.EndScrollView();
    }

    #region 测试

    private void Test()
    {
        plan = new GOAPPlan();
        plan.goalName = "测试计划";
        plan.startNode = new GOAPPlanNode() { action = new TestAction { } };
        CreateTestPlanData(plan.startNode, 3, 3, 0);
        EditorGUILayout.LabelField($"计划:{plan.goalName}");
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GOAPPlanNode startNode = plan.startNode;
        Color oldColor = GUI.color;
        PrintNode(startNode);
        GUI.color = oldColor;
        GUILayout.EndScrollView();
    }

    private void CreateTestPlanData(GOAPPlanNode node, int length, int maxDepth, int currentDepth = 0)
    {
        if (currentDepth == maxDepth) return;
        node.preconditions = new System.Collections.Generic.List<GOAPPlanNode>(length);
        for (int i = 0; i < length; i++)
        {
            GOAPPlanNode tempNode = new GOAPPlanNode() { action = new TestAction { } };
            node.preconditions.Add(tempNode);
            CreateTestPlanData(tempNode, length, maxDepth, currentDepth + 1);
        }
    }
    #endregion


    private void PrintNode(GOAPPlanNode node, int depth = 0)
    {
        string prefix = new string(' ', depth * 6);
        string nodeName = $"{prefix}{node.action.GetType().Name}";
        GUI.color = plan.runingNode == node ? Color.red : Color.yellow;
        EditorGUILayout.LabelField(nodeName);
        for (int i = 0; i < node.preconditions.Count; i++)
        {
            PrintNode(node.preconditions[i], depth + 1);
        }
    }
}
