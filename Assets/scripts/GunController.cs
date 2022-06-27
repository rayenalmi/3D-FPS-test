using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform WeapenHold;
    public Gun StartingGun;
    Gun Equipedgun;
    
    // Start is called before the first frame update
    void Start()
    {
       if (StartingGun != null )
        {
            EquipGun(StartingGun);
            
        }
    }
    public void shoot()
    {
        if (Equipedgun != null)
        {
            Equipedgun.shoots();
        }
    }
    public void EquipGun (Gun gotoEquip)
    {
        if (Equipedgun != null)
        {
            Destroy(Equipedgun.gameObject);
        }

        Equipedgun = Instantiate(gotoEquip, WeapenHold.position,WeapenHold.rotation) as Gun;
        Equipedgun.transform.parent = WeapenHold;


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

























