using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skill : MonoBehaviour
{
    public Sprite[] skillIcon;
    
    // 아래 변수들은 플레이어 스텟 정보입니다.

    private GameObject Player = GameObject.Find("Player");
    private int AutomicNumber = 5;
    private int Attack = 100;
    private int MovementSpeed = 100;
    private int ShieldTime = 100;
    private int GrabRange = 100;
    private int SizeOfBullet = 100;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "SelectingScene")
        {

            int skill1 = 0;
            int skill2 = 0;
            int skill3 = 0;

            while(skill1 == skill2 || skill2 == skill3 || skill3 == skill1)
            {
                skill1 = Random.Range(0, 12);
                skill2 = Random.Range(0, 12);
                skill3 = Random.Range(0, 12);
            }

            UpdateUI(GameObject.Find("SkillPanel1"), skill1);
            UpdateUI(GameObject.Find("SkillPanel2"), skill2);
            UpdateUI(GameObject.Find("SkillPanel3"), skill3);
        }
    }

    void UpdateUI(GameObject Panel, int index)
    {

        switch (index)
        {
            case 0:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[0];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Reduced Atomic Nucleus";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " nucleus decreases. This will be useful for avoiding patterns.";
                break;
            case 1:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[1];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Atomic Number";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " nucleus decreases. This will be useful for avoiding patterns.";
                break;
            case 2:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[2];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Attack";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Attack power is slightly increased." +
                    " This effect lasts until the end of the game.";
                break;
            case 3:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[3];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Movement Speed";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Movement speed is slightly increased." +
                    " This effect lasts until the end of the game.";
                break;
            case 4:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[4];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Extended Shield Time";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Shield time slightly increased." +
                    " This effect lasts until the end of the game.";
                break;
            case 5:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[5];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Extended Grab Range";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The grab range is slightly increased." +
                    " This effect lasts until the end of the game.";
                break;
            case 6:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[6];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Whatever";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Loading..." +
                    " ";
                break;
            case 7:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[7];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Big Bullet";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Bullet size is increased." +
                    " This effect lasts until the end of the game.";
                break;
            case 8:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[8];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Heavy Attack";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Attack power is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                break;
            case 9:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[9];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Lightning Fast";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " However, this effect is only effective in the next stage.";
                break;
            case 10:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[10];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Unbreakable shield";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " However, this effect is only effective in the next stage.";
                break;
            case 11:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[11];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Magnetic Grab";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " However, this effect is only effective in the next stage.";
                break;
        }
    }

    void ReduceSizeOfNucleus()
    {
        Transform atomicNucleus = Player.transform.Find("Nuclear");

        if(atomicNucleus != null) {
            atomicNucleus.localScale.Set(
                atomicNucleus.localScale.x - 0.2f, atomicNucleus.localScale.y - 0.2f, atomicNucleus.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
