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
        //StartCoroutine(Shoot4(5, 40, 10, 20));
        //StartCoroutine(ShootBackAndForth(3.5f, 8, 10, 15, 4));
        allAroundShotgunSingle(6, 40);
    }
    public IEnumerator Shoot(float moveSpeed, int totalbullet, int interval, float angleOffset, float angle_increment)
    {
        float angle = 0f + angleOffset;
        for (int i = 0; i < totalbullet; i++)
        {
            float dirx = this.transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
            float diry = this.transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);
            Vector3 movedir = (new Vector3(dirx, diry, 0) - this.transform.position);
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
    public IEnumerator Shoot4(float moveSpeed, int totalbullet, int interval, float angleOffset, float angle_increment)
    {
        float angle = 0f + angleOffset;
        for (int i = 0; i < totalbullet; i++)
        {
            for (int wing = 0; wing < 4; wing++)
            {
                float dirx = this.transform.position.x + Mathf.Cos(((angle + 90 * wing) * Mathf.PI) / 180f);
                float diry = this.transform.position.y + Mathf.Sin(((angle + 90 * wing) * Mathf.PI) / 180f);
                Vector3 movedir = (new Vector3(dirx, diry, 0) - this.transform.position);
                Vector3 spawnPos = this.transform.position;
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move3Init(moveSpeed, movedir);
                }
            }
            angle += angle_increment;
            for (int j = 0; j < interval; j++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator ShootBackAndForth(float moveSpeed, int bulletsInOneVolley, int interval, float angle_increment,int totalBackandForth)
    {
        float angle = -90f - (bulletsInOneVolley * angle_increment / 2);
        for (int k = 0; k < totalBackandForth; k++)
        {
            for (int i = 0; i < bulletsInOneVolley; i++)
            {
                float dirx = this.transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
                float diry = this.transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);
                Vector3 movedir = (new Vector3(dirx, diry, 0) - this.transform.position);
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
            for (int i = 0; i < bulletsInOneVolley; i++)
            {
                float dirx = this.transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
                float diry = this.transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);
                Vector3 movedir = (new Vector3(dirx, diry, 0) - this.transform.position);
                Vector3 spawnPos = this.transform.position;
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move3Init(moveSpeed, movedir);
                }
                angle -= angle_increment;
                for (int j = 0; j < interval; j++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
    public void allAroundShotgunSingle(float moveSpeed, int totalbullet)
    {
        float angle = 0f;
        for (int i = 0; i < totalbullet; i++)
        {
            float dirx = this.transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
            float diry = this.transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);
            Vector3 movedir = (new Vector3(dirx, diry, 0) - this.transform.position);
            Vector3 spawnPos = this.transform.position;
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move3Init(moveSpeed, movedir);
            }
            angle += 360f/totalbullet;
        }
    }
}
