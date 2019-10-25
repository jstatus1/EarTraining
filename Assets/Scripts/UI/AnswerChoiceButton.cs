﻿using System.Collections;
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
    [SerializeField] Color Color_Selected;
    [SerializeField] Color Color_Unselected;
    DataSingle dataSingle;
    AudioClip AudioClip_Audio;
    AudioSource AudioSource_Main;
    Button Button_CheckAnswer;

    bool isAnswer = false;
    
    public DataSingle setDataSingle
    {
        set{
            dataSingle = value;
            Text_AnswerOption.text = dataSingle.Title;
            AudioClip_Audio = dataSingle.getAudioClip;
        }
    }

    public Button setCheckAnswerButton
    {
        set{
            Button_CheckAnswer = value;
        }
    }

    void Start()
    {
        AudioSource_Main = Managers.QuizManager.Instance.getMainAudio; 
        setButton();
    }

    void setButton()
    {
        Button_Audio.onClick.AddListener(() =>{
            playAudio();
        });

        Button_Answer.onValueChanged.AddListener(delegate 
        {
             if(Button_Answer.isOn)
                {
                    Button_Answer.GetComponent<Image>().color = Color_Selected;
                    //set check answer button off
                    Button_CheckAnswer.gameObject.SetActive(true);

                }else{
                    Button_Answer.GetComponent<Image>().color = Color_Unselected;
                    //set check answer button off
                    Button_CheckAnswer.gameObject.SetActive(false);
                }
        });
    }

    public void setToggleGroup(ToggleGroup group)
    {
        Button_Answer.group = group;
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

    void playAudio()
    {
        AudioClip_Audio = dataSingle.getAudioClip;
        AudioSource_Main.clip = AudioClip_Audio;
        AudioSource_Main.Play();
    }
}
