using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snipe : MonoBehaviour
{
    [SerializeField]
    GameObject lrPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShootVolley(int volley, float moveSpeed, float interval)
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move3Init(moveSpeed, (PlayerManager.Instance.transform.position-this.transform.position).normalized);
            }
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void ShootSingle(float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move3Init(moveSpeed, (PlayerManager.Instance.transform.position - this.transform.position).normalized);
        }
    }
    public IEnumerator ShootWithSignaling(int volley, float moveSpeed, int signalFrame, int interval)
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 playerSnapshot = PlayerManager.Instance.transform.position;
            StartCoroutine(spawnLine(this.transform.position, this.transform.position + (playerSnapshot - this.transform.position).normalized * 30, signalFrame, moveSpeed));
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator RandomvolleyWithSignal(int volley, float moveSpeed, int signalFrame, int interval, int stageindex = 0)
    {
        for (int i = 0; i < volley; i++)
        {            
            StartCoroutine(spawnLine(new Vector3(-4.5f,Random.Range(-4f,4f),0), new Vector3(4.5f, Random.Range(-4f, 4f), 0), signalFrame, moveSpeed));   
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine(spawnLine(new Vector3(Random.Range(-3f, 3f), 5.5f, 0), new Vector3(Random.Range(-3f, 3f), -5.5f, 0), signalFrame, moveSpeed));
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator spawnLine(Vector3 pos1, Vector3 pos2, int signalFrame, float moveSpeed)
    {
        LineRenderer lr = Instantiate(lrPrefab).GetComponent<LineRenderer>();
        lr.transform.parent = this.transform;
        lr.positionCount = 2;
        lr.SetPosition(0, pos1);
        lr.SetPosition(1, pos2);
        Color targetcolor = new Color(1, 0, 0, 0.8f);
        Color startcolor = new Color(1, 0, 0, 0.1f);
        lr.startColor = startcolor;
        lr.endColor = startcolor;
        for (int j = 1; j <= signalFrame; j++)
        {
            float progress = (float)j / signalFrame;
            Color current = Color.Lerp(startcolor, targetcolor, progress);
            lr.startColor = current;
            lr.endColor = current;
            yield return new WaitForEndOfFrame();
        }
        Destroy(lr.gameObject);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = pos1;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move3Init(moveSpeed, (pos2 - pos1).normalized);
        }
    }
    public void debug()
    {
        //StartCoroutine(ShootWithSignaling(5, 25, 200, 200));
        //StartCoroutine(ShootVolley(5, 5, 50));
        StartCoroutine(RandomvolleyWithSignal(10, 30, 500, 40));
    }
}
