using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

///<summary>
/// Provides functionality for collection button
///<summary>

//TODO: Refactor Collection Button and Selection Button because it is not DRY
public class CollectionButton : MonoBehaviour
{
    [Header("Button Configurations")]
    [SerializeField] TMP_Text Title_Collection;
    DataCollection DataCollection;
    [SerializeField] Toggle _thisToggle;
    [SerializeField] Button Button_Information;
    [SerializeField] Color Color_Selected;
    [SerializeField] Color Color_Unselected;


    public CollectionButton(DataCollection dataCollection)
    {
        this.DataCollection = dataCollection;
        Title_Collection.text = dataCollection.Title_Collection;
    }

    public DataCollection SetDataCollection
    {
        set{
            DataCollection = value;
            Title_Collection.text = DataCollection.Title_Collection;

        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        setUpButton();
    }
    
    void setUpButton()
    {
        Button_Information.onClick.AddListener(() => {
            SceneManager.LoadSceneAsync("1_Information_Scene", LoadSceneMode.Additive);
        });

         _thisToggle.onValueChanged.AddListener(delegate{
                if(_thisToggle.isOn)
                {
                    this.gameObject.GetComponent<Image>().color = Color_Selected;
                    UI.QuizSetting.QuizSettingsListener.List_CategorySelection.Add(DataCollection);
                }else{
                    this.gameObject.GetComponent<Image>().color = Color_Unselected;
                    UI.QuizSetting.QuizSettingsListener.List_CategorySelection.Remove(DataCollection);
                }
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
