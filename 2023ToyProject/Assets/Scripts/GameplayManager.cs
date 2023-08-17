using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance = null;
    [SerializeField] Image timer;
    [SerializeField] int stageMaxTime;
    float currenttime = 0;

    [SerializeField]
    private GameObject gameOverPanel;

    [HideInInspector]
    public bool isGameOver = false;
    // Start is called before the first frame update

    private void Awake() {
        if(instance == null){
            instance = this;
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
        Shoot5Row enemySpawner = FindObjectOfType<Shoot5Row>();
        if(enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
    }

    public void stageClear(){
        
    } 
}
