using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GOAPStates
{
    public Dictionary<string, GOAPStateBase> stateDic = new Dictionary<string, GOAPStateBase>();
    public bool TryAddState(GOAPStateType type, GOAPStateBase state)
    {
        return stateDic.TryAdd(type, state);
    }

    public bool TryRemove(GOAPStateType type)
    {
        return stateDic.Remove(type);
    }

    public T GetStateBase<T>(GOAPStateType type) where T : GOAPStateBase
    {
        return (T)stateDic[type];
    }
    public bool TryGetState<T>(GOAPStateType type, out T state) where T : GOAPStateBase
    {
        if (stateDic.TryGetValue(type, out GOAPStateBase tempState))
        {
            state = (T)tempState;
            return true;
        }
        else
        {
            state = default;
            return false;
        }
    }
    public bool CheckStateForPrecondition(GOAPStateType type, GOAPStateComparer comparer)
    {
        if (TryGetState(type, out GOAPStateBase state))
        {
            return state.CompreForPrecondition(comparer);
        }
        return false;
    }
    public bool CheckStateForEffect(GOAPStateType type, GOAPStateComparer comparer)
    {
        if (TryGetState(type, out GOAPStateBase state))
        {
            return state.CompreForEffect(comparer);
        }
        return false;
    }
    public void ApplyEffect(GOAPTypeAndComparer effect)
    {
        if (stateDic.TryGetValue(effect.stateType, out GOAPStateBase value))
        {
            value.ApplyEffect(effect.stateComparer);
        }
    }

#if UNITY_EDITOR
    [Button]
    private void CheckStates()
    {
        List<string> createTypeList = new List<string>();
        foreach (KeyValuePair<string, GOAPStateBase> item in stateDic)
        {
            // 类型错误
            if (item.Value == null || item.Value.GetType() != GOAPGlobalConfig.GetStateValueType(item.Key))
            {
                createTypeList.Add(item.Key);
            }
        }
        foreach (string item in createTypeList)
        {
            stateDic[item] = GOAPGlobalConfig.CopyState(item);
        }
    }
#endif
}
