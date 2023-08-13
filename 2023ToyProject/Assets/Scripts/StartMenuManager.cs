using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] List<Sprite> stageSpriteList;
    [SerializeField] RawImage stageImagePlaceholder;
    [SerializeField] Button nextstageBtn;
    [SerializeField] Button prevstageBtn;
    [SerializeField] List<string> stageSceneList;
    int number_of_stages;
    int current_stage;
    // Start is called before the first frame update
    void Start()
    {
        current_stage = 0;
        stageImagePlaceholder.texture = stageSpriteList[current_stage].texture;
        number_of_stages = stageSpriteList.Count;
        prevstageBtn.interactable = false;
    }
    public void GoIntroScene(){
        SceneManager.LoadScene("MainMenuScene");
    }
    public void nextbtn()
    {
        if(current_stage<number_of_stages-1)
        {
            current_stage++;
            stageImagePlaceholder.texture = stageSpriteList[current_stage].texture;
            if(current_stage == number_of_stages-1)
            {
                nextstageBtn.interactable = false;
            }
            if (current_stage > 0)
            {
                prevstageBtn.interactable = true;
            }
        }
    }
    public void prevbtn()
    {
        if (current_stage > 0)
        {
            current_stage--;
            stageImagePlaceholder.texture = stageSpriteList[current_stage].texture;
            if (current_stage == 0)
            {
                prevstageBtn.interactable = false;
            }
            if (current_stage < number_of_stages - 1)
            {
                nextstageBtn.interactable = true;
            }
        }
    }
    public void playstageBtn()
    {
        SceneManager.LoadScene(stageSceneList[current_stage]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
