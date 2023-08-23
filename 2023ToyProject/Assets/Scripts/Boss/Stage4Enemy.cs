using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage4Enemy : MonoBehaviour
{
    // To indicate that the boss hp
    private float hp;
    [SerializeField]
    private float maxHp = 1f;

    
    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    private bool isLow = false;

    [SerializeField]
    ATGC BubblePattern;
    [SerializeField]
    float ATGCMoveSpeed;
    [SerializeField]
    Wall WallPattern;
    [SerializeField]
    float wallMoveSpeed;
    [SerializeField]
    float wallWidth;
    [SerializeField]
    int wallBulletNum;
    [SerializeField]
    int wallLineNum;
    [SerializeField]
    int wallInterval;

    [SerializeField]
    N2Gimmick N2Gimmick;

    [SerializeField]
    float patternInterval;


    private Color flashColor = Color.red; // Red color when is hit
    private float flashDuration = 0.02f; // To indicate that the time of change color

    private Color originalColor;
    private Renderer enemyRenderer;

    [SerializeField]
    Image healthbar;
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
        StartCoroutine(EnemyRoutine());
        StartCoroutine(Gimmick());
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.8f);

        while (true)
        {
            if (!isLow)
            {
                for (int i = 0; i < 3; i++)
                {
                    BubblePattern.Shoot(ATGCMoveSpeed, Random.Range(-2.5f, 2.5f), 0, Random.Range(0, 2));

                    yield return new WaitForSeconds(patternInterval * 0.25f);
                }

                yield return new WaitForSeconds(patternInterval * 0.75f);

                StartCoroutine(WallPattern.ShootLines(wallMoveSpeed, wallWidth, wallBulletNum, wallLineNum, wallInterval));

                yield return new WaitForSeconds(patternInterval + wallLineNum * wallInterval / 120f);
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    BubblePattern.Shoot(ATGCMoveSpeed * 1.5f, Random.Range(-2.5f, 2.5f), 0, Random.Range(0, 2));

                    yield return new WaitForSeconds(patternInterval * 0.25f);
                }

                yield return new WaitForSeconds(patternInterval * 0.75f);

                StartCoroutine(WallPattern.ShootLines(wallMoveSpeed * 1.5f, wallWidth, wallBulletNum, wallLineNum, (int)(wallInterval / 1.5f)));

                yield return new WaitForSeconds(patternInterval + wallLineNum * (int)(wallInterval / 1.5f) / 120f);
            }
        }
    }
    IEnumerator Gimmick()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            anim.SetTrigger("doInhale");
            N2Gimmick.Inhale(true);
            yield return new WaitForSeconds(5f);
            anim.SetTrigger("endInhale");
            N2Gimmick.Inhale(false);
            yield return new WaitForSeconds(2f);
            anim.SetTrigger("doEmission");
            N2Gimmick.Emission(true);
            yield return new WaitForSeconds(5f);
            anim.SetTrigger("endEmission");
            N2Gimmick.Emission(false);
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            if (hp < maxHp * 0.3)
            {
                anim.SetBool("isLowHp", true);
            }
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
