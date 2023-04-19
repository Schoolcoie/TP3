using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState m_CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentState = new RoamingPlayerState(this);
    }

    public void ChangeState(PlayerState newState)
    {
        m_CurrentState = newState;
    }

    // Update is called once per frame
    private void Update()
    {
        m_CurrentState.ExecuteUpdate();
    }

    private void FixedUpdate()
    {
        m_CurrentState.ExecuteFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_CurrentState.ExecuteOnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        m_CurrentState.ExecuteOnCollisionExit(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CurrentState.ExecuteOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        m_CurrentState.ExecuteOnTriggerExit(other);
    }
}
