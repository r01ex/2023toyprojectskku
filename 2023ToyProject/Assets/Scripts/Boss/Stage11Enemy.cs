using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage11Enemy : MonoBehaviour
{
    private float hp;
    [SerializeField]
    private float maxHp;

    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Cluster ClusterPattern;
    [SerializeField]
    int clusterBullets;
    [SerializeField]
    float clusterMoveSpeed;
    [SerializeField]
    float clusterMaxTargetPosOffset;
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
    TrailShots TrailShotsPattern;
    [SerializeField]
    float trailShotsMoveSpeed;
    [SerializeField]
    int trailShotsTrailInterval;
    [SerializeField]
    int trailShotsTrailDuration;
    [SerializeField]
    Spiral SpiralPattern;
    [SerializeField]
    float spiralMoveSpeed;
    [SerializeField]
    int spiralTotalBullet;
    [SerializeField]
    int spiralInterval;
    [SerializeField]
    float spiralAngleOffset;
    [SerializeField]
    float spiralAngleIncrement;

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
        damage = PlayerManager.Instance.attack;
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
            anim.SetTrigger("doAttack");
            StartCoroutine(SpiralPattern.Shoot4(spiralMoveSpeed, spiralTotalBullet, spiralInterval, spiralAngleOffset, spiralAngleIncrement));

            yield return new WaitForSeconds(patternInterval * 0f + spiralTotalBullet * spiralInterval / 120f / 3f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 3; i++)
            {
                ClusterPattern.Shoot(clusterBullets, clusterMoveSpeed, clusterMaxTargetPosOffset);

                yield return new WaitForSeconds(patternInterval * 0.5f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 3; i++)
            {
                TrailShotsPattern.SnipeShootSingle(trailShotsMoveSpeed, trailShotsTrailInterval, trailShotsTrailDuration);

                yield return new WaitForSeconds(patternInterval * 0.5f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));

                yield return new WaitForSeconds(patternInterval * 0.5f + randomfallIntervalRangeLow * randomfallVolley / 120f);
            }

            yield return new WaitForSeconds(patternInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp = Mathf.Clamp(hp - damage, 0, int.MaxValue);
            healthText.text = hp + "/" + maxHp;
            healthbar.fillAmount = hp / maxHp;
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
