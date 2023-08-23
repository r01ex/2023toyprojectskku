using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagonal : MonoBehaviour
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
        ShootDiagonal(5f, Random.Range(1f, 2.6f), 10, Random.Range(0, 2));
    }

    public void ShootDiagonal(float moveSpeed, float offset, int bulletNum, int directionNum, int stageIndex = 0)
    {
        Vector3 spawnPos = new Vector3(0f, 5.2f + offset, transform.position.z);
        Vector3 midPos = new Vector3(0, 0, 0);
        Vector3 bulletGap = new Vector3(0, 0, 0);

        float bulletGapX = 1.8f / bulletNum * 2;
        float bulletGapY = offset / bulletNum * 2;

        if (directionNum == 0)
        {
            spawnPos += new Vector3(2f, 0, 0);
            midPos += new Vector3(3.8f, 5.2f, 0);
            bulletGap += new Vector3(bulletGapX, -bulletGapY, 0);
        }
        else if (directionNum == 1)
        {
            spawnPos += new Vector3(-2f, 0, 0);
            midPos += new Vector3(-3.8f, 5.2f, 0);
            bulletGap += new Vector3(-bulletGapX, -bulletGapY, 0);
        }

        for (int i = 0; i < bulletNum; i++)
        {
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);

            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move3Init(5f, (PlayerManager.Instance.transform.position - midPos).normalized);
            }

            spawnPos += bulletGap;
        }
    }
}
