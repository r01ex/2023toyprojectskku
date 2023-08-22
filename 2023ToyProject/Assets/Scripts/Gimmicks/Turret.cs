using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] ATGC atgcPattern;
    [SerializeField] Animator anim;
    int Volley;
    float MoveSpeed;
    int Interval;
    int intervalCnt = 100;
    int letterCnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(intervalCnt<=0)
        {
            anim.SetTrigger("doShot");
            atgcPattern.ShootToPlayer(MoveSpeed, Random.Range(-30f, 30f), letterCnt);
            letterCnt++;
            intervalCnt = Interval;
        }
        if(letterCnt==Volley)
        {
            Destroy(this.gameObject);
        }
        intervalCnt--;
    }
    public void Init(int volley, float moveSpeed, int interval)
    {
        Volley = volley;
        MoveSpeed = moveSpeed;
        Interval = interval;
        intervalCnt = interval;
    }
}
