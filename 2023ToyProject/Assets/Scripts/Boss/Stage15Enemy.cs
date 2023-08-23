using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage15Enemy : MonoBehaviour
{
    private float hp;
    [SerializeField]
    private float maxHp = 1f;

    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Spiral SpiralPattern;
    [SerializeField]
    float spiralSpeed;
    [SerializeField]
    int spiralBulletInOneVolley;
    [SerializeField]
    int spiralInterval;
    [SerializeField]
    float spiralAngleIncrement;
    [SerializeField]
    int spiralTotal;
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
    AtomBomb AtomBombGimmick;
    [SerializeField]
    int atomBombDropSpeed;
    [SerializeField]
    float atomBombSplinterScaleSpeed;

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
        StartCoroutine("AtomBombRoutine");
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator AtomBombRoutine()
    {
        yield return new WaitForSeconds(1.8f);

        while (true)
        {
            anim.SetTrigger("doAttack");
            StartCoroutine(AtomBombGimmick.Shoot(atomBombDropSpeed, atomBombSplinterScaleSpeed));

            yield return new WaitForSeconds(patternInterval * 7f);
        }
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1.8f + patternInterval * 3f);

        while (true)
        {
            StartCoroutine(SpiralPattern.ShootBackAndForth(spiralSpeed, spiralBulletInOneVolley, spiralInterval, spiralAngleIncrement, spiralTotal));

            yield return new WaitForSeconds(patternInterval * (-2f) + spiralBulletInOneVolley * spiralInterval * spiralTotal / 120f);

            StartCoroutine(SnipePattern.RandomvolleyWithSignal(snipeVolley, snipeMoveSpeed, snipeSignalFrame, snipeInterval));

            yield return new WaitForSeconds(patternInterval * (-2f) + (snipeSignalFrame + snipeVolley * snipeInterval * 2) / 120f);

            StartCoroutine(TrailShotsPattern.RandomVolley(trailShotsMoveSpeedRangeLow, trailShotsMoveSpeedRangeHigh, trailShotsIntervalRangeLow, trailShotsIntervalRangeHigh, trailShotsVolley, trailShotsTrailInterval, trailShotsTrailDuration));

            yield return new WaitForSeconds(patternInterval * 1.5f);

            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));

                yield return new WaitForSeconds(patternInterval * 0.5f + randomfallIntervalRangeLow * randomfallVolley / 120f);
            }

            yield return new WaitForSeconds(patternInterval * 1.5f);
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
