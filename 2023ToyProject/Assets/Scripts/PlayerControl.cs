using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float width;
    [SerializeField] float height;

    [SerializeField] bool iskeyboardControl = false;
    Camera maincamera;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
        if (!Application.isEditor)
        {
            Invoke("setCursor", 0.1f);
        }
    }
    void setCursor()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (iskeyboardControl)
        {
            float moveX = 0;
            float moveY = 0;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveY += 0.01f * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveY += -0.01f * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveX += -0.01f * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX += 0.01f * speed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayerManager.Instance.Shoot();
            }
            if (Input.GetKey(KeyCode.X))
            {
                PlayerManager.Instance.Shield();
            }
            transform.position = new Vector3(Mathf.Clamp(moveX + transform.position.x, -width, width), Mathf.Clamp(moveY + transform.position.y, -height, height), 0);
        }
        else
        {
            Vector3 mousepos = maincamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.Clamp(mousepos.x, -width, width), Mathf.Clamp(mousepos.y, -height, height), 0);
            if(Input.GetMouseButtonUp(0))
            {
                PlayerManager.Instance.Shoot();
            }
            else if(Input.GetMouseButton(1))
            {
                PlayerManager.Instance.Shield();
            }
        }
    }
}
