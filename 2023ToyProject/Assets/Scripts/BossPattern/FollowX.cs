using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowX : MonoBehaviour
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
    public IEnumerator Shoot(float followSpeed, float volley, int interval, float fallSpeed)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move7Init(PlayerManager.Instance.gameObject, followSpeed, fallSpeed);
        }
        yield return new WaitForSeconds(interval / targetframe);
        for (int i = 0; i < volley-1; i++)
        {
            GameObject newEnemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (newEnemyObject != null)
            {
                newEnemyObject.transform.position = spawnPos;
                newEnemyObject.SetActive(true);
                EnemyBullet enemy = newEnemyObject.GetComponent<EnemyBullet>();
                enemy.move7Init(enemyObject, followSpeed, fallSpeed);
                enemyObject = newEnemyObject;
            }
            yield return new WaitForSeconds(interval / targetframe);
        }
    }
    public void debug()
    {
        StartCoroutine(Shoot(8, 8, 20, 5));
    }
}
