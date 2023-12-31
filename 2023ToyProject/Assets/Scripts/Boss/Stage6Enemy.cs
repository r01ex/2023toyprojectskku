using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage6Enemy : MonoBehaviour
{
    // To indicate that the boss hp

    private float hp;
    [SerializeField]
    private float maxHp;

    
    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    TrailShots TrailShotsPattern;
    [SerializeField]
    float snipeShootSingleMoveSpeed;
    [SerializeField]
    int snipeShootSingleTrailInterval;
    [SerializeField]
    int snipeShootSingleTrailDuration;
    [SerializeField]
    float randomVolleyMoveSpeedRangeLow;
    [SerializeField]
    float randomVolleyMoveSpeedRangeHigh;
    [SerializeField]
    int randomVolleyIntervalRangeLow;
    [SerializeField]
    int randomVolleyIntervalRangeHigh;
    [SerializeField]
    float randomVolleyVolley;
    [SerializeField]
    int randomVolleyTrailInterval;
    [SerializeField]
    int randomVolleyTrailDuration;
    [SerializeField]
    float shootSingleFollowMoveSpeed;
    [SerializeField]
    int shootSingleFollowTrailInterval;
    [SerializeField]
    int shootSingleFollowTrailDuration;
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
            for (int i = 0; i < 3; i++)
            {
                TrailShotsPattern.SnipeShootSingle(snipeShootSingleMoveSpeed, snipeShootSingleTrailInterval, snipeShootSingleTrailDuration);

                yield return new WaitForSeconds(patternInterval * 0.5f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
            anim.SetTrigger("doAttack");
            StartCoroutine(TrailShotsPattern.RandomVolley(randomVolleyMoveSpeedRangeLow, randomVolleyMoveSpeedRangeHigh, randomVolleyIntervalRangeLow, randomVolleyIntervalRangeHigh, randomVolleyVolley, randomVolleyTrailInterval, randomVolleyTrailDuration));

            yield return new WaitForSeconds(patternInterval + randomVolleyIntervalRangeHigh * randomVolleyVolley / 120f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(FollowXPattern.Shoot(followXFollowSpeed, followXVolley, followXInterval, followXFallSpeed));

                yield return new WaitForSeconds(patternInterval * 0.5f + followXVolley * followXInterval / 120f);
            }
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 2; i++)
            {
                TrailShotsPattern.ShootSingleFollow(shootSingleFollowMoveSpeed, shootSingleFollowTrailInterval, shootSingleFollowTrailDuration);

                yield return new WaitForSeconds(patternInterval);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp = Mathf.Clamp(hp - damage, 0, int.MaxValue);
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
