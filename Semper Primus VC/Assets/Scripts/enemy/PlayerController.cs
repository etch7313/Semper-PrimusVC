using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{


    public Animator MoveANIM;
    public NavMeshAgent agent;
    public GameObject player;
    public float lookRadius = 10;
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance<=lookRadius)
        {
            agent.SetDestination(target.position);
                MoveANIM.SetFloat("x", 0);
                MoveANIM.SetFloat("y", 1);
            if(distance<=agent.stoppingDistance+0.1f)
            {
                FaceTarget();
                float x = 1;
                
                MoveANIM.SetFloat("x", 0);
                MoveANIM.SetFloat("y",0);

            }
        }
       
               
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}