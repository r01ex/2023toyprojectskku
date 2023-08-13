using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY += 0.01f * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY += -0.01f * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX += -0.01f * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX += 0.01f * speed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        transform.position = new Vector3(Mathf.Clamp(moveX + transform.position.x, -width, width), Mathf.Clamp(moveY + transform.position.y, -height, height), 0);
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = this.transform.rotation;
            bullet.SetActive(true);
        }
        bullet.transform.parent = null;
    }
}
