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
                enemy.move3Init(moveSpeed, PlayerManager.Instance.transform.position-this.transform.position);
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
            enemy.move3Init(moveSpeed, PlayerManager.Instance.transform.position - this.transform.position);
        }
    }
    public IEnumerator ShootWithSignaling(int volley, float moveSpeed, int signalFrame, int interval)
    {
        for(int i=0;i<volley;i++)
        {
            LineRenderer lr = Instantiate(lrPrefab).GetComponent<LineRenderer>();
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 playerSnapshot = PlayerManager.Instance.transform.position;
            lr.positionCount = 2;
            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, this.transform.position + (playerSnapshot - this.transform.position).normalized*30);
            lr.startColor = new Color(1, 0, 0, 0.8f);
            lr.endColor = new Color(1, 0, 0, 0.8f);
            Color target = new Color(0, 0, 0, 0);
            for (int j = 0; j < signalFrame; j++)
            {
                float progress = (float)j / signalFrame;
                Color current = Color.Lerp(new Color(1, 0, 0, 0.8f), target, progress);
                lr.startColor = current;
                lr.endColor = current;
                yield return new WaitForEndOfFrame();
            }
            Destroy(lr.gameObject);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move3Init(moveSpeed, playerSnapshot - spawnPos);
            }
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void debug()
    {
        //StartCoroutine(ShootWithSignaling(5, 8, 200, 20));
        StartCoroutine(ShootVolley(5, 5, 50));
    }
}
