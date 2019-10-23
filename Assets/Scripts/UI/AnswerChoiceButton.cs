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

    AudioClip AudioClip_Audio;
    AudioSource AudioSource_Main;

    bool answer;

    void Start()
    {
        AudioSource_Main = Managers.QuizManager.Instance.getMainAudio; 
    }

    public bool getSetAnswer
    {
        get{
            return answer;
        }
        set{
            answer = value;
        }
    }
}
