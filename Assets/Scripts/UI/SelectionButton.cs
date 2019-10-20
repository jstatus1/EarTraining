using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI.IndividualButton
{
    public class SelectionButton : MonoBehaviour
    {
        [SerializeField] DataSingle DataSingle;
        [SerializeField] TMP_Text Text_title;
        Toggle _thisToggle;

        [SerializeField] Color Color_Selected;
        [SerializeField] Color Color_UnSelected;

        // Start is called before the first frame update
        void Start()
        {
            _thisToggle = this.gameObject.GetComponent<Toggle>();
            Text_title.text = DataSingle.Title.ToUpper();
            _thisToggle.onValueChanged.AddListener(delegate{
                if(_thisToggle.isOn)
                {
                    this.gameObject.GetComponent<Image>().color = Color_Selected;
                    UI.QuizSetting.QuizSettingsListener.List_SelectedDataSingles.Add(DataSingle);
                    //UI.QuizSetting.QuizSettingsListener.List_StoreSelection.Add(DataSingle);
                }else{
                    this.gameObject.GetComponent<Image>().color = Color_UnSelected;
                    UI.QuizSetting.QuizSettingsListener.List_SelectedDataSingles.Remove(DataSingle);
                    //UI.QuizSetting.QuizSettingsListener.List_StoreSelection.Remove(DataSingle);
                }
            });
            
        }

        public DataSingle GetSetDataSingle
        {
            get{
                return DataSingle;
            }
            set{
                DataSingle = value;
                Text_title.text = DataSingle.Title;
            }
            
        }

        
    }

}
    