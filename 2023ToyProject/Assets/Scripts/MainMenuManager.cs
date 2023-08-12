using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }    
    public void GoStartMenuScene(){
        SceneManager.LoadScene("StartMenuScene");
    }

    public void GoSettingScene(){
        SceneManager.LoadScene("SettingScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
