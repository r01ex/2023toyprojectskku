using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    float targetframe;
    // Start is called before the first frame update
    void Start()
    {
        targetframe = Application.targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Shoot(int volley, int dropSpeed, float bounceBulletSpeed, int interval)
    {
        for (int i = 0; i < volley; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-3f, 3f), transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move1Init(dropSpeed);
            }
            Vector3 lastBulletPos = new Vector3();
            while (enemyObject.activeInHierarchy)
            {
                lastBulletPos = enemyObject.transform.position;
                 yield return null;
            }
            if (lastBulletPos.y <= -5)
            {
                GameObject bounceBullet = BulletObjectPool.Instance.GetPooledEnemyBullet();
                if (bounceBullet != null)
                {
                    bounceBullet.transform.position = new Vector3(lastBulletPos.x, -5); ;
                    bounceBullet.SetActive(true);
                    EnemyBullet enemy = bounceBullet.GetComponent<EnemyBullet>();
                    float randomY = Random.Range(1f, 5f);
                    int randomX = ((Random.Range(0, 2) % 2) * 2) - 1;
                    enemy.move4Init(bounceBulletSpeed, new Vector2(randomX, randomY).normalized, new Vector2(0, -1), 10);
                }
            }
            yield return new WaitForSeconds(interval / targetframe);
        }
    }
    public void debug()
    {
        StartCoroutine(Shoot(5, 7, 10, 40));
    }
}
