using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonCycle : MonoBehaviour
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
    public IEnumerator shootHexagonCycleSingle(float moveSpeed, float frameBeforeDirChange, Vector3 initialDir, Vector3 spawnPos, int loop)
    {
        GameObject enemyObject = BulletObjectPool.Instance.GetPooledEnemyBullet();
        frameBeforeDirChange = frameBeforeDirChange * targetframe / 120;
        if (enemyObject != null)
        {
            enemyObject.transform.position = spawnPos;
            enemyObject.SetActive(true);
            EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
            int angle = 0;
            for (int i = 0; i < loop * 6; i++)
            {
                float dirx = Mathf.Cos((angle * Mathf.PI) / 180f);
                float diry = Mathf.Sin((angle * Mathf.PI) / 180f);
                enemy.move3Init(moveSpeed, new Vector3(dirx,diry));
                for (int j = 0; j < frameBeforeDirChange; j++)
                {
                    if(enemyObject.activeInHierarchy==false)
                    {
                        enemy.moveNumber = -1;
                        yield break;
                    }
                     yield return null;
                }
                angle -= 60;
            }
            enemy.moveNumber = -1;
            enemyObject.SetActive(false);
        }
    }
    public void debug()
    {
        //7.2 * 60 = 3.6 * 120 = 1.8 * 2 * 120 = 한변길이 * fps
        StartCoroutine(shootHexagonCycleSingle(7.2f, 60, Vector3.right, new Vector3(-1.8f, this.transform.position.y), 2));
    }
}
