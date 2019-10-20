using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
/// main controls for the front screen
////</summary>
public class MainPageControl : MonoBehaviour
{
    [SerializeField] Button Button_EarTraining;
    // Start is called before the first frame update
    void Start()
    {
        SetButton();
    }


    void SetButton()
    {
        Button_EarTraining.onClick.AddListener(() => {
            SceneManager.LoadScene("3_PreConfiguration_EarTraining");
        });
    }
}
