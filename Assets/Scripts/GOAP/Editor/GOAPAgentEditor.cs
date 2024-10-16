using Sirenix.OdinInspector.Editor;
using UnityEditor;
[CustomEditor(typeof(GOAPAgent))]
public class GOAPAgentEditor : OdinEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GOAPEditorUtility.agent = (GOAPAgent)target;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GOAPEditorUtility.agent = null;
    }
}
