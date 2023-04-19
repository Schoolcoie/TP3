using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlayerState : PlayerState
{

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
        Debug.Log("Fishing");
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
