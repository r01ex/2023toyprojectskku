using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    bool didkill = false;
    bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveAndEnabled)
        {
            if (sprite.color.a < 0.8f)
            {
                sprite.color = new Color(0, 0, 0, sprite.color.a + Time.deltaTime * 10);
            }
            if(sprite.color.a>=0.8f && didkill==false)
            {
                if (inRange == true)
                {
                    Debug.Log("hit");
                }
                didkill = true;
                this.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Nuclear")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Nuclear")
        {
            inRange = false;
        }
    }
}
