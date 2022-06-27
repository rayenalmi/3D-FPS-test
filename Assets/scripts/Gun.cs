using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform mezzule;
    public ProjecTile projecTile;
    public float msBetwwenshots = 100;
    public float mezzulrvelovity = 35;

    float nextshottime; 

    public void shoots()
    {
        if (Time.time >nextshottime)
        {
            nextshottime = Time.time + msBetwwenshots / 1000;
            ProjecTile newprojecTile = Instantiate(projecTile, mezzule.position, mezzule.rotation) as ProjecTile;
            newprojecTile.SetSpeed(mezzulrvelovity);
        }
    }

   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
