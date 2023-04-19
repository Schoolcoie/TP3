using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlayerState : PlayerState
{
    private float m_FishingProgress;
    private Rigidbody2D m_BobberBody;
    private FishingMinigame m_Fishing;

    public FishingPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        EventManager.StartListening("StopFishing", EndFishing);
    }

    private void EndFishing()
    {
        m_StateMachine.ChangeState(new RoamingPlayerState(m_StateMachine));
    }


    public override void ExecuteUpdate()
    {
        if (m_FishingProgress < 0 || m_FishingProgress > 100)
        {
            EventManager.TriggerEvent("StopFishing");
        }
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

}
