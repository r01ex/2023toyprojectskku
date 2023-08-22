using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage9Enemy : MonoBehaviour
{
    // To indicate that the boss hp
    [SerializeField]
    private float hp;
    private float maxHp = 1f;

    
    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Spiral SpiralPattern;
    [SerializeField]
    float spiralMoveSpeed;
    [SerializeField]
    int spiralTotalbullet;
    [SerializeField]
    Pinch PinchPattern;
    [SerializeField]
    float pinchMoveSpeed;
    [SerializeField]
    int pinchVolley;
    [SerializeField]
    int pinchInterval;
    [SerializeField]
    Cluster ClusterPattern;
    [SerializeField]
    int clusterBullets;
    [SerializeField]
    float clusterMoveSpeed;
    [SerializeField]
    float clusterMaxTargetPosOffset;

    [SerializeField]
    float patternInterval;

    [SerializeField]
    Image healthbar;

    private Color flashColor = Color.red; // Red color when is hit
    private float flashDuration = 0.02f; // To indicate that the time of change color

    private Color originalColor;
    private Renderer enemyRenderer;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
        StartEnemyRoutine(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < maxHp * 0.3){
            anim.SetBool("isLowHp", true);
        } else{
            anim.SetBool("isLowHp", false);
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

        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                ClusterPattern.Shoot(clusterBullets, clusterMoveSpeed, clusterMaxTargetPosOffset);

                yield return new WaitForSeconds(patternInterval * 0.5f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
                
            for (int i = 0; i < 5; i++)
            {
                SpiralPattern.allAroundShotgunSingle(spiralMoveSpeed, spiralTotalbullet + i * 8);

                yield return new WaitForSeconds(patternInterval);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);

            StartCoroutine(PinchPattern.MapShoot(pinchMoveSpeed, pinchVolley, pinchInterval));

            yield return new WaitForSeconds(patternInterval * 1.5f + pinchVolley * pinchInterval / 120f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            healthbar.fillAmount = hp / maxHp;
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            anim.SetTrigger("doHitted");
            enemyRenderer.material.color = flashColor;
        

            if (bullet != null)
            {
                bullet.DestroySelf();
            }

            if (hp <= 0)
            {
                //death
                Destroy(gameObject);
                GameplayManager.Instance.SetGameOver();
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
