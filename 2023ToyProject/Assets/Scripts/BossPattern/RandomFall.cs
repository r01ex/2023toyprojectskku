using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Shoot(float moveSpeedRangelow, float moveSpeedRangehigh, int intervalRangelow, int intervalRangehigh, float volley)
    {
        for(int i=0;i<volley;i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-3.2f,3.2f), transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move1Init(Random.Range(moveSpeedRangelow,moveSpeedRangehigh));
            }
            for (int j = 0; j < Random.Range(intervalRangelow,intervalRangehigh); j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void debug()
    {
        StartCoroutine(Shoot(2f, 4f, 10, 20, 10));
    }
}
