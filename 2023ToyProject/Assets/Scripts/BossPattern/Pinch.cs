using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinch : MonoBehaviour
{
    [SerializeField]
    Vector3 lowEndRight = new Vector3(4, -5.3f, 0);
    [SerializeField]
    Vector3 lowEndLeft = new Vector3(-4, -5.3f, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void debug()
    {
        StartCoroutine(MapShoot(6, 20,30));
    }
    public IEnumerator MapShoot(float moveSpeed, int volley, int interval) //6,20
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 spawnPos = this.transform.position;
            GameObject enemyObjectRight = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObjectRight != null)
            {
                enemyObjectRight.transform.position = spawnPos + new Vector3(0.3f, 0, 0);
                enemyObjectRight.SetActive(true);
                EnemyBullet enemy = enemyObjectRight.GetComponent<EnemyBullet>();
                enemy.move2Init(moveSpeed, lowEndRight + new Vector3((-i-1) * (5f / volley), 0, 0));
            }
            GameObject enemyObjectLeft = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObjectLeft != null)
            {
                enemyObjectLeft.transform.position = spawnPos + new Vector3(-0.3f, 0, 0);
                enemyObjectLeft.SetActive(true);
                EnemyBullet enemy = enemyObjectLeft.GetComponent<EnemyBullet>();
                enemy.move2Init(moveSpeed, lowEndLeft + new Vector3((i + 1) * (5f / volley), 0, 0));
            }
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator MapShoot(float moveSpeed, int volley, int interval, int stageIndex) //6,20
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 spawnPos = this.transform.position;
            GameObject enemyObjectRight = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);
            if (enemyObjectRight != null)
            {
                enemyObjectRight.transform.position = spawnPos + new Vector3(0.3f, 0, 0);
                enemyObjectRight.SetActive(true);
                EnemyBullet enemy = enemyObjectRight.GetComponent<EnemyBullet>();
                enemy.move2Init(moveSpeed, lowEndRight + new Vector3((-i-1) * (5f / volley), 0, 0));
            }
            GameObject enemyObjectLeft = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);
            if (enemyObjectLeft != null)
            {
                enemyObjectLeft.transform.position = spawnPos + new Vector3(-0.3f, 0, 0);
                enemyObjectLeft.SetActive(true);
                EnemyBullet enemy = enemyObjectLeft.GetComponent<EnemyBullet>();
                enemy.move2Init(moveSpeed, lowEndLeft + new Vector3((i + 1) * (5f / volley), 0, 0));
            }
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

