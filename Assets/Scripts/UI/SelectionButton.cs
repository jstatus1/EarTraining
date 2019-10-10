using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI.IndividualButton
{
    public class SelectionButton : MonoBehaviour
    {
        [SerializeField] IntervalDataSingle _intervalDataSingle;
        [SerializeField] TMP_Text Text_title;
        Toggle _thisToggle;

        [SerializeField] Color Color_Selected;
        [SerializeField] Color Color_UnSelected;

        // Start is called before the first frame update
        void Start()
        {
            _thisToggle = this.gameObject.GetComponent<Toggle>();
            Text_title.text = _intervalDataSingle.Title_Interval.ToUpper();
            _thisToggle.onValueChanged.AddListener(delegate{
                if(_thisToggle.isOn)
                {
                    this.gameObject.GetComponent<Image>().color = Color_Selected;
                    UI.QuizSetting.QuizSettingsListener.tester.Add(_intervalDataSingle);
                    //UI.QuizSetting.QuizSettingsListener.List_StoreSelection.Add(_intervalDataSingle);
                }else{
                    this.gameObject.GetComponent<Image>().color = Color_UnSelected;
                    UI.QuizSetting.QuizSettingsListener.tester.Remove(_intervalDataSingle);
                    //UI.QuizSetting.QuizSettingsListener.List_StoreSelection.Remove(_intervalDataSingle);
                }
            });
            
        }

        public IntervalDataSingle getDataSingle()
        {
            return _intervalDataSingle;
        }

        
    }

}
    