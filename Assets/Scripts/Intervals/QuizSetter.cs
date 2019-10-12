using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Managers
{
    public class QuizSetter : MonoBehaviour
    {
        [SerializeField] Button Button_next;
        [SerializeField] Button Button_PlaySound;
        [SerializeField] Button Button_Exit;
        [SerializeField] static AudioSource _mainAudio;
        [SerializeField] GameObject Button_Question;
        [SerializeField] Transform Questions_Location;
        static System.Random rnd = new System.Random();
        static AudioClip dataSound;
        string _answer;
        IntervalDataSingle _ansdata;

        //TODO: Replace in the future
        string [] Intervals = {"Perfect 5th, Perfect 4th, Perfect Octave, Major 2nd, Major 3rd"};

        
        // Start is called before the first frame update
        void Start()
        {
            _mainAudio = gameObject.GetComponent<AudioSource>();
            getSetRandomDataSingle();
            SetButtons();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

       public void getSetRandomDataSingle()
        {
            //int randomNumber = rnd.Next(QuizManager.Instance.IntervalList.Count);
            //return IntervalList[randomNumber];
            
            _ansdata =  QuizManager.Instance.getIntervalData();
            
            if(_ansdata.AudioClip_ListInterval.Count > 1)
            {
                int randomNumber = rnd.Next(_ansdata.AudioClip_ListInterval.Count);
                dataSound = _ansdata.AudioClip_ListInterval[randomNumber];
                _mainAudio.clip = dataSound;
                //dataSound = data.AudioClip_Interval;
            }else
            {
                dataSound = _ansdata.AudioClip_Interval;
                _mainAudio.clip = dataSound;
            }

            _answer = _ansdata.Title_Interval; 
        }

        void SetButtons()
        {
            Button_next.onClick.AddListener(() => {
                //go to the next slide
                getSetRandomDataSingle();
            });
            Button_PlaySound.onClick.AddListener(() => {
                
                if(_mainAudio.clip == null)
                {
                    getSetRandomDataSingle();
                }else{
                    _mainAudio.clip = dataSound;
                    _mainAudio.Play();
                }
            });
            Button_Exit.onClick.AddListener(() => {
                //show exit panel confirmation
            });
        }

        string LoopThroughList()
        {
            string str = "";
            if(Managers.QuizManager.Instance.IntervalList.Count == 0)
            {
                return "Nothing";
            }
            foreach(IntervalDataSingle i in  Managers.QuizManager.Instance.IntervalList)
            {
                str += i.Title_Interval + " ";
            }
            return str;
        }

        //generate choices - questions
        #region Generate Questions
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rnd.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }

        void LoadQuestionOptions()
        {
            List<IntervalDataSingle> unshuffled = new List<IntervalDataSingle>();
            unshuffled.Add(_ansdata);
            while(unshuffled.Count < QuizManager.Instance.NumOfChoices)
            {
                
            }

            for(int optionAmt = 0; optionAmt < QuizManager.Instance.NumOfChoices; optionAmt++)
            {
                GameObject newOption = Instantiate(Button_Question,Questions_Location, false) as GameObject;
            }
            
        }


        #endregion
        
    }
}

