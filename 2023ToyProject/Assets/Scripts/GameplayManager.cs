using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance = null;
    [SerializeField] int NofBosses;

    [SerializeField] Image timer;
    [SerializeField] int stageMaxTime;
    int currenttime = 0;

    [SerializeField]
    private GameObject bossClearPanel;

    [SerializeField]
    RawImage background;
    [HideInInspector]
    public bool isGameOver = false;

    int currentBoss=0;
    [SerializeField]
    GameObject[] bossPrefabs;
    [SerializeField]
    Sprite[] backGroundSprites;

    [SerializeField]
    AudioSource bgm;

    [SerializeField]
    TextMeshProUGUI bossname;
    [SerializeField]
    TextMeshProUGUI stagenum;
    [SerializeField]
    string[] bossnamelist;

    [SerializeField]
    GameObject defeatCanvas;
    [SerializeField]
    TextMeshProUGUI bossname_defeat_text;
    [SerializeField]
    TextMeshProUGUI remaintime_defeat_text;
    [SerializeField]
    TextMeshProUGUI totaltime_defeat_text;
    [SerializeField]
    GameObject clearCanvas;
    [SerializeField]
    TextMeshProUGUI totalAbsBullet_text;
    [SerializeField]
    TextMeshProUGUI totalShieldBullet_text;
    [SerializeField]
    TextMeshProUGUI totaltime_clear_text;
    int totaltimer = 0;
    public int totalAbsBullet = 0;
    public int totalShieldBullet = 0;

    // Start is called before the first frame update

    private void Awake() {
        Application.targetFrameRate = 120;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(startTimer());
    }
    IEnumerator startTimer()
    {
        while(true)
        {
            if (isGameOver == false)
            {
                timer.fillAmount = (float)(stageMaxTime - (float)(currenttime/2)) / stageMaxTime;
                currenttime += 1;
                if ((currenttime / 2) >= stageMaxTime)
                {
                    //end by time
                    Debug.Log("Game Over by Time");
                    showDefeat();
                    break;
                }
                totaltimer += 1;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameOver(){
        isGameOver = true;
        bgm.volume = 0.25f;
        BulletObjectPool.Instance.TurnOffAll();
        GameObject[] pattern = GameObject.FindGameObjectsWithTag("patternset");
        foreach(GameObject g in pattern)
        {
            Destroy(g);
        }
        if (NofBosses <= currentBoss+1)
        {
            showClear();
        }
        else
        {
            Invoke("ShowGameOverPanel", 0.3f);
            BulletObjectPool.Instance.ChangeAllEnemyBullet(currentBoss + 1);
        }
    }

    void ShowGameOverPanel(){
        PlayerManager.Instance.setBulletZero();
        bossClearPanel.SetActive(true);
        Debug.Log(bossClearPanel.activeInHierarchy);
        bossClearPanel.GetComponent<Skill>().Init();
    }

    public void spawnNextBoss(){
        currentBoss++;
        bossClearPanel.GetComponent<Skill>().updateUI();
        timer.fillAmount = 1;
        currenttime = 0;
        bgm.volume = 0.5f;
        Camera.main.transform.rotation = Quaternion.identity;
        Instantiate(bossPrefabs[currentBoss]);
        background.texture = backGroundSprites[currentBoss].texture;
        PlayerManager.Instance.gameObject.transform.position = new Vector3(0, -4, 0);
        bossname.text = bossnamelist[currentBoss];
        stagenum.text = "Stage " + (int)(currentBoss + 1);
    } 
    public void showDefeat()
    {
        isGameOver = true;
        PlayerManager.Instance.instantShieldoff();
        BulletObjectPool.Instance.TurnOffAll();
        GameObject[] pattern = GameObject.FindGameObjectsWithTag("patternset");
        BulletObjectPool.Instance.ChangeAllEnemyBullet(currentBoss);
        foreach (GameObject g in pattern)
        {
            Destroy(g);
        }
        Time.timeScale = 0;
        bgm.volume = 0.25f;
        defeatCanvas.SetActive(true);
        bossname_defeat_text.text = bossnamelist[currentBoss];
        TimeSpan result = TimeSpan.FromSeconds(currenttime/2);
        string fromTimeString = result.ToString("mm':'ss");
        remaintime_defeat_text.text = fromTimeString;
        TimeSpan result2 = TimeSpan.FromSeconds(totaltimer/2);
        string fromTimeString2 = result2.ToString("mm':'ss");
        totaltime_defeat_text.text = fromTimeString2;
    }
    public void showClear()
    {
        SoundEffectManager.Instance.PlayVictory();
        BulletObjectPool.Instance.TurnOffAll();
        GameObject[] pattern = GameObject.FindGameObjectsWithTag("patternset");
        BulletObjectPool.Instance.ChangeAllEnemyBullet(currentBoss);
        foreach (GameObject g in pattern)
        {
            Destroy(g);
        }
        clearCanvas.SetActive(true);
        bgm.volume = 0.25f;
        totalAbsBullet_text.text = totalAbsBullet.ToString();
        totalShieldBullet_text.text = totalShieldBullet.ToString();
        TimeSpan result2 = TimeSpan.FromSeconds(totaltimer / 2);
        string fromTimeString2 = result2.ToString("mm':'ss");
        totaltime_clear_text.text = fromTimeString2;
    }
    public void retryBoss()
    {
        PlayerManager.Instance.instantShieldoff();
        isGameOver = false;
        BulletObjectPool.Instance.TurnOffAll();
        GameObject[] pattern = GameObject.FindGameObjectsWithTag("patternset");
        BulletObjectPool.Instance.ChangeAllEnemyBullet(currentBoss);
        foreach (GameObject g in pattern)
        {
            Destroy(g);
        }
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
        timer.fillAmount = 1;
        totaltimer -= currenttime * 2;
        currenttime = 0;
        bgm.volume = 0.5f; 
        Camera.main.transform.rotation = Quaternion.identity;
        Instantiate(bossPrefabs[currentBoss]);
        background.texture = backGroundSprites[currentBoss].texture;
        PlayerManager.Instance.gameObject.transform.position = new Vector3(0, -4, 0);
        bossname.text = bossnamelist[currentBoss];
        stagenum.text = "Stage " + (int)(currentBoss + 1);
        defeatCanvas.SetActive(false);
        PlayerManager.Instance.setBulletZero();
        Time.timeScale = 1;
    }
    public void toMainScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void retryScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
