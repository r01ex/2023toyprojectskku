using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skill : MonoBehaviour
{
    public Sprite[] skillIcon;
    [SerializeField]
    GameObject skillPanel1;
    [SerializeField]
    GameObject skillPanel2;
    [SerializeField]
    GameObject skillPanel3;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Init()
    {
        int[] arr = new int[skillIcon.Length];
        for (int i = 0; i < skillIcon.Length; i++)
        {
            arr[i] = i;
        }
        for (int i = 0; i < skillIcon.Length; i++)
        {
            int rand = Random.Range(0, skillIcon.Length - 1);
            int tmp = arr[i];
            arr[i] = arr[rand];
            arr[rand] = tmp;
        }
        UpdateUI(skillPanel1, arr[0]);
        UpdateUI(skillPanel2, arr[1]);
        UpdateUI(skillPanel3, arr[2]);
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
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                
                break;
            case 1:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[1];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Atomic Number";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " nucleus decreases. This will be useful for avoiding patterns.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.addMaxBullet(5); });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 2:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[2];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Attack";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Attack power is slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 3:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[3];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Movement Speed";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Movement speed is slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 4:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[4];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Extended Shield Time";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Shield time slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 5:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[5];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Extpanded Grab Range";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The grab range is slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 6:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[6];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Whatever";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Loading..." +
                    " ";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 7:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[7];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Big Bullet";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Bullet size is increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 8:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[8];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Heavy Attack";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Attack power is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 9:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[9];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Lightning Fast";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Movement speed is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 10:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[10];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Unbreakable shield";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Shield time greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
            case 11:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[11];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Magnetic Grab";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The grab range is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); });
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
