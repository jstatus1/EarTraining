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

public enum ChordType
{
    None,
    Major,
    Minor,
    Diminshed,
    Augmented, 
    Major7th,
    Minor7th,
    Dominant7th,
    Suspended
}

public enum InstrumentTypes
{
    Guitar,
    Piano,
    Violin
}

[CreateAssetMenu(fileName = "Data Single", menuName= "ScriptableObjects/DataSingle")]
public class DataSingle : ScriptableObject
{
    public DataTypes DataType;
    public ChordType ChordType;
    public InstrumentTypes InstrumentTypes;
    public string Title;
    public AudioClip AudioClip;
    public List<AudioClip> List_AudioClip;

    [TextArea]
    public string Information;
    public Image Image;

    System.Random rnd = new System.Random();

    public AudioClip getAudioClip
    {
        get
        {
            AudioClip rtnAudioClip;
            if(List_AudioClip.Count > 0)
            {
                 rtnAudioClip =  List_AudioClip[rnd.Next(List_AudioClip.Count)];
            }else{
                rtnAudioClip = AudioClip;
            }
            return rtnAudioClip;
        }
        
    }


}
