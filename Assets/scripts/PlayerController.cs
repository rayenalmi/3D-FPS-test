using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 vel;
    Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    public void move(Vector3 Velocity)
    {
        vel = Velocity;
    }

    public void LookAt(Vector3 lookpoint)
    {
        Vector3 HeightCorrectPoint = new Vector3(lookpoint.x, transform.position.y, lookpoint.z);
        transform.LookAt(HeightCorrectPoint);


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rg.MovePosition(rg.position + vel * Time.fixedDeltaTime);
    }
}
