using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wall : MonoBehaviour
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
        StartCoroutine(ShootLines(5f, 4f, 10, 10, 200));
    }

    public IEnumerator ShootLines(float moveSpeed, float width, int bulletNum, int lineNum, int interval)
    {
        float startPos;

        for (int i = 0; i < lineNum; i++)
        {
            startPos = Random.Range(-3.5f, 3.5f - width);
            Vector3 spawnPos = new Vector3(startPos, this.transform.position.y, 0);

            for (int j = 0; j < bulletNum; j++)
            {
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();

                if (enemyObject != null )
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                spawnPos += new Vector3(width / bulletNum, 0, 0);
            }
            for (int k = 0; k < interval; k++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
