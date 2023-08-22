using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage5Enemy : MonoBehaviour
{
    // To indicate that the boss hp
    [SerializeField]
    private float hp;
    private float maxHp = 10f;

    
    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Diagonal DiagonalPattern;
    [SerializeField]
    float diagonalMoveSpeed;
    [SerializeField]
    int diagonalBulletNum;
    [SerializeField]
    Snipe SnipePattern;
    [SerializeField]
    int snipeVolley;
    [SerializeField]
    float snipeMoveSpeed;
    [SerializeField]
    int snipeSignalFrame;
    [SerializeField]
    int snipeInterval;
    [SerializeField]
    RandomFall RandomFallPattern;
    [SerializeField]
    float randomfallMoveSpeedRangeLow;
    [SerializeField]
    float randomfallMoveSpeedRangeHigh;
    [SerializeField]
    int randomfallIntervalRangeLow;
    [SerializeField]
    int randomfallIntervalRangeHigh;
    [SerializeField]
    float randomfallVolley;

    [SerializeField]
    ATGC ATGCGimmick;
    [SerializeField]
    float ATGCMoveSpeed;

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
            for (int i = 0; i < 5; i++)
            {
                DiagonalPattern.ShootDiagonal(diagonalMoveSpeed, Random.Range(1f, 2.6f), diagonalBulletNum, Random.Range(0, 2), 5);

                yield return new WaitForSeconds(patternInterval * 0.25f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);

            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));

                yield return new WaitForSeconds(patternInterval * 0f + randomfallIntervalRangeLow * randomfallVolley / 120f);
            }

            yield return new WaitForSeconds(patternInterval);

            StartCoroutine(SnipePattern.RandomvolleyWithSignal(snipeVolley, snipeMoveSpeed, snipeSignalFrame, snipeInterval, 5));

            yield return new WaitForSeconds(patternInterval * 0.5f + (snipeSignalFrame + snipeVolley * snipeInterval * 2) / 120f);

            for (int i = 0; i < 4; i++)
            {
                ATGCGimmick.Shoot(ATGCMoveSpeed, Random.Range(-2.1f, 2.1f), Random.Range(-25f, 25f), Random.Range(0, 2), 5);

                yield return new WaitForSeconds(patternInterval * 0.5f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
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
