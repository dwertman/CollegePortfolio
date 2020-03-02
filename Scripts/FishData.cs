using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FishData", menuName = "Fish Data", order = 51)]
public class FishData : ScriptableObject
{
    [SerializeField]
    private string fishSpecies;
    [SerializeField]
    private string fishRarity;
    [SerializeField]
    private int fishValue;
    [SerializeField]
    private float fishSize;

    public string FishSpecies { get => fishSpecies;  }
    public string FishRarity { get => fishRarity;  }
    public int FishValue { get => fishValue; }
    public float FishSize { get => fishSize;  }
}
