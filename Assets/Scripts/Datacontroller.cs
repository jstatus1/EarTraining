using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Datacontroller : MonoBehaviour
{
    public roundData[] allRoundData;
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MenuScreen");
	}

    public roundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
