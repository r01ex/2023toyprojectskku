using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage7Enemy : MonoBehaviour
{
    // To indicate that the boss hp
    [SerializeField]
    private float hp;
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
    private void Awake() {
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
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 2.5f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 0f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, -2.5f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));

            yield return new WaitForSeconds(patternInterval * (-2f) + helixTotalBullet * helixInterval / 120f);

            for (int i = 0; i < 4; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));

                yield return new WaitForSeconds(patternInterval * 0f + randomfallIntervalRangeLow * randomfallVolley / 120f);
            }
            for (int i = 0; i < 2; i++)
            {
                StartCoroutine(RandomFallPattern.Shoot(randomfallMoveSpeedRangeLow, randomfallMoveSpeedRangeHigh, randomfallIntervalRangeLow, randomfallIntervalRangeHigh, randomfallVolley));
            }

            StartCoroutine(SpiralPattern.ShootBackAndForth(backnforthSpeed, backnforthBulletInOneVolley, backnforthInterval, backnforthAngleIncrement, backnforthTotal));

            yield return new WaitForSeconds(patternInterval * 1f + backnforthBulletInOneVolley * backnforthInterval * backnforthTotal / 120f);
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
