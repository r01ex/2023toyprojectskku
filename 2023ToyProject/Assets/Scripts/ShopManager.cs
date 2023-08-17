using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoIntroScene(){
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoStatScene(){
        SceneManager.LoadScene("StatScene");
    }

    public void GoSkillScene(){
        SceneManager.LoadScene("SkillScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
