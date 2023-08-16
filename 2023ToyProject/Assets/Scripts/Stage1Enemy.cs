using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp = 5f;

    [SerializeField]
    private float damage = 1f;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌");
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp -= damage;
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            anim.SetTrigger("doHitted");

            if (bullet != null)
            {
                bullet.DestroySelf();
            }

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
