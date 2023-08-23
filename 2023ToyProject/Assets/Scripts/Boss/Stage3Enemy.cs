using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage3Enemy : MonoBehaviour
{
    // To indicate that the boss hp
    [SerializeField]
    private float hp;
    private float maxHp;

    
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
    Pinch PinchPattern;
    [SerializeField]
    float pinchMoveSpeed;
    [SerializeField]
    int pinchVolley;
    [SerializeField]
    int pinchInterval;
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
    NH3Gimmick NH3Gimmick;
    [SerializeField]
    float NH3Volley;
    [SerializeField]
    float NH3Inverval;

    [SerializeField]
    float patternInterval;

    [SerializeField]
    Image healthbar;

    private Color flashColor = Color.red; // Red color when is hit
    private float flashDuration = 0.02f; // To indicate that the time of change color

    private Color originalColor;
    private Renderer enemyRenderer;

    //Gimmick Sound
    [SerializeField]
    private GimmickSoundManager gimmickSoundManager;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
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
            StartCoroutine(PinchPattern.MapShoot(pinchMoveSpeed, pinchVolley, pinchInterval, 3));

            yield return new WaitForSeconds(patternInterval * (-1f) + pinchVolley * pinchInterval / 120f);
            anim.SetTrigger("doAttack");
            StartCoroutine(SpiralPattern.Shoot(spiralMoveSpeed, spiralTotalBullet, spiralInterval, spiralAngleOffset, spiralAngleIncrement, 3));

            yield return new WaitForSeconds(patternInterval * (-1.5f) + spiralTotalBullet * spiralInterval / 120f);
            anim.SetTrigger("doAttack");
            StartCoroutine(HelixPattern.ShootSingle(helixMoveSpeed, helixTotalBullet, helixInterval, 0f, helixWidthNumber, helixWidthSeparation, helixDiminisherMult, true, 3));

            yield return new WaitForSeconds(patternInterval * (-1.5f) + helixTotalBullet * helixInterval / 120f);
            anim.SetTrigger("doAttack");
            //gimmickSoundManager.PlayGimmickSound(0);
            StartCoroutine(NH3Gimmick.Shoot(NH3Volley, NH3Inverval, 3));

            yield return new WaitForSeconds(patternInterval * (-1f) + NH3Volley * NH3Inverval / 120f);
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
