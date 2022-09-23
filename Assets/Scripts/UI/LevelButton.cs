using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public int level;
    //public GameObject gameObject;
    public string levelToLoad;
    // Start is called before the first frame update
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


        Debug.Log(levelText);

        SceneManager.LoadScene("SampleScene");

    }
}
