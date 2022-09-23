using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSplash : MonoBehaviour
{
    public string sceneLoad;
    // Start is called before the first frame update
    public void OK(){
        SceneManager.LoadScene(sceneLoad);
    }
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
