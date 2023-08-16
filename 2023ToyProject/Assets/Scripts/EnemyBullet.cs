using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public void move1Init(float speed)
    {
        StartCoroutine(move1(speed));
    }
    IEnumerator move1(float moveSpeed)
    {
        while(true)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Nuclear")
        {
            Debug.Log("hit");
            this.transform.GetChild(0).GetComponent<EnemybulletInner>().isHit = true;
            this.gameObject.SetActive(false);
        }
        else if(collision.tag == "Shield")
        {
            Debug.Log("shield");
            this.gameObject.SetActive(false);
        }
        else if(collision.tag == "BulletDestroyWall")
        {
            this.gameObject.SetActive(false);
        }
    }
    
}