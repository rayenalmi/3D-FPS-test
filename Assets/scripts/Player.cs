using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity
{
    public float movespeed = 5;
    PlayerController controller;


    Camera ViewCamera;



    GunController gunController;

    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        ViewCamera = Camera.main ;
    }

    // Update is called once per frame
    void Update()
    {
        // move conrole
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
        Vector3 movevolocity = moveInput.normalized * movespeed;

        controller.move(movevolocity);




        // look at 

        Ray ray = ViewCamera.ScreenPointToRay(Input.mousePosition) ;

        Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

        float RayDistance;



        if(groundPlane.Raycast(ray , out RayDistance))
            {   
            Vector3 point = ray.GetPoint(RayDistance);

            // Debug.DrawLine(ray.origin, point, Color.red);

            controller.LookAt(point);
            
            }

        // shoot input 
        if (Input.GetMouseButton(0))
        {
            gunController.shoot(); 
        }

    }
}





























