using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

///<summary>
/// This is button that displays for the selection screen
///</summary>

namespace UI.IndividualButton
{
    public class SelectionButton : MonoBehaviour
    {
        [Header("Configuration for UI")]
        [SerializeField] TMP_Text Text_title;
        [SerializeField] Button Button_Audio;
        [SerializeField] Color Color_Selected;
        [SerializeField] Color Color_UnSelected;
        DataSingle DataSingle;
        [SerializeField] Toggle _thisToggle;
        [SerializeField] AudioSource _audioSource;
        

        // Start is called before the first frame update
        void Start()
        {
            _audioSource = Managers.QuizManager.Instance.getMainAudio;
            //_thisToggle = gameObject.GetComponent<Toggle>();
            Text_title.text = DataSingle.Title.ToUpper();
            SetUpButton();
        }
        
        void SetUpButton()
        {
            Button_Audio.onClick.AddListener(() => {
               _audioSource.clip = DataSingle.AudioClip;
               _audioSource.Play(); 
            });

             _thisToggle.onValueChanged.AddListener(delegate{
                if(_thisToggle.isOn)
                {
                    _thisToggle.GetComponent<Image>().color = Color_Selected;
                    UI.QuizSetting.QuizSettingsListener.List_SelectedDataSingles.Add(DataSingle);
                    //UI.QuizSetting.QuizSettingsListener.List_StoreSelection.Add(DataSingle);
                }else{
                    _thisToggle.GetComponent<Image>().color = Color_UnSelected;
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
    