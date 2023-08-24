using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NH3Gimmick : MonoBehaviour
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
    /// <summary>
    /// offsetY´Â 3.5~1»çÀÌ
    /// </summary>
    void spawn(float speed, float spawnY, float accel)
    {
        Vector3 spawnPos = new Vector3(3.55f, spawnY, transform.position.z);
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            enemyObject.GetComponent<EnemyBullet>().move4Init(speed, Vector2.left, Vector2.down, accel);
        }
        spawnPos = new Vector3(-3.55f, spawnY, transform.position.z);
        enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            enemyObject.GetComponent<EnemyBullet>().move4Init(speed, Vector2.right, Vector2.down, accel);
        }
    }
    public IEnumerator Shoot(float volley, float interval)
    {
        float xspeed = Random.Range(2f, 4f);
        float yoffset = Random.Range(1f, 3.5f);
        for (int i = 0; i < volley; i++)
        {
            spawn(xspeed, yoffset, 10);
            yield return new WaitForSeconds(interval / targetframe);
        }
    }
    public void debug()
    {
        StartCoroutine(Shoot(8f, 20));
    }
}
