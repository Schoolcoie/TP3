using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningPlayerState : PlayerState
{
    private Animator m_PlayerAnimator;
    public WinningPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        m_PlayerAnimator = stateMachine.GetComponent<Animator>();

        stateMachine.StartCoroutine(WinRoutine(2));
    }

    public override void ExecuteUpdate()
    {
    }

    public override void ExecuteFixedUpdate()
    {
    }

    public override void ExecuteOnCollisionEnter(Collision collision)
    {
    }
    public override void ExecuteOnCollisionExit(Collision collision)
    {
    }

    public override void ExecuteOnTriggerEnter(Collider other)
    {
    }

    public override void ExecuteOnTriggerExit(Collider other)
    {
    }

    private IEnumerator WinRoutine(float seconds)
    {
        m_PlayerAnimator.SetBool("IsWinning", true);
        yield return new WaitForSeconds(seconds);
        m_PlayerAnimator.SetBool("IsWinning", false);
        EventManager.TriggerEvent("Reset");
        m_StateMachine.ChangeState(new RoamingPlayerState(m_StateMachine));
        
    }
}
