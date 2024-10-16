#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class GOAPEditorUtility
{
    public static GOAPAgent agent;
    public static GOAPGlobal global { get; private set; }

    [InitializeOnLoadMethod]
    public static void Init()
    {
        TryGetGlobal();
    }
    private static void TryGetGlobal()
    {
        if (global == null) global = GameObject.FindAnyObjectByType<GOAPGlobal>();
    }
}
#endif
