using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data Collection", menuName= "ScriptableObjects/DataCollection")]
public class DataCollection : ScriptableObject
{
    public DataTypes dataTypes;
    public string Title_Collection;
    public List<DataSingle> List_DataSingles;

    public string Information;
    public Image Image;


    
}
