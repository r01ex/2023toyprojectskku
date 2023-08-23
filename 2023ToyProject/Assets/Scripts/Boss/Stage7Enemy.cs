using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage7Enemy : MonoBehaviour
{
    // To indicate that the boss hp

    private float hp;
    [SerializeField]
    private float maxHp = 1f;

    
    [SerializeField]
    private float damage = 1f;  //To indicate that the damage of each electrons

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
    Spiral SpiralPattern;
    [SerializeField]
    float backnforthSpeed;
    [SerializeField]
    int backnforthBulletInOneVolley;
    [SerializeField]
    int backnforthInterval;
    [SerializeField]
    float backnforthAngleIncrement;
    [SerializeField]
    int backnforthTotal;
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
        healthText = GameObject.Find("bosshealth").GetComponent<TMPro.TextMeshProUGUI>();

        anim = GetComponent<Animator>();
        Camera.main.transform.Rotate(new Vector3(0, 0, 180));
        PlayerControl.Instance.flipMovement();
    }
    private void OnDisable()
    {
        PlayerControl.Instance.flipMovement();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = hp + "/" + maxHp;
        hp = maxHp;
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
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 2.5f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 0f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, -2.5f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));

            yield return new WaitForSeconds(patternInterval * (-2f) + helixTotalBullet * helixInterval / 120f);
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));

                yield return new WaitForSeconds(patternInterval * 0.5f + randomfallIntervalRangeLow * randomfallVolley / 120f);
            }

            yield return new WaitForSeconds(patternInterval * 1f);
            anim.SetTrigger("doAttack");
            StartCoroutine(SpiralPattern.ShootBackAndForth(backnforthSpeed, backnforthBulletInOneVolley, backnforthInterval, backnforthAngleIncrement, backnforthTotal));

            yield return new WaitForSeconds(patternInterval * 1f + backnforthBulletInOneVolley * backnforthInterval * backnforthTotal / 120f);
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
