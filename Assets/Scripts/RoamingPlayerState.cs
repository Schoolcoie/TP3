using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingPlayerState : PlayerState
{
    private Rigidbody m_Body;
    private float m_Speed = 200.0f;
    private string m_MinigameEventToTrigger;
    private bool m_CanTriggerMinigame = false;

    public RoamingPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        m_Body = stateMachine.GetComponent<Rigidbody>();
        EventManager.StartListening("MinigameFishing", StartFishing);
        EventManager.StartListening("MinigameForaging", StartForaging);
        EventManager.StartListening("Pause", Pause);
    }

    private void StartFishing()
    {
        m_StateMachine.ChangeState(new FishingPlayerState(m_StateMachine));
    }

    private void StartForaging()
    {
        m_StateMachine.ChangeState(new ForagingPlayerState(m_StateMachine));
    }

    private void Pause()
    {
        EventManager.StopListening("MinigameFishing", StartFishing);
        EventManager.StopListening("MinigameForaging", StartForaging);
        EventManager.StopListening("Pause", Pause);
        m_StateMachine.ChangeState(new PausedPlayerState(m_StateMachine));
    }


    public override void ExecuteUpdate()
    {
        if (m_CanTriggerMinigame)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventManager.TriggerEvent(m_MinigameEventToTrigger);
            }
        }
    }

    public override void ExecuteFixedUpdate()
    {
        float horizontalaxis = Input.GetAxis("Horizontal");
        float verticalaxis = Input.GetAxis("Vertical");
        m_Body.velocity = new Vector3(horizontalaxis * m_Speed, 0, verticalaxis * m_Speed) * Time.deltaTime;
    }

    public override void ExecuteOnCollisionEnter(Collision collision)
    {
    }
    public override void ExecuteOnCollisionExit(Collision collision)
    {
    }

    public override void ExecuteOnTriggerEnter(Collider other)
    {
        EventManager.TriggerEvent("StartInteraction");

        Debug.Log(other.name);
        if (other.gameObject.tag.Contains("Minigame"))
        {
            m_MinigameEventToTrigger = other.gameObject.tag;
            m_CanTriggerMinigame = true;

            //show ui for pressing e to start minigame
        }
    }

    public override void ExecuteOnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Minigame"))
        {
            m_MinigameEventToTrigger = null;
            m_CanTriggerMinigame = false;
        }
    }
}
