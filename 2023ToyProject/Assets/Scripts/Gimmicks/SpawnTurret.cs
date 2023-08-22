using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : MonoBehaviour
{
    [SerializeField] GameObject turretPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        GameObject turret = Instantiate(turretPrefab);
        turret.transform.position = new Vector3(this.transform.position.x + Random.Range(-1.4f, 1.4f), this.transform.position.y - Random.Range(0f, 1.5f), 0);
        turret.transform.parent = this.gameObject.transform;
        turret.GetComponent<Turret>().Init(4, 5, 240);
    }
}
