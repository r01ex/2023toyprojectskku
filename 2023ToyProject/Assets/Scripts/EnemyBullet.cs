using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    private float minY = -7;
    public void setMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Nuclear")
        {
            Debug.Log("�ǰ�");
            this.transform.GetChild(0).GetComponent<EnemybulletInner>().isHit = true;
            this.gameObject.SetActive(false);
        }
        else if(collision.tag == "Shield")
        {
            Debug.Log("����");
            this.gameObject.SetActive(false);
        }
    }
    
}