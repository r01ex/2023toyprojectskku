using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

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

    public void GoShopScene(){
        SceneManager.LoadScene("ShopScene");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown){
            SceneManager.LoadScene(nextSceneName);
        }
        
    }
}
