using System;
using System.Collections.Generic;

public class StateMachine
{
    private IStateMachineOwner owner;
    private Dictionary<Type, FSMStateBase> stateDic = new Dictionary<Type, FSMStateBase>();
    private FSMStateBase currentState;
    public bool haveState { get => currentState != null; }
    public Type currentStateType { get => currentState.GetType(); }
    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }

    // 切换状态
    public T ChangeState<T>(bool reCurrentState = false) where T : FSMStateBase, new()
    {
        // 状态一致，并且不需要刷新状态 则不需要进行切换
        if (haveState && currentStateType == typeof(T) && !reCurrentState) return (T)currentState;
        // 退出当前状态
        if (currentState != null)
        {
            currentState.OnExit();
        }
        // 进入新状态
        currentState = GetState<T>();
        currentState.OnEnter();
        return (T)currentState;
    }

    // 获取状态
    public FSMStateBase GetState<T>() where T : FSMStateBase, new()
    {
        Type type = typeof(T);
        // 缓存字段那种如果不存在则实例化一个新的
        if (!stateDic.TryGetValue(type, out FSMStateBase state))
        {
            state = new T();
            state.Init(owner);
            stateDic.Add(type, state);
        }
        return state;
    }


    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void Stop()
    {
        currentState?.OnExit();
        currentState = null;
        foreach (FSMStateBase item in stateDic.Values)
        {
            item.UInit();
        }
        stateDic.Clear();
    }

}
