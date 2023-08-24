using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
public class ShotGun5 : MonoBehaviour
{
    [SerializeField] Vector3[] top5 = { new Vector3(-0.5f,2.3f,0) , new Vector3(-0.25f, 2.3f, 0) , new Vector3(0, 2.3f, 0) , new Vector3(0.25f, 2.3f, 0) , new Vector3(0.5f, 2.3f, 0) };
    [SerializeField] Vector3[] low5 = { new Vector3(-3, -5.5f, 0), new Vector3(-1.5f, -5.5f, 0), new Vector3(0, -5.5f, 0), new Vector3(1.5f, -5.5f, 0), new Vector3(3, -5.5f, 0) };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(float moveSpeed, Vector3 offset)
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPos = top5[i] + offset;
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move2Init(moveSpeed, low5[i] + offset);
            }
        }
    }
    public IEnumerator Shotgun(float volley, float speed)
    {
        float offset = Random.Range(-1f, 1f);
        for (int i = 0; i < volley; i++)
        {
            Shoot(speed, new Vector3(offset, 0, 0));
            for (int j = 0; j < 10; j++)
            {
                 yield return null;
            }
        }
    }
}
