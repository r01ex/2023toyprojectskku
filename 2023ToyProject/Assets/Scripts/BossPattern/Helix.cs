using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour
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
        StartCoroutine(ShootDouble(6, 140, 10, 10, 0.35f, 1.8f, true));
    }

    /// <summary>
    /// interval�� �߻�ӵ�(���� ������ ����), widthNumber�� ������ �ִ� ź��*2, width_separation�� ź�� ���� ����, diminisher_mult�� �ձ۾����� ���� 0-2���̰�, ismiddle�� �߾ӻ������� 
    /// </summary>
    public IEnumerator ShootSingle(float moveSpeed, int totalbullet, int interval, float offset, int widthNumber, float width_separation,float diminisher_mult, bool ismiddle, int stageIndex = 0)
    {
        float startPos;
        if (ismiddle)
        {
            startPos = 0;
        }
        else 
        {
            startPos = Random.Range(-2.5f + (widthNumber * width_separation), 2.5f - (widthNumber * width_separation));
        }
        for (int i = 0; i < (totalbullet / widthNumber) / 2; i++)
        {
            Vector3 spawnPos = new Vector3(startPos + offset, this.transform.position.y, 0);
            float diminisher = 0;
            for (int j = 0; j < widthNumber / 2; j++)
            {
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                spawnPos += new Vector3(width_separation - diminisher, 0, 0);
                diminisher += diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            for (int j = 0; j < widthNumber / 2; j++)
            {
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                spawnPos -= new Vector3(width_separation - diminisher, 0, 0);
                diminisher -= diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            for (int j = 0; j < widthNumber / 2; j++)
            {
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                spawnPos -= new Vector3(width_separation - diminisher, 0, 0);
                diminisher += diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            for (int j = 0; j < widthNumber / 2; j++)
            {
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageIndex);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                spawnPos += new Vector3(width_separation - diminisher, 0, 0);
                diminisher -= diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
    /// <summary>
    /// interval�� �߻�ӵ�(���� ������ ����), widthNumber�� ������ �ִ� ź��*2, width_separation�� ź�� ���� ����, diminisher_mult�� �ձ۾����� ���� 0-2���̰�, ismiddle�� �߾ӻ������� 
    /// </summary>
    public IEnumerator ShootDouble(float moveSpeed, int totalbullet, int interval, int widthNumber, float width_separation,float diminisher_mult, bool ismiddle, int stageNumber = 0)
    {
        float startPos;
        if (ismiddle)
        {
            startPos = 0;
        }
        else
        {
            startPos = Random.Range(-2.5f + (widthNumber * width_separation), 2.5f - (widthNumber * width_separation));
        }
        for (int i = 0; i < (totalbullet / widthNumber) / 2; i++)
        {
            Vector3 spawnPos1 = new Vector3(startPos, this.transform.position.y, 0);
            Vector3 spawnPos2 = new Vector3(startPos, this.transform.position.y, 0);
            float diminisher = 0;
            for (int j = 0; j < widthNumber / 2; j++)
            {
                spawnPos1 += new Vector3(width_separation - diminisher, 0, 0);
                spawnPos2 -= new Vector3(width_separation - diminisher, 0, 0);
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos1;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos2;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                diminisher += diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            diminisher -= diminisher_mult * width_separation / widthNumber;
            for (int j = 0; j < widthNumber / 2; j++)
            {
                spawnPos1 -= new Vector3(width_separation - diminisher, 0, 0);
                spawnPos2 += new Vector3(width_separation - diminisher, 0, 0);
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos1;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos2;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                diminisher -= diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            diminisher += diminisher_mult * width_separation / widthNumber;
            for (int j = 0; j < widthNumber / 2; j++)
            {
                spawnPos1 -= new Vector3(width_separation - diminisher, 0, 0);
                spawnPos2 += new Vector3(width_separation - diminisher, 0, 0);
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos1;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos2;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                diminisher += diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            diminisher -= diminisher_mult * width_separation / widthNumber;
            for (int j = 0; j < widthNumber / 2; j++)
            {
                spawnPos1 += new Vector3(width_separation - diminisher, 0, 0);
                spawnPos2 -= new Vector3(width_separation - diminisher, 0, 0);
                GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos1;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet(stageNumber);
                if (enemyObject != null)
                {
                    enemyObject.transform.position = spawnPos2;
                    enemyObject.SetActive(true);
                    EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                    enemy.move1Init(moveSpeed);
                }
                diminisher -= diminisher_mult * width_separation / widthNumber;
                for (int k = 0; k < interval; k++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
