using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : MonoBehaviour
{
    [SerializeField] GameObject turretPrefab;
    [SerializeField] GameObject transformParent;
    int[] randlist = new int[8];
    int cnt=0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            randlist[i] = i;
        }
        for (int i = 0; i < 8; i++)
        {
            int r = Random.Range(0, 8);
            int temp = randlist[i];
            randlist[i] = randlist[r];
            randlist[r] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        GameObject turret = Instantiate(turretPrefab);
        turret.transform.position = transformParent.transform.GetChild(randlist[cnt%8]).position;
        turret.transform.parent = this.gameObject.transform;
        turret.GetComponent<Turret>().Init(4, 5, 240);
        cnt++;
    }
}
