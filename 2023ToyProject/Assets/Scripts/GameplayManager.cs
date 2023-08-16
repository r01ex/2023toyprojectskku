using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayManager : MonoBehaviour
{
    [SerializeField] Image timer;
    [SerializeField] int stageMaxTime;
    float currenttime = 0;
    // Start is called before the first frame update
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

    public void stageClear(){
        
    } 
}
