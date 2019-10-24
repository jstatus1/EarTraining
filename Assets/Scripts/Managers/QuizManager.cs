using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;


/*
    * Purpose: Stores all the options that the user wants for his quiz
 */
namespace Managers
{
    public class QuizManager : MonoBehaviour
    {   
        [SerializeField] AudioSource Audio_MainAudio;
        private static QuizManager _instance;
        public static QuizManager Instance{get{return _instance;}}

        [Header("User Desired Quiz Training")]
        public List<DataSingle> List_DataSingles = new List<DataSingle>();
        static System.Random rnd = new System.Random();
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

        public DataSingle getDataSingle
        {
            get
            {
                int randomInt = rnd.Next(List_DataSingles.Count);
                return List_DataSingles[randomInt];
            } 
        }

        public void clearDataList()
        {
            List_DataSingles.Clear();
        }

        public AudioSource getMainAudio
        {
            get{
                return Audio_MainAudio;
            }
        }
        
    }

}
    