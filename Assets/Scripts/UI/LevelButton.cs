using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;

    
    public TextMeshProUGUI levelText;
    public int level;
    public GameObject confirmPanel;
    //public GameObject gameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonImage=GetComponent<Image>();
        myButton=GetComponent<Button>();
        DecideSprite();
        ShowLevel();
    }

    void DecideSprite(){

        if(isActive){
            buttonImage.sprite=activeSprite;
            myButton.enabled=true;
            levelText.enabled=true;
        } else {
            buttonImage.sprite=lockedSprite;
            myButton.enabled=false;
            levelText.enabled=false;
        }
    }

    void ShowLevel(){

        levelText.text="" + level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmPanel(int level){

        confirmPanel.GetComponent<ConfirmPanel>().level=level;
        confirmPanel.SetActive(true);
    }
    
}
