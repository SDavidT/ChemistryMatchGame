using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ConfirmPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelToLoad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel(){

        this.gameObject.SetActive(false);
    }

    public void Play(){


        //Debug.Log(levelText);

        SceneManager.LoadScene(levelToLoad);

    }
}
