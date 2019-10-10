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
        public static List<IntervalDataSingle> IntervalList = new List<IntervalDataSingle>();
        public static List<string> strIntervalList = new List<string>();
        public int NumOfChoices = 2;

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
    