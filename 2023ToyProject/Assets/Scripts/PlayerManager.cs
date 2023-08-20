using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

     public  GameObject bullet;

    [SerializeField] int maxBullet;
    public int currentBullet = 0;
    [SerializeField] Image bulletcircle;
    [SerializeField] GameObject Electricfield;

    [SerializeField] float attack = 1;

    [SerializeField] float speed = 600;

    [SerializeField] float shieldtime;
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
    public void onbulletfull()
    {
        //TODO
    }
    public void Shoot()
    {
        StartCoroutine(shootNbulletHorizontal(currentBullet));
        addbullet(-currentBullet);
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
                bullet.SetActive(true);
            }
            bullet.transform.parent = null;
            yield return new WaitForSeconds(0.05f);
            n--;
        }
    }

    public void increaseAttack(float increaseAmount){
        attack += increaseAmount;
    }

    public void increaseSpeed(float increaseAmount){
        speed += increaseAmount;
    }

    public void increaseShieldTime(float increaseAmount){
        shieldtime += increaseAmount;
    }

    public void increaseGrabRange(){
        Electricfield.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.0f);
    }

    public void increaseBulletSize(){
       
        bullet.GetComponent<Bullet>().increaseSize();
    }
    
     public void superIncreaseAttack(float increaseAmount){
        attack += increaseAmount;
    }


     public void superIncreaseSpeed(float increaseAmount){
        attack += increaseAmount;
    }

     public void superIncreaseSheildTime(float increaseAmount){
        attack += increaseAmount;
    }

    public void superIncreaseGrabRange(){
        Electricfield.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f, 0.0f);
    }

    public void rollBackAttack(float increaseAmount){
        attack -= increaseAmount;
    }
    public void rollBackSpeed(float increaseAmount){
        attack -= increaseAmount;
    }
    public void rollBackSheildTime(float increaseAmount){
        attack -= increaseAmount;
    }
    public void rollBackGrabRange(){
        Electricfield.GetComponent<Transform>().localScale = new Vector3(-0.3f, -0.3f, 0.0f);
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
                Electricfield.GetComponent<SpriteRenderer>().color = new Color(0, 0.07f, 1, 0.45f);
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
        if (Input.GetKey(KeyCode.X) || Input.GetMouseButton(1))
        {
            Electricfield.tag = "ElectricField";
        }
        else
        {
            Electricfield.tag = "ElectricField";
            Electricfield.GetComponent<SpriteRenderer>().color = new Color(0, 0.07f, 1, 0.45f);
        }
    }

    #region player skill
    public void addMaxBullet(int n)
    {
        maxBullet += n;
    }

    #endregion
}