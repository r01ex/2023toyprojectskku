using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
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
        StartCoroutine(Shoot(100, 200, 5, 12, 4, 3, 4, 30));
    }
    public IEnumerator Shoot(int dropframelow, int dropframehigh, float dropSpeed, int splitNumber, float splitSpeed, float volley, float splitvolley,float splitvolleyinterval)
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
            for (int j = 0; j < Random.Range(dropframelow, dropframehigh); j++)
            {
                 yield return null;
            }
            if (enemyObject.activeInHierarchy == true)
            {
                Vector3 explodePos = enemyObject.transform.position;
                enemyObject.SetActive(false);
                float angle = Random.Range(0f, 180f);
                for (int n = 0; n < splitvolley; n++)
                {
                    for (int j = 0; j < splitNumber; j++)
                    {
                        float dirx = Mathf.Cos((angle * Mathf.PI) / 180f);
                        float diry =  Mathf.Sin((angle * Mathf.PI) / 180f);
                        Vector3 movedir = new Vector3(dirx, diry, 0).normalized;
                        GameObject splinter = BulletObjectPool.Instance.GetPooledEnemyBullet();
                        if (splinter != null)
                        {
                            splinter.transform.position = explodePos;
                            splinter.SetActive(true);
                            EnemyBullet enemy = splinter.GetComponent<EnemyBullet>();
                            enemy.move3Init(splitSpeed, movedir);
                        }
                        angle += 360/splitNumber;
                    }
                    for (int j = 0; j < splitvolleyinterval; j++)
                    {
                         yield return null;
                    }
                }
            }
        }
    }
}
