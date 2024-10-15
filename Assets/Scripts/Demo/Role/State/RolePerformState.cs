using System;
using UnityEngine;
public class RolePerformState : RoleStateBase
{
    private string animationName;
    private Action onEnd;
    private float endTime;
    private float endTimer;
    public void PlayAnimation(string animationName, float endTime, Action onEndAction)
    {
        this.animationName = animationName;
        this.onEnd = onEndAction;
        this.endTime = endTime;
        role.PlayAniamtion(animationName);
        endTimer = 0;
    }
    public override void OnUpdate()
    {
        endTimer += Time.deltaTime;
        if (endTimer > endTime)
        {
            onEnd?.Invoke();
            onEnd = null;
        }
    }
    public override void OnExit()
    {
        onEnd = null;
    }
}
