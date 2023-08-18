using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MonoBehaviour
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
        StartCoroutine(Shoot(6, 200, 30, 20));
    }
    public IEnumerator Shoot(float moveSpeed, int totalbullet, int interval, float angle_increment)
    {
        float angle = 0f;
        for (int i = 0; i < totalbullet; i++)
        {
            float dirx = this.transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
            float diry = this.transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);
            Vector3 movedir = (new Vector3(dirx, diry, 0) - this.transform.position).normalized;
            Vector3 spawnPos = this.transform.position;
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move3Init(moveSpeed, movedir);
            }
            angle += angle_increment;
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
