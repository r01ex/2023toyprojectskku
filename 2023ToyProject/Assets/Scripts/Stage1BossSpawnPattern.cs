using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossSpawnPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject enemies;
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};
    [SerializeField]
    private float patternInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }
    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
    }
    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        while(true){
            foreach(float posX in arrPosX){
                SpawnEnemy(posX, moveSpeed);
            }

            spawnCount++;
            if(spawnCount % 5 == 0){
                moveSpeed += 2;
            }
            if(moveSpeed > 10){
                moveSpeed = 3f;
            }

            yield return new WaitForSeconds(patternInterval);
        }
    }

    void SpawnEnemy(float posX, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        GameObject enemyObject = Instantiate(enemies, spawnPos, Quaternion.identity); // last param is rotate
        EnemyBullet enemy = enemyObject.GetComponent<EnemyBullet>();
        enemy.setMoveSpeed(moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
