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
        [SerializeField] AudioSource _mainAudio;
        static System.Random rnd = new System.Random();
        static AudioClip dataSound;

        
        // Start is called before the first frame update
        void Start()
        {
            SetButtons();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

       public static void getSetRandomDataSingle()
        {
            //int randomNumber = rnd.Next(QuizManager.Instance.IntervalList.Count);
            //return IntervalList[randomNumber];

            IntervalDataSingle data =  QuizManager.Instance.getIntervalData();
            if(data.AudioClip_ListInterval.Count > 1)
            {
                int randomNumber = rnd.Next(data.AudioClip_ListInterval.Count);
                dataSound = data.AudioClip_ListInterval[randomNumber];
                //dataSound = data.AudioClip_Interval;
            }else
            {
                dataSound = data.AudioClip_Interval;
            }
            
        }

        void SetButtons()
        {
            Button_next.onClick.AddListener(() => {
                //go to the next slide
                Debug.Log("Clicked");
                LoopThroughList();
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
        
    }
}

