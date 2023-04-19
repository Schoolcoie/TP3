using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Fish : ScriptableObject
{
    public Texture2D Icon;
    public string Name;
    public int Difficulty;
    public float MovementInterval;
}
