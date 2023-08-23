using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stage1Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp = 10f;
    private float maxHp = 10f;

    [SerializeField]
    private float damage = 1f;  //To indicate that the damage of each electrons

    private Animator anim;

    [SerializeField]
    Shoot5Row Shoot5RowPattern;
    [SerializeField]
    Wall WallPattern;

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
        yield return new WaitForSeconds(3f);

        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                anim.SetTrigger("doAttack");
                Shoot5RowPattern.Shoot(5, Random.Range(-1.3f, 1.3f), 1);

                yield return new WaitForSeconds(patternInterval);
            }
            anim.SetTrigger("doAttack");
            StartCoroutine(WallPattern.ShootLines(3.5f, 7.6f, 28, 1, 10, 1));

            yield return new WaitForSeconds(patternInterval);
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
