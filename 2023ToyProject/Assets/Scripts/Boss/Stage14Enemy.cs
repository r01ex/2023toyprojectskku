using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage14Enemy : MonoBehaviour
{
    private float hp;
    [SerializeField]
    private float maxHp = 1f;

    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Helix HelixPattern;
    [SerializeField]
    float helixMoveSpeed;
    [SerializeField]
    int helixTotalBullet;
    [SerializeField]
    int helixInterval;
    [SerializeField]
    int helixWidthNumber;
    [SerializeField]
    float helixWidthSeparation;
    [SerializeField]
    float helixDiminisherMult;
    [SerializeField]
    FollowShoot FollowShootPattern;
    [SerializeField]
    float followShootMoveSpeed;
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
    SpawnTurret SpawnTurretGimmick;

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
        StartCoroutine("TurretRoutine");
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator TurretRoutine()
    {
        yield return new WaitForSeconds(1.8f);

        while (true)
        {

            SpawnTurretGimmick.Spawn();

            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.8f + patternInterval);

        while (true)
        {

            StartCoroutine(HelixPattern.ShootDouble(helixMoveSpeed, helixTotalBullet, helixInterval, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));

            yield return new WaitForSeconds(patternInterval * (-1f) + helixTotalBullet * helixInterval / 120f);

            StartCoroutine(SpiralPattern.Shoot4(spiralMoveSpeed, spiralTotalBullet, spiralInterval, spiralAngleOffset, spiralAngleIncrement));

            yield return new WaitForSeconds(patternInterval * 3.5f + spiralTotalBullet * spiralInterval / 120f / 3f);

            for (int i = 0; i < 3; i++)
            {
                FollowShootPattern.Shoot(followShootMoveSpeed);

                yield return new WaitForSeconds(patternInterval * 0.5f);
            }

            yield return new WaitForSeconds(patternInterval * 2f);
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
