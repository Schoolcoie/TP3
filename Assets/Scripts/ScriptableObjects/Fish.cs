using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Fish : ScriptableObject
{

    [System.Serializable]
    public struct FishStruct
    {
        public InventoryManager.Items itemdrop;
        public int difficulty;
        public float movementinterval;
    }

    public List<FishStruct> FishList = new List<FishStruct>();
}

   
