using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerNotifier : MonoBehaviour
{
    [SerializeField] GameObject CorrectObj;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip incorrectSound;
    [SerializeField] GameObject IncorrectObj;
    AudioSource mainAudioSource;

    void Start()
    {
        mainAudioSource = Managers.QuizManager.Instance.getMainAudio;
    }
    
    public void notifier(bool correct)
    {
        if(correct)
        {
            CorrectObj.SetActive(true);
            mainAudioSource.clip = correctSound;
            mainAudioSource.Play();
            Debug.Log("Correct");
            
            CorrectObj.SetActive(false);
        }
        else{
            IncorrectObj.SetActive(true);
            mainAudioSource.clip = incorrectSound;
            mainAudioSource.Play();
            Debug.Log("Wrong");
            IncorrectObj.SetActive(false);
        }
    }


}
