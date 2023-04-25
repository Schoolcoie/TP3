using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;
    public AudioPool Pool;

    [System.Serializable]
    private struct SoundStruct
    {
        public SoundEnum Name;
        public AudioClip Clip;
    }

    [SerializeField] private List<SoundStruct> SoundAssociations;

    public enum SoundEnum
    {
        Win,
        Lose,
        EndGame,
        FishReeling,
        FishEscaping,
        FishCaught,
        FishLost
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static AudioManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(SoundEnum sound)
    {
        AudioSource source = Pool.GetValidAudioSource();

        for (int i = 0; i < SoundAssociations.Count; i++)
        {
            if (SoundAssociations[i].Name == sound)
            {
                source.clip = SoundAssociations[i].Clip;
            }
        }
        source.gameObject.transform.position = FindObjectOfType<PlayerStateMachine>().transform.position;
        source.Play();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<AudioPool>() != null)
            Pool = FindObjectOfType<AudioPool>();
    }
}
