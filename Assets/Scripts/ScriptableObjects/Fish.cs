using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Fish : ScriptableObject
{

    [System.Serializable]
    public struct Fishes
    {
        public Sprite Icon;
        public string Name;
        public int Difficulty;
        public float MovementInterval;
    }

    public List<Fishes> FishList = new List<Fishes>();
}

   
