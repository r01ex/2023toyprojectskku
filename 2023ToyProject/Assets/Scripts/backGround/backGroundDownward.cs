using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundDownward : MonoBehaviour
{
    private float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < -7.5){
            transform.position += new Vector3(0, 7.5f + 7.32f, 0);
        } 
    }
}
