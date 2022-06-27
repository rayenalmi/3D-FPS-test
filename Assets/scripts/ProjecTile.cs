using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecTile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1f;
    float lifetime = 3f;
    float skinWight = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);

        Collider [] initilaCollision = Physics.OverlapSphere(transform.position, .1f, collisionMask);

        if(initilaCollision.Length > 0)
        {
            OnHitObj(initilaCollision[0]);
        }
    }
    public void SetSpeed(float newspeed)
    {
        speed = newspeed;
    }

    void OnHitObj(Collider c)
    {
        IDamageable damageableoject = c.GetComponent<IDamageable>();

        if (damageableoject != null)
        {
            damageableoject.takedamage(damage);
        }

        GameObject.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    void CheckCollision(float movedistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, movedistance +skinWight, collisionMask , QueryTriggerInteraction.Collide))
        {
            OnHit(hit);
        }
    }

    void OnHit(RaycastHit hitt)
    {
        IDamageable damageableoject = hitt.collider.GetComponent<IDamageable>();

        if (damageableoject != null)
        {
            damageableoject.takehit(damage, hitt);
        }

        GameObject.Destroy(gameObject);
       
    }

}
















