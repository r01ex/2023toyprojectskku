using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
}
