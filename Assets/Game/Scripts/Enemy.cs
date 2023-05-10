using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int distance;
    [SerializeField] Animator anim;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

       
    }


    bool isChase = false;
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Vector3.Distance(transform.position,target.position)) < distance)
        {
            anim.SetBool("IsRun",true);
            isChase = true;
        }

        if (Mathf.Abs(Vector3.Distance(transform.position, target.position)) > distance)
        {
            anim.SetBool("IsRun", false);
            isChase = false;
            agent.SetDestination(transform.position);
        }


        if (isChase)
        {
            agent.SetDestination(target.position);
            if (agent.velocity.magnitude > .5f)
                agent.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(agent.velocity).
                    eulerAngles.y, 0);
        }
    }

    //Reference of the moving GameObject that will be corrected
    public GameObject movingObject;

    //Offset postion from where the raycast is cast from
    public Vector3 originOffset;
    public float maxRayDist = 100f;

    //The speed to apply the corrected slope angle
    public float slopeRotChangeSpeed = 10f;

    void FixedUpdate()
    {
        // Align();

        //Get the object's position
        Transform objTrans = movingObject.transform;
        Vector3 origin = objTrans.position;

        //Only register raycast consided as Hill(Can be any layer name)
        int hillLayerIndex = LayerMask.NameToLayer("Terrian");
        //Calculate layermask to Raycast to. 
        int layerMask = (1 << hillLayerIndex);


        RaycastHit slopeHit;

        //Perform raycast from the object's position downwards
        if (Physics.Raycast(origin + originOffset, Vector3.down, out slopeHit, maxRayDist, layerMask))
        {
            //Drawline to show the hit point
            Debug.DrawLine(origin + originOffset, slopeHit.point, Color.red);

            //Get slope angle from the raycast hit normal then calcuate new pos of the object
            Quaternion newRot = Quaternion.FromToRotation(objTrans.up, slopeHit.normal)
                * objTrans.rotation;

            //Apply the rotation 
            objTrans.rotation = Quaternion.Lerp(objTrans.rotation, newRot,
                Time.deltaTime * slopeRotChangeSpeed);

        }
    }

  
}
