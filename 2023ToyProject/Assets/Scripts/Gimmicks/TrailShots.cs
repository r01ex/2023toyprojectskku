using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailShots : MonoBehaviour
{
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
        //SnipeShootSingleDisappearAtOnce(4, 20, 80);
        StartCoroutine(RandomVolleyDisappearAtOnce(5f, 7f, 10, 20, 5, 40, 80));
    }
    public void debug2()
    {
        //SnipeShootSingle(4, 20, 800);
        StartCoroutine(RandomVolley(5f, 7f, 10, 20, 5, 40, 800));
    }
    IEnumerator LeaveTrail(GameObject bullet, int interval, int duration)
    {
        while (bullet.activeInHierarchy)
        {
            for(int i=0;i<interval;i++)
            {
                if(!bullet.activeInHierarchy)
                {
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = bullet.transform.position;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move6Init(duration);
            }
        }
    }
    IEnumerator LeaveTrailDisappearAtOnce(GameObject bullet, int interval, int duration)
    {
        List<GameObject> trailList = new List<GameObject>();
        while (bullet.activeInHierarchy)
        {
            for (int i = 0; i < interval; i++)
            {
                if (!bullet.activeInHierarchy)
                {
                    goto exitCoroutine;
                }
                yield return new WaitForEndOfFrame();
            }
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            trailList.Add(enemyObject);
            if (enemyObject != null)
            {
                enemyObject.transform.position = bullet.transform.position;
                enemyObject.SetActive(true);
            }
        }
    exitCoroutine:
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        foreach (GameObject trailbullet in trailList)
        {
            trailbullet.SetActive(false);
        }
    }
    public void SnipeShootSingleDisappearAtOnce(float moveSpeed, int trailInterval, int trailDuration)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move3Init(moveSpeed, (PlayerManager.Instance.transform.position - this.transform.position).normalized);
            StartCoroutine(LeaveTrailDisappearAtOnce(enemyObject, trailInterval, trailDuration));
        }
    }
    public void SnipeShootSingle(float moveSpeed, int trailInterval, int trailDuration)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move3Init(moveSpeed, (PlayerManager.Instance.transform.position - this.transform.position).normalized);
            StartCoroutine(LeaveTrail(enemyObject, trailInterval, trailDuration));
        }
    }
    public IEnumerator RandomVolley(float moveSpeedRangelow, float moveSpeedRangehigh, int intervalRangelow, int intervalRangehigh, float volley, int trailInterval, int trailDuration)
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-3.2f, 3.2f), transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move1Init(Random.Range(moveSpeedRangelow, moveSpeedRangehigh));
                StartCoroutine(LeaveTrail(enemyObject, trailInterval, trailDuration));
            }
            for (int j = 0; j < Random.Range(intervalRangelow, intervalRangehigh); j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator RandomVolleyDisappearAtOnce(float moveSpeedRangelow, float moveSpeedRangehigh, int intervalRangelow, int intervalRangehigh, float volley, int trailInterval, int trailDuration)
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-3.2f, 3.2f), transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move1Init(Random.Range(moveSpeedRangelow, moveSpeedRangehigh));
                StartCoroutine(LeaveTrailDisappearAtOnce(enemyObject, trailInterval, trailDuration));
            }
            for (int j = 0; j < Random.Range(intervalRangelow, intervalRangehigh); j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
