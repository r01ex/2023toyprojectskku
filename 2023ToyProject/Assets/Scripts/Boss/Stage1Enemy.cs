using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp = 5f;

    [SerializeField]
    private float damage = 1f;

    private Animator anim;

    [SerializeField]
    Shoot5Row Shoot5RowPattern;

    [SerializeField]
    float patternInterval;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(3f);

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
            anim.SetTrigger("doHitted");

            if (bullet != null)
            {
                bullet.DestroySelf();
            }

            if (hp <= 0)
            {
                //death
                Destroy(gameObject);
            }
        }
    }
}
