using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Skill : MonoBehaviour
{
    public Sprite[] skillIcon;

    [SerializeField]
    GameObject skillPanel1;

    [SerializeField]
    GameObject skillPanel2;

    [SerializeField]
    GameObject skillPanel3;

    private int lastSuperUpgrade = -1;

    [SerializeField] float attackBaseInc;
    [SerializeField] float speedBaseInc;
    [SerializeField] float shieldTimeBaseInc;
    [SerializeField] float grabRangeBaseInc;
    [SerializeField] float bulletSizeBaseInc;

    [SerializeField] Text maxbullet;
    [SerializeField] Text attack;
    [SerializeField] Text movespeed;
    [SerializeField] Text shieldduration;
    [SerializeField] Text electronsize;
    [SerializeField] Text nuclearsize;
    [SerializeField] Text grabrange;
    int maxbulletup = 0;
    int attackup = 0;
    int movespeedup = 0;
    int shielddurationup = 0;
    int electronsizeup = 0;
    int nuclearsizeup = 0;
    int grabrangeup = 0;
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
        switch (lastSuperUpgrade)
        {
            case 0:
                PlayerManager.Instance.increaseAttack(-3 * attackBaseInc);
                attackup -= 3;
                break;
            case 1:
                PlayerManager.Instance.increaseSpeed(-3 * speedBaseInc);
                movespeedup -= 3;
                break;
            case 2:
                PlayerManager.Instance.increaseShieldTime(-3 * shieldTimeBaseInc);
                shielddurationup -= 3;
                break;
            case 3:
                PlayerManager.Instance.increaseGrabRange(-3 * grabRangeBaseInc);
                grabrangeup -= 3;
                break;
        }
        lastSuperUpgrade = -1;
        switch (index)
        {
            case 0:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[0];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Reduced Atomic Nucleus";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " nucleus decreases. This will be useful for avoiding patterns.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.decreaseNuclearSize(0.7f); nuclearsizeup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 1:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[1];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Atomic Number";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The size of the atomic" +
                    " nucleus decreases. This will be useful for avoiding patterns.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.addMaxBullet(5); maxbulletup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 2:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[2];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Attack";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Attack power is slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseAttack(attackBaseInc); attackup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 3:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[3];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Increased Movement Speed";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Movement speed is slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseSpeed(speedBaseInc); movespeedup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 4:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[4];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Extended Shield Time";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Shield time slightly increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseShieldTime(shieldTimeBaseInc); shielddurationup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 5:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[5];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Expanded Grab Range";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The grab range is slightly increased." +
                    " This effect lasts until the end of the game.";

                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseGrabRange(grabRangeBaseInc); grabrangeup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });
                
                break;
            case 6:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[6];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Big Bullet";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Bullet size is increased." +
                    " This effect lasts until the end of the game.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseBulletSize(bulletSizeBaseInc); electronsizeup++; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 7:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[7];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Heavy Attack";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Attack power is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseAttack(attackBaseInc*3); });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { lastSuperUpgrade = 0; attackup += 3; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });

                break;
            case 8:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[8];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Lightning Fast";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Movement speed is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseSpeed(speedBaseInc * 3); });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { lastSuperUpgrade = 1; movespeedup += 3; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });
                break;
            case 9:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[9];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Unbreakable shield";
                Panel.transform.Find("Description").GetComponent<Text>().text = "Shield time greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseShieldTime(shieldTimeBaseInc*3); });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { lastSuperUpgrade = 2; shielddurationup += 3; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });
                break;
            case 10:
                Panel.transform.Find("Image").GetComponent<Image>().sprite = skillIcon[10];
                Panel.transform.Find("SkillName").GetComponent<Text>().text = "Magnetic Grab";
                Panel.transform.Find("Description").GetComponent<Text>().text = "The grab range is greatly increased." +
                    " However, this effect is only effective in the next stage.";
                Panel.GetComponent<MultiImageBtn>().onClick.RemoveAllListeners();
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { PlayerManager.Instance.increaseGrabRange(grabRangeBaseInc*3); });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { lastSuperUpgrade = 3; grabrangeup += 3; });
                Panel.GetComponent<MultiImageBtn>().onClick.AddListener(delegate { GameplayManager.Instance.spawnNextBoss(); this.gameObject.SetActive(false); GameplayManager.Instance.isGameOver = false; });
                break;
        }
    }
    public void updateUI()
    {
        maxbullet.text = "+" + maxbulletup;
        attack.text = "+" + attackup;
        movespeed.text = "+" + movespeedup;
        shieldduration.text = "+" + shielddurationup;
        electronsize.text = "+" + electronsizeup;
        nuclearsize.text = "+" + nuclearsizeup;
        grabrange.text = "+" + grabrangeup;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}