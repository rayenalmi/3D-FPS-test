using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State {Idle , Chasing , Attaking };
    State currentstate;
    float attackDistanceThoreshold = 0.5f;
    float timebeetweenattack = 1;
    float nextattacktime;
    float myCollisionRadius;
    float targetCollisionRadius;
    float damage =1;
    
    LivingEntity targetEntity;

    Color originalcolor;
    Material SkinMaterial;
    NavMeshAgent pathfinder;
    Transform target;

    bool hastarget;
    // Start is called before the first frame update
   protected override void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        SkinMaterial = GetComponent<Renderer>().material;
        originalcolor = SkinMaterial.color;
        base.Start();

        if (GameObject.FindGameObjectWithTag("Player") != null)     
        {
            currentstate = State.Chasing;
            hastarget = true;  
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius; 
            StartCoroutine(UpdatePath());

           
        }
    }


    void OnTargetDeath()
    {
        hastarget = false;
        currentstate = State.Idle;

    }
         

    // Update is called once per frame
    void Update()
    {
        if (hastarget)
        {
            if (Time.time > nextattacktime)
            {

                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarget < Mathf.Pow(attackDistanceThoreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextattacktime = Time.time + timebeetweenattack;
                    StartCoroutine(attack());
                }
            }
        }
       // pathfinder.SetDestination(target.position);
    }


    IEnumerator attack()
    {
        currentstate = State.Attaking; 
        pathfinder.enabled = false;
        Vector3 originalposition = transform.position;

        Vector3 DirTotarget = (target.position - transform.position).normalized;
        Vector3 attackposition = target.position - DirTotarget * (myCollisionRadius );

        float attackspeed = 3f;
        float percent = 0 ;
        bool hasapplidamage = false;

        SkinMaterial.color = Color.red; 

        while (percent <= 1)

        {
            if (percent >= .5f && !hasapplidamage)
            {

                hasapplidamage = true;
                targetEntity.takedamage(damage);

            }
            percent += Time.deltaTime * attackspeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalposition, attackposition, interpolation);
            yield return null;
        }

        SkinMaterial.color = originalcolor;

        currentstate = State.Chasing; 
        pathfinder.enabled = true; 
    }
    IEnumerator UpdatePath()
    {
    
        float refresh = 0.25f;
        while (hastarget)
        {
            if (currentstate == State.Chasing)
            {
                Vector3 DirTotarget = (target.position - transform.position).normalized;
                Vector3 targetposition = target.position - DirTotarget * (myCollisionRadius + targetCollisionRadius /2);
                if (!death)
                {
                    pathfinder.SetDestination(targetposition);
                }
            }
            yield return new WaitForSeconds(refresh);
        }

    }




}

















