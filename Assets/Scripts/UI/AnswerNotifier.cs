using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerNotifier : MonoBehaviour
{
    [SerializeField] Button CorrectObj;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip incorrectSound;
    [SerializeField] Button IncorrectObj;
    [SerializeField] Button BlockPanel;
    AudioSource mainAudioSource;

    void Start()
    {
        mainAudioSource = Managers.QuizManager.Instance.getMainAudio;
        setButton();
    }

    void setButton()
    {
        BlockPanel.onClick.AddListener(() => {
            BlockPanel.gameObject.SetActive(false);
            CorrectObj.gameObject.SetActive(false);
            IncorrectObj.gameObject.SetActive(false);

        });
    }
    
    public void notifier(bool correct)
    {
        BlockPanel.gameObject.SetActive(true);
        if(correct)
        {
            CorrectObj.gameObject.SetActive(true);
            mainAudioSource.clip = correctSound;
            mainAudioSource.Play();
            Debug.Log("Correct");
        }
        else
        {
            IncorrectObj.gameObject.SetActive(true);
            mainAudioSource.clip = incorrectSound;
            mainAudioSource.Play();
            Debug.Log("Wrong");
        }
        
    }


}
