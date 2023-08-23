using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage8Enemy : MonoBehaviour
{
    // To indicate that the boss hp

    private float hp;
    [SerializeField]
    private float maxHp = 1f;

    
    [SerializeField]
    private float damage = 1f;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Diagonal DiagonalPattern;
    [SerializeField]
    float diagonalMoveSpeed;
    [SerializeField]
    int diagonalBulletNum;
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
    ShotGun5 ShotGun5Pattern;
    [SerializeField]
    float shotgunSpeed;
    [SerializeField]
    int shotgunVolleys;

    [SerializeField]
    float patternInterval;

    [SerializeField]
    Image healthbar;
    [SerializeField]
    Transform particleSystem;
    [SerializeField]
    float smokeFallSpeed;
    bool didresetSmoke = false;

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
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
        anim.SetBool("isLowHp", false);
        StartEnemyRoutine(); 
    }

    // Update is called once per frame
    void Update()
    {
        particleSystem.position = new Vector3(particleSystem.position.x, Mathf.Clamp(particleSystem.position.y - (smokeFallSpeed * Time.deltaTime), 1.5f, 15f));
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
                StartCoroutine(ShotGun5Pattern.Shotgun(shotgunVolleys, shotgunSpeed));

                yield return new WaitForSeconds(patternInterval * 0.5f + shotgunVolleys / 12f);
            }
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 5; i++)
            {
                DiagonalPattern.ShootDiagonal(diagonalMoveSpeed, Random.Range(1f, 2.6f), diagonalBulletNum, Random.Range(0, 2));

                yield return new WaitForSeconds(patternInterval * 0.25f);
            }
            anim.SetTrigger("doAttack");
            for (int i = 0; i < 6; i++)
            {
                StartCoroutine(SpiralPattern.Shoot(spiralMoveSpeed, spiralTotalBullet, spiralInterval, spiralAngleOffset, spiralAngleIncrement));

                yield return new WaitForSeconds(patternInterval * 0f + spiralTotalBullet * spiralInterval / 120f / 6f);
            }

            yield return new WaitForSeconds(patternInterval * 2.5f);
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
            if (hp < maxHp * 0.5)
            {
                anim.SetBool("isLowHp", true);
                if (didresetSmoke == false)
                {
                    particleSystem.position = new Vector3(particleSystem.position.x, 15);
                    didresetSmoke = true;
                }
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
