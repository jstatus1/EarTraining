using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    * Purpose: Stores all the options that the user wants for his quiz
 */
namespace Managers
{
    public class QuizManager : MonoBehaviour
    {   

        private static QuizManager _instance;
        public static QuizManager Instance{get{return _instance;}}

        [Header("User Desired Quiz Training")]
        public List<IntervalData> IntervalList;
        public int NumOfChoices;

        void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }else{
                _instance = this;
            }
            DontDestroyOnLoad(this);
        }



    }

}
    