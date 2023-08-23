using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance = null;
    [SerializeField] Image timer;
    [SerializeField] int stageMaxTime;
    float currenttime = 0;

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
            timer.fillAmount = (stageMaxTime - currenttime) / stageMaxTime;
            currenttime += 0.5f;
            if(currenttime >= stageMaxTime)
            {
                //end by time
                Debug.Log("Game Over by Time");
                break;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameOver(){
        isGameOver = true;
        BulletObjectPool.Instance.TurnOffAll();
        GameObject[] pattern = GameObject.FindGameObjectsWithTag("patternset");
        BulletObjectPool.Instance.ChangeAllEnemyBullet(currentBoss + 1);
        foreach(GameObject g in pattern)
        {
            Destroy(g);
        }
        Invoke("ShowGameOverPanel", 0.3f);
    }

    void ShowGameOverPanel(){
        PlayerManager.Instance.setBulletZero();
        bossClearPanel.SetActive(true);
        Debug.Log(bossClearPanel.activeInHierarchy);
        bossClearPanel.GetComponent<Skill>().Init();
    }

    public void spawnNextBoss(){
        currentBoss++;
        Camera.main.transform.rotation = Quaternion.identity;
        Instantiate(bossPrefabs[currentBoss]);
        background.texture = backGroundSprites[currentBoss].texture;
        PlayerManager.Instance.gameObject.transform.position = new Vector3(0, -4, 0);
    } 
}
