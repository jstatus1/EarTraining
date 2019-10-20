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

[CreateAssetMenu(fileName = "Interval_Data", menuName= "ScriptableObjects/IntervalData")]
public class DataSingle : ScriptableObject
{
    public DataTypes DataType;
    public string Title_Interval;
    public AudioClip AudioClip_Interval;
    public List<AudioClip> AudioClip_ListInterval;
    public string Information_Interval;
    public Image Image_Interval;


}
