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

    public void PlayLoopingSound(SoundEnum sound)
    {
        AudioSource source = Pool.GetValidAudioSource();

        for (int i = 0; i < SoundAssociations.Count; i++)
        {
            if (SoundAssociations[i].Name == sound)
            {
                source.clip = SoundAssociations[i].Clip;
                source.loop = true;
            }
        }
        source.gameObject.transform.position = FindObjectOfType<PlayerStateMachine>().transform.position;
        source.Play();
    }

    public void StopLoopingSound(SoundEnum sound)
    {
        for (int i = 0; i < Pool.transform.childCount; i++)
        {
            AudioSource source = Pool.transform.GetChild(i).GetComponent<AudioSource>();
            if (source.isPlaying)
            {
                for (int y = 0; y < SoundAssociations.Count; y++)
                {
                    if (SoundAssociations[y].Clip == source.clip && SoundAssociations[y].Name == sound)
                    {
                        source.Stop();
                        source.loop = false;
                    }
                }
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<AudioPool>() != null)
            Pool = FindObjectOfType<AudioPool>();
    }
}
