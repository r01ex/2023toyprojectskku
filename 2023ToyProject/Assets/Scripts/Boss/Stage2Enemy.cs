using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage2Enemy : MonoBehaviour
{
    private float hp;
    [SerializeField]
    private float maxHp;

    [SerializeField]
    private float damage;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    ShotGun5 ShotGun5Pattern;
    [SerializeField]
    float shotgunSpeed;
    [SerializeField]
    int shotgunVolleys;
    [SerializeField]
    Spiral SpiralPattern;
    [SerializeField]
    float allAroundShotgunSpeed;
    [SerializeField]
    int allAroundShotgunTotalbullet;
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
    float patternInterval;

    [SerializeField]
    Image healthbar;

    private Color flashColor = Color.red; // Red color when is hit
    private float flashDuration = 0.02f; // To indicate that the time of change color

    private Color originalColor;
    private Renderer enemyRenderer;

    [SerializeField]
    private float nextMoveTime;
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
            StartCoroutine(ShotGun5Pattern.Shotgun(shotgunVolleys, shotgunSpeed));

            yield return new WaitForSeconds(patternInterval * 0.5f + shotgunVolleys / 12f);

            anim.SetTrigger("doAttack");
            StartCoroutine(SpiralPattern.ShootBackAndForth(backnforthSpeed, backnforthBulletInOneVolley, backnforthInterval, backnforthAngleIncrement, backnforthTotal));

            yield return new WaitForSeconds(patternInterval * 0.5f + backnforthBulletInOneVolley * backnforthInterval * backnforthTotal / 120f);


            for (int i = 0; i < 3; i++)
            {
                anim.SetTrigger("doAttack");
                SpiralPattern.allAroundShotgunSingle(allAroundShotgunSpeed, allAroundShotgunTotalbullet + i * 5);

                yield return new WaitForSeconds(patternInterval);
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
