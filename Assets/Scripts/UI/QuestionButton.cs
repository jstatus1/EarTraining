﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.IndividualButton
{
    public class QuestionButton : LongButton
    {
        [SerializeField] TMP_Text Text_title;
        [SerializeField] AudioClip Sound;

        // Start is called before the first frame update
        void Start()
        {   
            onLongClick.AddListener(playSound);
        }


        void playSound()
        {
            
        }
    }

}
