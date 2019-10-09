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

        

        
    }
}

