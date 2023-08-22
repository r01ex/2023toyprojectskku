using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage10Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp;
    private float maxHp = 10f;

    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    private bool isLow = false;

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
    ATGC BubblePattern;
    [SerializeField]
    float ATGCMoveSpeed;
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
        if(hp < maxHp * 0.5){
            anim.SetBool("isLowHp", true);
            isLow = true;
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
            if (!isLow)
            {
                for (int i = 0; i < 5; i++)
                {
                    BubblePattern.Shoot(ATGCMoveSpeed, Random.Range(-2.5f, 2.5f), 0, Random.Range(0, 2));

                    yield return new WaitForSeconds(patternInterval * 0.25f);
                }

                yield return new WaitForSeconds(patternInterval * 0.5f);

                StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 2.5f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));                
                StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 0f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));
                StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, -2.5f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true));

                yield return new WaitForSeconds(patternInterval * (-2f) + helixTotalBullet * helixInterval / 120f);

                for (int i = 0; i < 3; i++)
                {
                    StartCoroutine(SpiralPattern.Shoot(spiralMoveSpeed, spiralTotalBullet, spiralInterval, spiralAngleOffset, spiralAngleIncrement));

                    yield return new WaitForSeconds(patternInterval * 0f + spiralTotalBullet * spiralInterval / 120f / 3f);
                }

                yield return new WaitForSeconds(patternInterval * 2f);
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    DiagonalPattern.ShootDiagonal(diagonalMoveSpeed, Random.Range(1f, 2.6f), diagonalBulletNum, Random.Range(0, 2));

                    yield return new WaitForSeconds(patternInterval * 0.25f);
                }

                yield return new WaitForSeconds(patternInterval * 0.5f);

                StartCoroutine(WallPattern.ShootLines(wallMoveSpeed, wallWidth, wallBulletNum, wallLineNum, wallInterval));

                yield return new WaitForSeconds(patternInterval * 1.5f + wallLineNum * wallInterval / 120f);

                StartCoroutine(SnipePattern.RandomvolleyWithSignal(snipeVolley, snipeMoveSpeed, snipeSignalFrame, snipeInterval));

                yield return new WaitForSeconds(patternInterval * 2f + (snipeSignalFrame + snipeVolley * snipeInterval * 2) / 120f);
            }
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
