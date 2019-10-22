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
    using SA.CrossPlatform.UI;


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
          [Tooltip("Storage For All The Questions This Scene Needs")]
          
          [SerializeField] GameObject Panel_Ready;
          [SerializeField] GameObject Question_CategorySelector;
          [SerializeField] GameObject Question_AnswerChoiceSize;

          [Tooltip("Prefab For The Question")]
          [SerializeField] GameObject Prefab_QuestionSelection;
          [SerializeField] Transform Location_PanelQuestion;
          Dictionary<string, GameObject> Dictionary_AddedSelectorQuestions = new Dictionary<string, GameObject>();

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

          [SerializeField] Animator animator;

          [SerializeField] Transform Location_CategorySelection;

          [SerializeField] CollectionsStorage _collectionsStorage;

          void Awake()
          {
            
          }
          
          //loop through and ask questions
          void Start()
          {
              SetQuestions();
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
                //QuestionSetUp();

              });
              Button_Back.onClick.AddListener(()=> {
                --_currentQuestionIndex;
                 //QuestionSetUp();

              });
              
              //TODO: Make Separate Class Called Questions with specialized child of different variations
              DropDown_Choices.onValueChanged.AddListener(delegate{
                Managers.QuizManager.Instance.NumOfChoices = Int32.Parse(DropDown_Choices.options[DropDown_Choices.value].text);
              });
          }

          void SetQuestions()
          {
            Panel_Ready.SetActive(false);
            Question_AnswerChoiceSize.SetActive(false);
            Question_CategorySelector.SetActive(true);
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

          #region Deletable??
          // void QuestionSetUp()
          // {
          //     //Pull how many questions are in the list
          //     if(_currentQuestionIndex == numberOfQuestions)
          //       {
          //           Button_Back.gameObject.SetActive(true);
          //           Button_Forward.gameObject.SetActive(false);
          //       }
          //     else if(_currentQuestionIndex == 1)
          //       {
          //           Button_Back.gameObject.SetActive(false);
          //           Button_Forward.gameObject.SetActive(true);
          //       }
          //     else
          //       {
          //           Button_Back.gameObject.SetActive(true);
          //           Button_Forward.gameObject.SetActive(true);
          //       }
            
          //    //set all gameObject to null
          //    List_Questions.ForEach(question => {
          //      question.SetActive(false);
          //    });

          //    //Array is zerobased
          //    List_Questions[(_currentQuestionIndex - 1)].SetActive(true);  
          // }
    #endregion      
          
          ///<summary>
          /// Connect the buttons with the flow of the questions
          /// refreshes every time
          /// replacement for QuestionSetUp()
          ///</summary>
          void QuestionFlow()
          {
              var title = "";
              var message = "";
              
                  

            //if Dictionary_AddedSelectorQuestions count is zero do not allow the user to go to the next question
            if(Dictionary_AddedSelectorQuestions.Count == 0)
            {
                title = "Error";
                message = "Please Select At Least One Topic To Be Quizzed Over";
                var builder = new UM_NativeDialogBuilder(title, message);
                builder.SetPositiveButton("Okay", () => {
                      
                  });
                return;
            }else{
                Question_CategorySelector.SetActive(false);
                Question_AnswerChoiceSize.SetActive(true);
                return;
            }
            //check to see if the Question_AnswerChoicesQuestion is active, if so, then check to see if the the player...
            if(Question_AnswerChoiceSize.activeSelf)
            {

            }
            //if player is on Question_Selectable type then make make sure the player has at least one of the SelectorButton activivated
            //if player is on Panel_Ready launch, check to see if all the following validation matches before proceeding to the question
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
                    
                    foreach(DataCollection p in e.NewItems)
                    {
                      Debug.Log("Added: " + p.Title_Collection + "to Collection List");
                      var obj = Instantiate(Prefab_QuestionSelection) as GameObject;
                      //DataCollection data = p;
                      obj.GetComponent<SelectorQuestion>().SetDataCollection = p;
                      obj.transform.SetParent(Location_PanelQuestion, false);
                      obj.SetActive(false);
                      Dictionary_AddedSelectorQuestions.Add(p.Title_Collection, obj);
                      Debug.Log($"Amount in Dictionary: {Dictionary_AddedSelectorQuestions.Count}");
                    }
                  }else if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                  {
                    foreach(DataCollection p in e.OldItems)
                    {
                      Debug.Log("Removed: " + p.Title_Collection + "from Collection List");
                      //remove from the List_Question

                      //remove from the scene
                      GameObject.Destroy(Dictionary_AddedSelectorQuestions[p.Title_Collection]);
                      Dictionary_AddedSelectorQuestions.Remove(p.Title_Collection);

                      
                      
                      Debug.Log($"Amount in Dictionary: {Dictionary_AddedSelectorQuestions.Count}");
                    }
                  }
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

