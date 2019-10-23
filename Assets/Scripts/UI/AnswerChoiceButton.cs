using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

///<summary>
/// Prefab for Answer Choices
///</summary>
public class AnswerChoiceButton : MonoBehaviour
{
    [SerializeField] TMP_Text Text_AnswerOption;
    [SerializeField] Button Button_Audio;
    [SerializeField] Toggle Button_Answer;
    DataSingle dataSingle;
    AudioClip AudioClip_Audio;
    AudioSource AudioSource_Main;

    bool isAnswer;

    public DataSingle setDataSingle
    {
        set{
            dataSingle = value;
            Text_AnswerOption.text = dataSingle.Title;
            AudioClip_Audio = dataSingle.getAudioClip;
        }
    }

    void Start()
    {
        AudioSource_Main = Managers.QuizManager.Instance.getMainAudio; 
    }

    public bool getSetIsAnswer
    {
        get{
            return isAnswer;
        }
        set{
            isAnswer = value;
        }
    }
}
