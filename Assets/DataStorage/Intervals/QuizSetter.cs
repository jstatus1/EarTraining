using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;


///<summary>
///The purpose of this class is to spawn quiz question, load the audio, and save
// the right answer
///</sumary>
public class QuizSetter: MonoBehaviour
{
    [Header("Quiz Configuration")]
    [SerializeField] Button Button_next;
    [SerializeField] Button Button_PlaySound;
    [SerializeField] Button Button_Exit;
    [SerializeField] TMP_Text Text_Question;
    [SerializeField] static AudioSource _mainAudio;
    [SerializeField] Transform Questions_Location;
    [SerializeField] GameObject Prefab_AnswerChoice;
    static System.Random rnd = new System.Random();
    static AudioClip AudioClip_AnswerClip;
    string _answer;
    DataSingle _ansdata;

    int _answerChoicesAmt = 2;

    //TODO: Replace in the future
    string [] Intervals = {"Perfect 5th, Perfect 4th, Perfect Octave, Major 2nd, Major 3rd"};

    void Start()
    {
        _mainAudio = Managers.QuizManager.Instance.getMainAudio;
        _answerChoicesAmt = Managers.QuizManager.Instance.NumOfChoices;


        getSetAnswer();
        SetButtons();
        //SetAnswerOptions();
    }

    ///<summary>
    /// Gets Random DataSingle from the Manager Class and Loads it
    ///</<summary>
    public void getSetAnswer()
    {
        _ansdata =  Managers.QuizManager.Instance.getDataSingle;
            
        if(_ansdata.List_AudioClip.Count > 1)
        {
            int randomNumber = rnd.Next(_ansdata.List_AudioClip.Count);
            AudioClip_AnswerClip = _ansdata.List_AudioClip[randomNumber];
            _mainAudio.clip = AudioClip_AnswerClip;
        }else
        {
            AudioClip_AnswerClip = _ansdata.AudioClip;
            _mainAudio.clip = AudioClip_AnswerClip;
        }

        _answer = _ansdata.Title; 
        Text_Question.text = $"What {_ansdata.DataType.ToString().TrimEnd('s')} is this?";
    }

    ///<summary>
    /// Sets up the Buttons for this Quiz Scene
    ///</summary>
    void SetButtons()
    {
            Button_next.onClick.AddListener(() => {
                //go to the next slide
                getSetAnswer();
            });
            Button_PlaySound.onClick.AddListener(() => {
                
                if(_mainAudio.clip == null)
                {
                    getSetAnswer();
                }else{
                    _mainAudio.clip = AudioClip_AnswerClip;
                    _mainAudio.Play();
                }
            });
            Button_Exit.onClick.AddListener(() => {
                //show exit panel confirmation
            });
    }

    ///<summary>
    ///
    ///</summary>
    string LoopThroughList()
    {
            string str = "";
            if(Managers.QuizManager.Instance.List_DataSingles.Count == 0)
            {
                return "Nothing";
            }
            foreach(DataSingle i in  Managers.QuizManager.Instance.List_DataSingles)
            {
                str += i.Title + " ";
            }
            return str;
    }

    ///<summary>
    /// Sets The Answer Choices through random shuffle
    ///</summary>
    public void SetAnswerOptions()
    {
        List<DataSingle> answerChoices = new List<DataSingle>();
        answerChoices.Add(_ansdata);
        
        while(answerChoices.Count < _answerChoicesAmt)
        {
            //query all that that was in the User's Given Choice and not the answer
            var result = Managers.QuizManager.Instance.List_DataSingles.Where(p => !answerChoices.Any(p2 => p2 == p));
            if(result != null)
            {
                answerChoices.Add(result.First());
            }else{
                //TODO: Replace with not hardcoded choices
                //answerChoices.Add();
                break;
            }
        }

        foreach(DataSingle i in answerChoices)
        {
            Debug.Log(i.Title);
        }

        
    }
}