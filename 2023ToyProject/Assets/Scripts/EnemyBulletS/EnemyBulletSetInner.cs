using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSetInner : MonoBehaviour
{
    public bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isHit == false)
        {
            if (collision.tag == "ElectricField")
            {
                Debug.Log("absorb");
                PlayerManager.Instance.addbullet(1);
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
