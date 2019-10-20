

namespace UI.QuizSetting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine.SceneManagement;

    public class QuizSettingsListener : MonoBehaviour
    {
          [Header("Button Configuration")]
          [SerializeField] Button Button_Play;
          [SerializeField] Button Button_Back;
          [SerializeField] Button Button_Forward;
          

          [Header("Stored Settings")]
          [SerializeField] List<DataSingle> List_Data;
          [SerializeField] TMP_Dropdown DropDown_Choices;

          [Header("Question Configuration")]
          [SerializeField] List<GameObject> List_Questions;

          [Header("Prefab Configuration")]
          [SerializeField] GameObject Prefab_CollectionButton;

          ///<summary>
          /// Stores all the User's Selection Categories
          ///</summary>
          [SerializeField] public static ObservableCollection<DataCollection> List_CategorySelection = new ObservableCollection<DataCollection>();

          ///<summary>
          /// Stores all the User's Selected Data Singles
          ///</summary>
          public static ObservableCollection<DataSingle> List_SelectedDataSingles = new ObservableCollection<DataSingle>();
          [SerializeField] TMP_Text Text_SelectedDataResult;
          int _currentQuestionIndex = 1;
          //count number of questions asked:
          int numberOfQuestions = 0;

          [SerializeField] Animator animator;

          [SerializeField] Transform Location_CategorySelection;

          [SerializeField] CollectionsStorage _collectionsStorage;

          void Awake()
          {
            
          }
          
          //loop through and ask questions
          void Start()
          {
              
              numberOfQuestions = List_Questions.Count;
              QuestionSetUp();
              SetButtons();
              listenToSelections();
              listenToCategorySelections();

              LoadCategories();
          }

          void SetButtons()
          {

              Button_Play.onClick.AddListener(() => {
                playButtonPressed();
              });
              Button_Forward.onClick.AddListener(() => {
                ++_currentQuestionIndex;
                Debug.Log("clicked forward");
                QuestionSetUp();

              });
              Button_Back.onClick.AddListener(()=> {
                --_currentQuestionIndex;
                 QuestionSetUp();

              });
              

              //TODO: Make Separate Class Called Questions with specialized child of different variations
              DropDown_Choices.onValueChanged.AddListener(delegate{
                Managers.QuizManager.Instance.NumOfChoices = Int32.Parse(DropDown_Choices.options[DropDown_Choices.value].text);
              });
          }

          void LoadCategories()
          {
            foreach(DataCollection collection in _collectionsStorage.getListCollections())
            {
              var obj = Instantiate(Prefab_CollectionButton) as GameObject;
              obj.GetComponent<CollectionButton>().SetDataCollection = collection;
              obj.transform.SetParent(Location_CategorySelection, false);
            }
          }

          void QuestionSetUp()
          {
              //Pull how many questions are in the list
              if(_currentQuestionIndex == numberOfQuestions)
                {
                    Button_Back.gameObject.SetActive(true);
                    Button_Forward.gameObject.SetActive(false);
                }
              else if(_currentQuestionIndex == 1)
                {
                    Button_Back.gameObject.SetActive(false);
                    Button_Forward.gameObject.SetActive(true);
                }
              else
                {
                    Button_Back.gameObject.SetActive(true);
                    Button_Forward.gameObject.SetActive(true);
                }
            
             //set all gameObject to null
             List_Questions.ForEach(question => {
               question.SetActive(false);
             });

             //Array is zerobased
             List_Questions[(_currentQuestionIndex - 1)].SetActive(true);  
          }
          


          #region storage of data and tranfer to Quiz Manager

          //store quiz questions
          //store selected intervals


          //TODO:
          //1. need a storage container that contains all the IntervalData_Singles and creates button elements for each single one on the second Question2 GameObject
          //2. This is redandant for Chords, Scales, etc
          //3. Check to see if amount of stored values on the List_StoreSelection
          

          ///<summary>
          /// Listen to category selections
          ///</summary>
          public void listenToCategorySelections()
          { 
            List_CategorySelection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
                delegate(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                  if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                  {
                    Debug.Log("Added Collection");
                  }else if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                  {
                    Debug.Log("Removed Collection");
                  }

                  //ReadyGameListener.Text_Selection.text = DisplayUserSelections();
                  Text_SelectedDataResult.text = DisplayUserSelections();
                }
              );
          } 

          //Listen To Adding or Removing Selections
          public void listenToSelections()
          {
              Text_SelectedDataResult.text = DisplayUserSelections();
              //TODO: Abstract this part of the code away
              //adds a listener to the list to see when things are added or removed
              //updates the text selected
              List_SelectedDataSingles.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
                delegate(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                  if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                  {
                    Debug.Log("Added");
                  }else if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                  {
                    Debug.Log("Removed");
                  }

                  //ReadyGameListener.Text_Selection.text = DisplayUserSelections();
                  Text_SelectedDataResult.text = DisplayUserSelections();
                }
              );
          }
          
          //send the names
          public string DisplayUserSelections()
          {
            Text_SelectedDataResult.color = Color.green;
            string result = "";
            foreach(DataSingle data in List_SelectedDataSingles)
            {
              result += data.Title + ", ";
            }
            

            // while(result[result.Length - 1].Equals(" ") || result[result.Length - 1].Equals(','))
            // {
            //   result.Remove(result.Length - 1);
            // }

            if(result == "")
            {
              Text_SelectedDataResult.color = Color.red;
              return "Error, Please Select Intervals To Practice With";
            }
              return result;
          }
          

          #endregion    


          #region transfer data to game
          void playButtonPressed()
          {
            
            if(Managers.QuizManager.Instance.NumOfChoices == 0)
            {
              Managers.QuizManager.Instance.NumOfChoices = 2;
            }

            if(List_SelectedDataSingles.Count == 0)
            {
              Debug.Log("Please Select more than 0 items");
              animator.Play("Pressed_Failed");
              return;
            }
            
            transferSelectionsToListManager();

            SceneManager.LoadScene("2_Quiz_Scene");
          }

          void transferSelectionsToListManager()
          {
            Managers.QuizManager.Instance.IntervalList.Clear();
            foreach(DataSingle single in List_SelectedDataSingles)
            {
              Managers.QuizManager.Instance.IntervalList.Add(single);
            }
          }
          #endregion
    }
}

