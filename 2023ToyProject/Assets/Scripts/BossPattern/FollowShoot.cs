using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move5Init(moveSpeed);
        }
    }
    public void debug()
    {
        Shoot(4);
    }
}
