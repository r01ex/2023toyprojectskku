using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public static Bullet Instance;

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
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        this.gameObject.GetComponent<Animator>().Play("BulletFly");
    }

    public void increaseSize(){
        this.GetComponent<Transform>().localScale = new Vector3(0.05f, 0.05f, 0.0f);
    }
}
