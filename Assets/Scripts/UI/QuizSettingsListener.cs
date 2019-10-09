using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.QuizSetting
{
    public class QuizSettingsListener : MonoBehaviour
    {
          [Header("Button Configuration")]
          [SerializeField] Button Button_Play;
          [SerializeField] Button Button_Back;
          [SerializeField] Button Button_Forward;
          

          [Header("Stored Settings")]
          [SerializeField] List<IntervalData> List_Data;
          [SerializeField] TMP_Dropdown DropDown_Choices;

          [Header("Question Configuration")]
          [SerializeField] List<GameObject> List_Questions;
        
          int _currentQuestionIndex = 1;
          //count number of questions asked:
          int numberOfQuestions = 0;
          
          //loop through and ask questions
          void Start()
          {
              numberOfQuestions = List_Questions.Count;
              QuestionSetUp();
              SetButtons();
          }

          void SetButtons()
          {

              Button_Play.onClick.AddListener(() => {
                
              });
              Button_Forward.onClick.AddListener(() => {
                _currentQuestionIndex++;
                Debug.Log("clicked forward");
                QuestionSetUp();

              });
              Button_Back.onClick.AddListener(()=> {
                _currentQuestionIndex--;
                 QuestionSetUp();

              });
              

              //TODO: Make Separate Class Called Questions with specialized child of different variations
              DropDown_Choices.onValueChanged.AddListener(delegate{
                Managers.QuizManager.Instance.NumOfChoices = (int)(DropDown_Choices.value);
              });
          }

          void QuestionSetUp()
          {
              //Pull how many questions are in the list
              
              

              if(_currentQuestionIndex >= numberOfQuestions)
                {
                    Button_Back.gameObject.SetActive(true);
                    Button_Forward.gameObject.SetActive(false);
                }
              else if(_currentQuestionIndex == 1)
                {
                    Button_Back.gameObject.SetActive(false);
                    Button_Forward.gameObject.SetActive(true);
                }
              else
                {
                    Button_Back.gameObject.SetActive(true);
                    Button_Back.gameObject.SetActive(true);
                }
             
             //set all gameObject to null
             List_Questions.ForEach(question => {
               question.SetActive(false);
             });

             //Array is zerobased
             List_Questions[(_currentQuestionIndex - 1)].SetActive(true);
          }
          
          #region storage of data and tranfer to Quiz Manager

        //store quiz questions

          #endregion    
    }
}

