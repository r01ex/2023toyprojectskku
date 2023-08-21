using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomBombForPrefab : MonoBehaviour
{
    float Scale = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(float scale)
    {
        Scale = scale;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += new Vector3(Scale * Time.deltaTime, Scale * Time.deltaTime);
        foreach(Transform c in transform)
        {
            c.localScale = new Vector3(0.1f / this.transform.localScale.x, 0.1f / this.transform.localScale.y);
        }
    }
}
