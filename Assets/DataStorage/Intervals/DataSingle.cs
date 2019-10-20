using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataTypes
{
    Intervals,
    Chords,
    ChordProgression,
    Scales,
    Triads,
    Notes,
    
}

[CreateAssetMenu(fileName = "Data Single", menuName= "ScriptableObjects/DataSingle")]
public class DataSingle : ScriptableObject
{
    public DataTypes DataType;
    public string Title;
    public AudioClip AudioClip;
    public List<AudioClip> List_AudioClip;
    public string Information;
    public Image Image;


}
