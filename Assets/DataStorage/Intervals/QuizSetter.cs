using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using SA.CrossPlatform.UI;


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
    [SerializeField] CollectionsStorage CollectionsStorage;
    [SerializeField] ToggleGroup ToggleGroup;
    static System.Random rnd = new System.Random();
    static AudioClip AudioClip_AnswerClip;
    string _answer;
    DataSingle _ansdata;

    int _answerChoicesAmt = 2;


    void Awake()
    {
        CollectionsStorage = GameObject.FindObjectOfType<CollectionsStorage>();
    }

    void Start()
    {
        _mainAudio = Managers.QuizManager.Instance.getMainAudio;
        _answerChoicesAmt = Managers.QuizManager.Instance.NumOfChoices;


        getSetAnswer();
        SetButtons();
        InstantiateAnswerOptions();
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
                if(!ToggleGroup.ActiveToggles().Any())
                {
                    string message = $"Please Select An Answer";
                    var builder = new UM_NativeDialogBuilder("", message);
                    builder.SetPositiveButton("Okay", () => {
                        
                    });

                    var dialog = builder.Build();
                    dialog.Show();
                    return;
                }
                checkAnswer();
            });
            Button_PlaySound.onClick.AddListener(() => {
                _mainAudio.clip = AudioClip_AnswerClip;
                _mainAudio.Play();
            });
            Button_Exit.onClick.AddListener(() => {
                string title = "Return Home";
                string message = "Are You Sure You Want To Return Home?";
                var builder = new UM_NativeDialogBuilder(title, message);
                builder.SetPositiveButton("Okay", () => {
                    SceneManager.LoadScene("0_MenuScreen");
                    Managers.QuizManager.Instance.clearDataList();
                });

                builder.SetNegativeButton("Nah", () =>
                {
                    
                });

                var dialog = builder.Build();
                dialog.Show();
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
    /// Shuffle The Answer Choices through random shuffle
    /// Returns A List<DataSingle>
    ///</summary>
    public List<DataSingle> SetAnswerOptionsList()
    {
        List<DataSingle> answerChoices = new List<DataSingle>();
        answerChoices.Add(_ansdata);
        DataTypes dataTypes = _ansdata.DataType;
        
        
        DataCollection alternativeAnswerCollection = CollectionsStorage.GetComponent<CollectionsStorage>().grabCollection(dataTypes);
        
        //set answer and place inside list
        while(answerChoices.Count < _answerChoicesAmt)
        {
            //query all that that was in the User's Given Choice and not the answer
            var result = Managers.QuizManager.Instance.List_DataSingles.Where(p => (!answerChoices.Any(p2 => (p2 == p))) && p.DataType.Equals(dataTypes));
            
            if(result.Any())
            {
                answerChoices.Add(result.First());
            }else{
                
                var altResult = alternativeAnswerCollection.List_DataSingles.Where(p => (!answerChoices
                             .Any(p2 => (p2 == p)) && (p.DataType.Equals(dataTypes))));
                
                if(altResult.Any())
                {
                    answerChoices.Add(altResult.First());
                    break;
                }else{
                    var anyResult = alternativeAnswerCollection.List_DataSingles.Where(p => (!answerChoices
                             .Any(p2 => (p2 == p))));
                    answerChoices.Add(altResult.First());
                    break;
                }
                
            }
        }

        //shuffle list
        answerChoices.Shuffle();
        return answerChoices;
    }

    ///<summary>
    ///creates the option
    ///</summary>
    public void InstantiateAnswerOptions()
    {
        Questions_Location.Clear();
        ToggleGroup.SetAllTogglesOff();
        List<DataSingle> List_AnswerOptions = SetAnswerOptionsList();
        foreach(DataSingle answerChoice in List_AnswerOptions)
        {
            GameObject optionsBtn =  Instantiate(Prefab_AnswerChoice) as GameObject;
            optionsBtn.GetComponent<AnswerChoiceButton>().setDataSingle = answerChoice;
            if(answerChoice.Equals(_ansdata))
            {
                optionsBtn.GetComponent<AnswerChoiceButton>().getSetIsAnswer = true;
            }
            optionsBtn.GetComponent<AnswerChoiceButton>().setToggleGroup(ToggleGroup);
            optionsBtn.transform.SetParent(Questions_Location, false);
        }
    }


    void checkAnswer()
    {          

        Debug.Log($"Answer: {ToggleGroup.ActiveToggles().First().GetComponentInParent<AnswerChoiceButton>().getSetIsAnswer}");
        if(ToggleGroup.ActiveToggles().First().GetComponentInParent<AnswerChoiceButton>().getSetIsAnswer)
        {
            string title = "Correct";
            string message = "Good Job";
            var builder = new UM_NativeDialogBuilder(title, message);
            builder.SetPositiveButton("Okay", () => {
                            
            });
            var dialog = builder.Build();
            dialog.Show();
        }else{
            string title = "Incorrect";
            string message = "Please Try Again";
            var builder = new UM_NativeDialogBuilder(title, message);
            builder.SetPositiveButton("Okay", () => {
                            
            });
            var dialog = builder.Build();
            dialog.Show();
            return;
        }
        getSetAnswer();
        InstantiateAnswerOptions();
    }



}