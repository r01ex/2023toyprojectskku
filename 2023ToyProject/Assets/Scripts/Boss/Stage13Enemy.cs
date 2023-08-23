using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage13Enemy : MonoBehaviour
{
    private float hp;
    [SerializeField]
    private float maxHp = 1f;

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
    FollowX FollowXPattern;
    [SerializeField]
    float followXFollowSpeed;
    [SerializeField]
    float followXVolley;
    [SerializeField]
    int followXInterval;
    [SerializeField]
    float followXFallSpeed;
    [SerializeField]
    TrailShots TrailShotsPattern;
    [SerializeField]
    float trailShotsMoveSpeedRangeLow;
    [SerializeField]
    float trailShotsMoveSpeedRangeHigh;
    [SerializeField]
    int trailShotsIntervalRangeLow;
    [SerializeField]
    int trailShotsIntervalRangeHigh;
    [SerializeField]
    float trailShotsVolley;
    [SerializeField]
    int trailShotsTrailInterval;
    [SerializeField]
    int trailShotsTrailDuration;
    [SerializeField]
    HexagonCycle HexagonCycleGimmick;
    [SerializeField]
    float hexagonCycleMoveSpeed;
    [SerializeField]
    int hexagonCycleFrameBeforeDirChange;
    [SerializeField]
    Vector3 hexagonCycleInitialDir;
    [SerializeField]
    Vector3 hexagonCycleSpawnPos;
    [SerializeField]
    int hexagonCycleLoop;

    [SerializeField]
    float patternInterval;

    [SerializeField]
    Image healthbar;

    private Color flashColor = Color.red; // Red color when is hit
    private float flashDuration = 0.02f; // To indicate that the time of change color

    private Color originalColor;
    private Renderer enemyRenderer;
    TMPro.TextMeshProUGUI healthText;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        healthText = GameObject.Find("bosshealth").GetComponent<TMPro.TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        healthText.text = hp + "/" + maxHp;
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
        StartEnemyRoutine();
        anim.SetBool("isLowHp", false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
        StartCoroutine("HexagonRoutine");
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator HexagonRoutine()
    {
        yield return new WaitForSeconds(1.8f);

        while (true)
        {
            StartCoroutine(HexagonCycleGimmick.shootHexagonCycleSingle(hexagonCycleMoveSpeed, hexagonCycleFrameBeforeDirChange, hexagonCycleInitialDir, hexagonCycleSpawnPos, hexagonCycleLoop));

            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.8f);

        while (true)
        {
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 5; i++)
            {
                DiagonalPattern.ShootDiagonal(diagonalMoveSpeed, Random.Range(1f, 2.6f), diagonalBulletNum, Random.Range(0, 2));

                yield return new WaitForSeconds(patternInterval * 0.25f);
            }

            yield return new WaitForSeconds(patternInterval * 1.5f);
            anim.SetTrigger("doAttack");
            StartCoroutine(TrailShotsPattern.RandomVolley(trailShotsMoveSpeedRangeLow, trailShotsMoveSpeedRangeHigh, trailShotsIntervalRangeLow, trailShotsIntervalRangeHigh, trailShotsVolley, trailShotsTrailInterval, trailShotsTrailDuration));

            yield return new WaitForSeconds(patternInterval);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(FollowXPattern.Shoot(followXFollowSpeed, followXVolley, followXInterval, followXFallSpeed));

                yield return new WaitForSeconds(patternInterval * 0.5f + followXVolley * followXInterval / 120f);
            }

            yield return new WaitForSeconds(patternInterval * 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            healthbar.fillAmount = hp / maxHp;
            healthText.text = hp + "/" + maxHp;
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            SoundEffectManager.Instance.PlayEnemyHit();
            anim.SetTrigger("doHitted");
            enemyRenderer.material.color = flashColor;


            if (bullet != null)
            {
                bullet.DestroySelf();
            }
            if (hp < maxHp * 0.3)
            {
                anim.SetBool("isLowHp", true);
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
