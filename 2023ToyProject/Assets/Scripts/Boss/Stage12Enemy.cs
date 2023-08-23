using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage12Enemy : MonoBehaviour
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
    float spiralMoveSpeed;
    [SerializeField]
    int spiralTotalbullet;
    [SerializeField]
    Pinch PinchPattern;
    [SerializeField]
    float pinchMoveSpeed;
    [SerializeField]
    int pinchVolley;
    [SerializeField]
    int pinchInterval;
    [SerializeField]
    Fireworks FireworksPattern;
    [SerializeField]
    int fireworksDropFrameLow;
    [SerializeField]
    int fireworksDropFrameHigh;
    [SerializeField]
    float fireworksDropSpeed;
    [SerializeField]
    int fireworksSplitNumber;
    [SerializeField]
    float fireworksSplitSpeed;
    [SerializeField]
    float fireworksVolley;
    [SerializeField]
    float fireworksSplitVolley;
    [SerializeField]
    float fireworksSplitVolleyInterval;

    [SerializeField]
    Bounce BounceGimmick;
    [SerializeField]
    int bounceVolley;
    [SerializeField]
    int bounceDropSpeed;
    [SerializeField]
    float bounceBounceBulletSpeed;
    [SerializeField]
    int bounceInterval;

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
            StartCoroutine(PinchPattern.MapShoot(pinchMoveSpeed, pinchVolley, pinchInterval));

            yield return new WaitForSeconds(patternInterval * 0.5f + pinchVolley * pinchInterval / 120f);

            StartCoroutine(FireworksPattern.Shoot(fireworksDropFrameLow, fireworksDropFrameHigh, fireworksDropSpeed, fireworksSplitNumber, fireworksSplitSpeed, fireworksVolley, fireworksSplitVolley, fireworksSplitVolleyInterval));

            yield return new WaitForSeconds(patternInterval * 4f);

            StartCoroutine(BounceGimmick.Shoot(bounceVolley, bounceDropSpeed, bounceBounceBulletSpeed, bounceInterval));

            yield return new WaitForSeconds(patternInterval * (-2.5f) + bounceVolley * bounceInterval / 120f);

            for (int i = 0; i < 5; i++)
            {
                SpiralPattern.allAroundShotgunSingle(spiralMoveSpeed, spiralTotalbullet + i * 10);

                yield return new WaitForSeconds(patternInterval);
            }

            yield return new WaitForSeconds(patternInterval * 2f);
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
