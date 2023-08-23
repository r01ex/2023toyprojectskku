using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage5Enemy : MonoBehaviour
{
    // To indicate that the boss hp

    private float hp;
    [SerializeField]
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
            for (int i = 0; i < 5; i++)
            {

                DiagonalPattern.ShootDiagonal(diagonalMoveSpeed, Random.Range(1f, 2.6f), diagonalBulletNum, Random.Range(0, 2));

                yield return new WaitForSeconds(patternInterval * 0.25f);
            }

            yield return new WaitForSeconds(patternInterval * 0.5f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));

                yield return new WaitForSeconds(patternInterval * 0f + randomfallIntervalRangeLow * randomfallVolley / 120f);
            }

            yield return new WaitForSeconds(patternInterval);
            anim.SetTrigger("doAttack");
            StartCoroutine(SnipePattern.RandomvolleyWithSignal(snipeVolley, snipeMoveSpeed, snipeSignalFrame, snipeInterval));

            yield return new WaitForSeconds(patternInterval * 0.5f + (snipeSignalFrame + snipeVolley * snipeInterval * 2) / 120f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 4; i++)
            {
                ATGCGimmick.Shoot(ATGCMoveSpeed, Random.Range(-2.1f, 2.1f), Random.Range(-25f, 25f), Random.Range(0, 2));

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
