using UnityEngine;
using UnityEngine.AI;

public class RoleController : MonoBehaviour, IStateMachineOwner, IGOAPOwner
{
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public float hpDownSpeed = 1;
    public GOAPAgent goapAgent;
    public StateMachine stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        goapAgent.Init(this);
    }

    public void PlayAniamtion(string animationName)
    {
        animator.CrossFadeInFixedTime(animationName, 0.25f);
    }
    public void StartMove()
    {
        navMeshAgent.enabled = true;
    }
    public void StopMove()
    {
        navMeshAgent.enabled = false;
    }
    public void SetDestination(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

}
