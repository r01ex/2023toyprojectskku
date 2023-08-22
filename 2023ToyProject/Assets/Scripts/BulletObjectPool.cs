using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public List<GameObject> pulledPlayerBullets;
    public List<GameObject> pulledEnemyBullets;
    [SerializeField] GameObject playerBulletToPool;
    [SerializeField] GameObject enemyBulletToPool;
    public int amountToPool=100;
    [SerializeField]
    EnemyBulletSoundManager enemyBulletSoundManager;

    #region singleton
    public static BulletObjectPool Instance;
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
    #endregion
    void Start()
    {
        pulledPlayerBullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(playerBulletToPool);
            tmp.SetActive(false);
            pulledPlayerBullets.Add(tmp);
        }
        pulledEnemyBullets = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(enemyBulletToPool);
            tmp.SetActive(false);
            pulledEnemyBullets.Add(tmp);
        }
    }
    public GameObject GetPooledPlayerBullet()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pulledPlayerBullets[i].activeInHierarchy)
            {
                return pulledPlayerBullets[i];
            }
        }
        return null;
    }
    public GameObject GetPooledEnemyBullet(int stageNumber)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pulledEnemyBullets[i].activeInHierarchy)
            {
                enemyBulletSoundManager.PlaySound("ATTACK", stageNumber);
                pulledEnemyBullets[i].transform.GetChild(0).gameObject.GetComponent<EnemybulletInner>().isHit = false;
                return pulledEnemyBullets[i];
            }
        }
        return null;
    }

    public GameObject GetPooledEnemyBullet()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pulledEnemyBullets[i].activeInHierarchy)
            {
                enemyBulletSoundManager.PlaySound("ATTACK", 1);
                pulledEnemyBullets[i].transform.GetChild(0).gameObject.GetComponent<EnemybulletInner>().isHit = false;
                return pulledEnemyBullets[i];
            }
        }
        return null;
    }
    public void TurnOffAll()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            pulledEnemyBullets[i].SetActive(false);           
        }
        for (int i = 0; i < amountToPool; i++)
        {
            pulledPlayerBullets[i].SetActive(false);
        }
    }
}
