using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSplash : MonoBehaviour
{
    public string sceneLoad;
    private Board board;
    
    //public World world;
    int nextLevel;
    int lengthLevel;
    // Start is called before the first frame update
    void Start()
    {
        //world=FindObjectOfType<World>();
        board=FindObjectOfType<Board>();
        
    }
    public void OK(){
        SceneManager.LoadScene(sceneLoad);
    }

    public void ReloadLevel(){
        SceneManager.LoadScene("Levels");
    }

    public void NextLevel(){
    
        nextLevel=board.level+1;
        lengthLevel=board.lengthLevels;
        
        if(nextLevel<lengthLevel){

            PlayerPrefs.SetInt("Current Level",nextLevel);
            SceneManager.LoadScene("Levels");
        }else{
            SceneManager.LoadScene("SelectLevel");
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
