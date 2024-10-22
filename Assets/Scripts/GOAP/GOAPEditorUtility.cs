#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
public static class GOAPEditorUtility
{
    public static GOAPAgent agent;
    public static GOAPGlobal global { get; private set; }

    [InitializeOnLoadMethod]
    public static void Init()
    {
        TryGetGlobal();
        EditorSceneManager.sceneOpened += EditorSceneManager_sceneOpened;
    }
    private static void EditorSceneManager_sceneOpened(Scene scene, OpenSceneMode mode)
    {
        GetGlobal();
    }
    private static void TryGetGlobal()
    {
        if (global == null) GetGlobal();
    }
    private static void GetGlobal()
    {
        global = GameObject.FindAnyObjectByType<GOAPGlobal>();
    }
}
#endif
