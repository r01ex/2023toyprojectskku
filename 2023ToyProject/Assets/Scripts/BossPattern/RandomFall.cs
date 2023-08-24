using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFall : MonoBehaviour
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
            yield return new WaitForSeconds(Random.Range(intervalRangelow, intervalRangehigh) / targetframe);
        }
    }
    public void debug()
    {
        StartCoroutine(Shoot(2f, 4f, 10, 20, 10));
    }
}
