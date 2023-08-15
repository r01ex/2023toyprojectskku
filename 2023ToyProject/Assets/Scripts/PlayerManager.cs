using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int maxBullet;
    int currentBullet = 0;
    [SerializeField] Image bulletcircle;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addbullet(int addAmount)
    {
        currentBullet = Mathf.Clamp(currentBullet + addAmount, 0, maxBullet);
        bulletcircle.fillAmount = (float)currentBullet / (float)maxBullet;
        if(currentBullet == maxBullet)
        {
            onbulletfull();
        }
    }
    public void onbulletfull()
    {
        //TODO
    }
}
