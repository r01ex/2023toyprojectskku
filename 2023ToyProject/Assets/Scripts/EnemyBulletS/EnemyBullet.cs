using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int moveNumber = -1;
    float Speed;
    Vector3 Pos1;
    Vector3 Target;
    Vector3 Direction;
    Vector2 InitialVector;
    Vector2 AccelVector;
    float AccelAmount;
    public void move1Init(float speed)
    {
        this.transform.GetChild(0).gameObject.GetComponent<EnemybulletInner>().isHit = false;
        Speed = speed;
        moveNumber = 1;
    }
    void move1(float moveSpeed)
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
    public void move2Init(float speed, Vector3 target)
    {
        this.transform.GetChild(0).gameObject.GetComponent<EnemybulletInner>().isHit = false;
        Speed = speed;
        Target = target;
        moveNumber = 2;
    }
    void move2(float moveSpeed, Vector3 pos2)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, pos2, moveSpeed * Time.deltaTime);
    }

    public void move3Init(float speed, Vector3 moveDir)
    {
        this.transform.GetChild(0).gameObject.GetComponent<EnemybulletInner>().isHit = false;
        Speed = speed;
        Direction = moveDir;
        moveNumber = 3;
    }
    void move3(float moveSpeed, Vector3 moveDir)
    {
        this.transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    public void move4Init(float moveSpeed, Vector2 initialVector, Vector2 accelVector, float accelamount)
    {
        InitialVector = initialVector;
        AccelVector = accelVector;
        AccelAmount = accelamount;
        Speed = moveSpeed;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = InitialVector * Speed;
        moveNumber = 4;
    }
    void move4()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity += AccelVector * AccelAmount * Time.deltaTime;
    }
    public void move5Init(float moveSpeed)
    {
        Speed = moveSpeed;
        moveNumber = 5;
    }
    void move5(float moveSpeed)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerManager.Instance.transform.position, moveSpeed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveNumber)
        {
            case 1:
                move1(Speed);
                break;
            case 2:
                move2(Speed, Target);
                break;
            case 3:
                move3(Speed, Direction);
                break;
            case 4:
                move4();
                break;
            case 5:
                move5(Speed);
                break;
            default:
                break;
        }
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