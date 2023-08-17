using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Enemy : MonoBehaviour
{
    // To indicate that the boss hp
    [SerializeField]
    private float hp = 40f;
    private float maxHp = 40f;

    
    [SerializeField]
    private float damage = 1f;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Shoot5Row Shoot5RowPattern;

    [SerializeField]
    float patternInterval;


    private Color flashColor = Color.red; // Red color when is hit
    private float flashDuration = 0.02f; // To indicate that the time of change color

    private Color originalColor;
    private Renderer enemyRenderer;

    //Random Move
    private float minX = -2f;         // X Min
    private float maxX = 2f;         // X Max
    private float moveInterval = 2f; 

    [SerializeField]
    private float nextMoveTime;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        nextMoveTime = Time.time + moveInterval;
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
        StartEnemyRoutine(); 
    }

    // Update is called once per frame
    void Update()
    {
        //if(hp < maxHp * 0.3){
        //    anim.SetBool("isLowHp", true);
        //} else{
        //    anim.SetBool("isLowHp", false);
        //}
        if (Time.time >= nextMoveTime)
        {
            // Random Y position
            float newX = Random.Range(minX, maxX);

            Vector3 newPosition = transform.position;
            newPosition.x = newX;
            transform.position = newPosition;

            
            nextMoveTime = Time.time + moveInterval;
        }
    }
    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.8f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        while (true)
        {
            Shoot5RowPattern.Shoot(moveSpeed);
            spawnCount++;
            if (spawnCount % 5 == 0)
            {
                moveSpeed += 2;
            }
            if (moveSpeed > 10)
            {
                moveSpeed = 3f;
            }

            yield return new WaitForSeconds(patternInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            //anim.SetTrigger("doHitted");
            enemyRenderer.material.color = flashColor;
        

            if (bullet != null)
            {
                bullet.DestroySelf();
            }

            if (hp <= 0)
            {
                //death
                Destroy(gameObject);
                GameplayManager.instance.SetGameOver();
            }
            // To change origin color
            Invoke("ResetColor", flashDuration);
        }
    }
    private void ResetColor()
    {
        enemyRenderer.material.color = originalColor;
    }
}
