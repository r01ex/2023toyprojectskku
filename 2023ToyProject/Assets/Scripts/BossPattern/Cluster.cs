using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// moveSpeed > 1
    /// </summary>
    public void Shoot(int bullets, float moveSpeed, float maxTargetPosOffset)
    {
        Vector3 playerPos = PlayerManager.Instance.transform.position;
        for (int i = 0; i < bullets; i++)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move3Init(moveSpeed+Random.Range(-1f,1f), ((playerPos + new Vector3(Random.Range(-maxTargetPosOffset, maxTargetPosOffset), Random.Range(-maxTargetPosOffset, maxTargetPosOffset), 0)) - spawnPos).normalized);
            }
        }
    }
    public void debug()
    {
        Shoot(10, 4f, 1f);
    }
}
