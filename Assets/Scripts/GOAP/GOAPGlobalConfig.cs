using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Config/GOAP/GOAPGlobalConfig")]
public class GOAPGlobalConfig : SerializedScriptableObject
{
    public class GOAPStateConfigItem
    {
        public GOAPStateBase state;
        public bool isGlobal;
    }
    public Dictionary<GOAPStateType, GOAPStateConfigItem> stateConfigDic = new Dictionary<GOAPStateType, GOAPStateConfigItem>();
    public static GOAPGlobalConfig instance { get; private set; }
    private void OnEnable()
    {
        instance = this;
    }
    public static GOAPStateBase CopyState(GOAPStateType stateType)
    {
        return instance.stateConfigDic[stateType].state.Copy();
    }

    public static bool IsGlobalState(GOAPStateType stateType)
    {
        return instance.stateConfigDic[stateType].isGlobal;
    }

    public static Type GetStateValueType(GOAPStateType stateType)
    {
        return instance.stateConfigDic[stateType].GetType();
    }
}

