using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomBomb : MonoBehaviour
{
    [SerializeField] GameObject AtomBombPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Shoot(int dropSpeed, float splinterScaleSpeed)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-1.5f, 1.5f), transform.position.y, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            enemy.move1Init(dropSpeed);
        }
        Vector3 lastBombPos = new Vector3();
        while (enemyObject.activeInHierarchy)
        {
            lastBombPos = enemyObject.transform.position;
             yield return null;
        }
        if (lastBombPos.y <= -5)
        {
            GameObject nukeCloud = Instantiate(AtomBombPrefab);
            nukeCloud.transform.position = new Vector3(lastBombPos.x, -5);
            nukeCloud.GetComponent<AtomBombForPrefab>().Init(splinterScaleSpeed);
        }
    }
    public void debug()
    {
        StartCoroutine(Shoot(7, 1.5f));
    }
}
