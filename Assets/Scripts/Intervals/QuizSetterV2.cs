using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


///<summary>
///The purpose of this class is to spawn quiz question, load the audio, and select
// the right answer
///</sumary>
public class QuizSetterV2: MonoBehaviour
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

    int _answerChoicesAmt = 2;

    //TODO: Replace in the future
    string [] Intervals = {"Perfect 5th, Perfect 4th, Perfect Octave, Major 2nd, Major 3rd"};

    void Start()
    {
            _mainAudio = gameObject.GetComponent<AudioSource>();
            getSetRandomDataSingle();
            SetButtons();
            SetAnswerOptions();
    }

    public void getSetRandomDataSingle()
    {
            //int randomNumber = rnd.Next(QuizManager.Instance.IntervalList.Count);
            //return IntervalList[randomNumber];
            
            _ansdata =  Managers.QuizManager.Instance.getIntervalData();
            
            if(_ansdata.AudioClip_ListInterval.Count > 1)
            {
                int randomNumber = rnd.Next(_ansdata.AudioClip_ListInterval.Count);
                dataSound = _ansdata.AudioClip_ListInterval[randomNumber];
                _mainAudio.clip = dataSound;
                //dataSound = data.AudioClip_Interval;
            }else
            {
                dataSound = _ansdata.AudioClip_Interval;
                //_mainAudio.clip = dataSound;
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

    void SetAnswerOptions()
    {
        List<IntervalDataSingle> answerChoices = new List<IntervalDataSingle>();
        answerChoices.Add(_ansdata);
        
        while(answerChoices.Count < _answerChoicesAmt)
        {
            var result = Managers.QuizManager.Instance.IntervalList.Where(p => !answerChoices.Any(p2 => p2 == p));
            if(result != null)
            {
                answerChoices.Add(result.First());
            }else{
                //answerChoices.Add();
                break;
            }
        }

        
    }
}