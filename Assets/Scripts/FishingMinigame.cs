using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    [SerializeField] private Fish m_FishScriptableObject;
    private List<Fish.Fishes> m_PossibleFish;
    private Fish.Fishes m_CurrentFish;
    [SerializeField] private Rigidbody2D m_FishBody;
    private Vector3 m_FishInitialPosition;
    private Vector3 m_BobberInitialPosition;
    [SerializeField] private Rigidbody2D m_Bobber;
    [SerializeField] private GameObject m_ProgressBar;
    private float m_FishProgress = 100;

    private float m_Ceiling;
    private float m_Floor;

    //Fish Stats
    private Texture2D m_FishIcon;
    private string m_FishName;
    private int m_FishDifficulty;
    private float m_FishMovementInterval;

    private void Awake()
    {
        m_FishInitialPosition = m_Bobber.transform.localPosition;
        m_BobberInitialPosition = m_FishBody.transform.localPosition;
        m_PossibleFish = m_FishScriptableObject.FishList;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        
        m_CurrentFish = m_PossibleFish[Random.Range(0, m_PossibleFish.Count)];
        Debug.Log($"Current fish is: {m_CurrentFish.Name}");
        m_Bobber.transform.localPosition = m_BobberInitialPosition;
        m_FishBody.transform.localPosition = m_FishInitialPosition;
        m_Ceiling = gameObject.transform.Find("TopEdge").transform.localPosition.y;
        m_Floor = gameObject.transform.Find("BottomEdge").transform.localPosition.y;
        m_FishProgress = 50;
        //m_FishIcon = m_CurrentFish.Icon;
        m_FishName = m_CurrentFish.Name;
        m_FishDifficulty = m_CurrentFish.Difficulty;
        m_FishMovementInterval = m_CurrentFish.MovementInterval;
        StartCoroutine(FishActionRoutine());
    }

    void Update()
    {
        m_ProgressBar.transform.localScale = new Vector3(m_ProgressBar.transform.localScale.x, (m_FishProgress / 100), m_ProgressBar.transform.localScale.z);

        if (m_FishProgress > 100 || m_FishProgress < 0)
        {
            EventManager.TriggerEvent("StopFishing");
        }

        if (Mathf.Abs(m_Bobber.transform.localPosition.y - m_FishBody.transform.localPosition.y) <= 12) //add variable for bobber range
        {
            m_FishProgress += 10 * Time.deltaTime;
            Debug.Log("Augmenting");
        }
        else
        {
            m_FishProgress -= 15 * Time.deltaTime;
            Debug.Log("Lowering");
        }


        if (m_Bobber.transform.localPosition.y > m_Ceiling)
        {
            m_Bobber.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Ceiling, m_Bobber.transform.localPosition.z);
            m_Bobber.velocity = Vector2.zero;
        }
        else if (m_Bobber.transform.localPosition.y < m_Floor)
        {
            m_Bobber.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Floor, m_Bobber.transform.localPosition.z);
            m_Bobber.velocity = Vector2.zero;
        }

        if (m_FishBody.transform.localPosition.y > m_Ceiling)
        {
            m_FishBody.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Ceiling, m_Bobber.transform.localPosition.z);
            m_FishBody.velocity = Vector2.zero;
        }
        else if (m_FishBody.transform.localPosition.y < m_Floor)
        {
            m_FishBody.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Floor, m_Bobber.transform.localPosition.z);
            m_FishBody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            m_Bobber.AddForce(Vector2.up * 4); //Add bobber speed variable
        }
    }

    private IEnumerator FishActionRoutine()
    {
        while (m_FishProgress < 100 && m_FishProgress > 0)
        {
            yield return new WaitForSeconds(m_FishMovementInterval);

            if (m_FishBody.transform.localPosition.y >= 50)
            {
                m_FishBody.AddForce(new Vector2(0, Random.Range(m_FishDifficulty / 2, m_FishDifficulty) * -1));
            }
            else if (m_FishBody.transform.localPosition.y <= -50)
            {
                m_FishBody.AddForce(new Vector2(0, Random.Range(m_FishDifficulty / 2, m_FishDifficulty)));
            }
            else
            {
                m_FishBody.AddForce(new Vector2(0, Random.Range(m_FishDifficulty / 2, m_FishDifficulty) * (Random.Range(0, 2) * 2 - 1)));
            }
        }
    }
}
