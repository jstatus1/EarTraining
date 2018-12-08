using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour {

	// Use this for initialization
	
  
   

    public void changemenuscene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void randomlvl()
    {
        SceneManager.LoadScene(Random.Range(1, 4));
    }


    public void randomlvl2()
    {
        SceneManager.LoadScene(Random.Range(5, 8));
        
    }

  

}
