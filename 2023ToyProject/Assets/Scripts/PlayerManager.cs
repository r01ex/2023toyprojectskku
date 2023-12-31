using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] int maxBullet;
    public int currentBullet = 0;
    [SerializeField] Image bulletcircle;
    [SerializeField] GameObject Electricfield;

    public float attack = 1;
    Vector3 addedBulletSize = new Vector3(0, 0, 0);

    [SerializeField] float shieldtime;
    Coroutine bulletShootCoroutine;

    [SerializeField] GameObject Nuclear;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addbullet(int addAmount)
    {
        currentBullet = Mathf.Clamp(currentBullet + addAmount, 0, maxBullet);
        bulletcircle.fillAmount = (float)currentBullet / (float)maxBullet;
        if(currentBullet == maxBullet)
        {
            onbulletfull();
        }
    }
    public void setBulletZero()
    {
        currentBullet = 0;
        bulletcircle.fillAmount = 0;
        if (bulletShootCoroutine != null)
        {
            StopCoroutine(bulletShootCoroutine);
            bulletShootCoroutine = null;
        }
    }
    public void onbulletfull()
    {
        //TODO
    }
    public void Shoot()
    {
        if (bulletShootCoroutine == null)
        {
            bulletShootCoroutine = StartCoroutine(shootNbulletHorizontal(currentBullet));
            addbullet(-currentBullet);
        }
    }
    IEnumerator shootNbulletHorizontal(int n)
    {
        while(n>0)
        {
            GameObject bullet = BulletObjectPool.Instance.GetPooledPlayerBullet();
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = this.transform.rotation;
                bullet.transform.localScale = new Vector3(0.1f, 0.1f, 1) + addedBulletSize;
                bullet.SetActive(true);
            }
            bullet.transform.parent = null;
            SoundEffectManager.Instance.PlayShoot();
            yield return new WaitForSeconds(0.05f);
            n--;
        }
        bulletShootCoroutine = null;
    }
    public void Shield()
    {
        if (Electricfield.tag != "Shield")
        {
            if (currentBullet > 0)
            {
                //TODO art
                Electricfield.GetComponent<SpriteRenderer>().color = new Color(1, 0.75f, 0, 1);
                //TODO rule
                addbullet(-1);
                Debug.Log("shield");
                Electricfield.tag = "Shield";
                StartCoroutine(ShieldOff());
            }
            else
            {
                Electricfield.tag = "ElectricField";
                Electricfield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                Debug.Log("no bullet to shield");
            }
        }
        else
        {
            Debug.Log("Shield already on");
        }
    }
    IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(shieldtime);
        if (Input.GetKey(KeyCode.X))
        {
            Electricfield.GetComponent<SpriteRenderer>().color = new Color(1, 0.75f, 0, 1);
            Electricfield.tag = "ElectricField";
        }
        else
        {
            Electricfield.tag = "ElectricField";
            Electricfield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        }
    }
    public void instantShieldoff()
    {
        Electricfield.tag = "ElectricField";
        Electricfield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
    #region player skill
    public void addMaxBullet(int n)
    {
        maxBullet += n;
    }
    public void increaseAttack(float increaseAmount)
    {
        attack += increaseAmount;
    }

    public void increaseSpeed(float increaseAmount)
    {
        PlayerControl.Instance.addSpeed(increaseAmount);
    }

    public void increaseShieldTime(float increaseAmount)
    {
        shieldtime += increaseAmount;
    }

    public void increaseGrabRange(float increaseAmount)
    {
        Electricfield.GetComponent<Transform>().localScale += new Vector3(increaseAmount, increaseAmount, 0);
    }

    public void increaseBulletSize(float increaseAmount)
    {
        addedBulletSize += new Vector3(increaseAmount, increaseAmount, 0);
    }
    public void decreaseNuclearSize(float mult)
    {
        Nuclear.transform.localScale = new Vector3(Nuclear.transform.localScale.x * mult, Nuclear.transform.localScale.y * mult, Nuclear.transform.localScale.z);
    }
    #endregion
}