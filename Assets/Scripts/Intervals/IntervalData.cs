using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Interval_Data", menuName= "ScriptableObjects/IntervalData")]
public class IntervalData : ScriptableObject
{
    public string Title_Interval;
    public AudioClip AudioClip_Interval;
    public List<AudioClip> AudioClip_ListInterval;
    public string Information_Interval;
    public Image Image_Interval;


}
