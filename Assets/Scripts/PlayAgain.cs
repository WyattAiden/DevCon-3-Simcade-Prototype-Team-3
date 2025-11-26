using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Wyatt Sobota 000952105

public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sends you to the first level
      if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("stage 1");
        }
    }
    
}
