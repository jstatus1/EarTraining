using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

///<summary>
/// Acts As A Prefab for displaying all the data singles 
/// from the data collection to the UI
/// The dataCollection data type will decide what selections are available
///</summary>
public class SelectorQuestion : MonoBehaviour
{
    [Header("Configurations for Question Selection")]
    [SerializeField] TMP_Text Text_Question;
    [SerializeField] Transform Location_Content;
    DataCollection dataCollection;

    [SerializeField] GameObject Prefab_SelectionButton;


    public DataCollection SetDataCollection
    {
        set{
            dataCollection = value;
            Text_Question.text = "Please Select Your " + dataCollection.Title_Collection;
            CreateDataSingles();
        }
    }

    void CreateDataSingles()
    {
        foreach(DataSingle data in dataCollection.List_DataSingles)
        {
            var obj = Instantiate(Prefab_SelectionButton) as GameObject;
            obj.GetComponent<UI.IndividualButton.SelectionButton>().GetSetDataSingle = data;
            obj.transform.SetParent(Location_Content,false);
        }
    }
}
