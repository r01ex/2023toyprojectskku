using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot5Row : MonoBehaviour
{
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Shoot(float moveSpeed,float offset)
    {
        foreach (float posX in arrPosX)
        {
            Vector3 spawnPos = new Vector3(posX+offset, transform.position.y, transform.position.z);
            GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
            if (enemyObject != null)
            {
                enemyObject.transform.position = spawnPos;
                enemyObject.SetActive(true);
                EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
                enemy.move1Init(moveSpeed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //temporary
    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }
}
