using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemybulletInner : MonoBehaviour
{
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
        if (collision.tag == "ElectricField")
        {
            Debug.Log("Èí¼ö");
            PlayerManager.Instance.addbullet(1);
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
