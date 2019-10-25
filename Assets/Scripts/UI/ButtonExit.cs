using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
/// For the exit Button
///</summary>
public class ButtonExit : MonoBehaviour
{
    [SerializeField] Button Button_Exit;
    
    [SerializeField] UI.QuizSetting.QuizSettingsListener quizSettingsListener;
    [SerializeField] string SceneToReturnTo;
    // Start is called before the first frame update
    void Start()
    {
        Button_Exit.onClick.AddListener(() => {
            if(!quizSettingsListener.Equals(null))
            {
                quizSettingsListener.OnExit();
            }
            SceneManager.LoadScene(SceneToReturnTo);
        });
    }

    
}
