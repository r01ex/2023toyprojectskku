using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemybulletInner : MonoBehaviour
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
                GameplayManager.Instance.totalAbsBullet++;
                PlayerManager.Instance.addbullet(1);
                SoundEffectManager.Instance.PlayAbsorb();
                this.transform.parent.gameObject.GetComponent<EnemyBullet>().moveNumber = -1;
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
