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
    public int level;
    public TextMeshProUGUI introlevel;
   

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        introlevel.text = "LEVEL" + " " + level;
    }

    public void Cancel(){

        this.gameObject.SetActive(false);
    }

    public void Play(){


        PlayerPrefs.SetInt("Current Level",level-1);

        SceneManager.LoadScene(levelToLoad);

    }

    public void nextLevel(){

        PlayerPrefs.SetInt("Current Level",level+1);
        SceneManager.LoadScene(levelToLoad);
    }
}
