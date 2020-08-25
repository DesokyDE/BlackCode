using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class playerDetact : MonoBehaviour
{
    NavMeshAgent EnemyAgent;
    float distenceToAttack;
    [SerializeField]
    float speed = 3f;
    bool IsPlayerInsight;
    Vector3 NewPos;
    [SerializeField]
    float min, max;
    [SerializeField]
    LayerMask player;
    float timeBtwPoses;
    Collider[] DetactedItiems;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAgent = this.GetComponent<NavMeshAgent>();
        distenceToAttack = 3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        float X = Random.Range(min, max);
        float Z = Random.Range(min, max);
        NewPos = new Vector3(X, 0, Z);
        DetactedItiems = Physics.OverlapSphere(transform.position, 3f,player);
        
        if (DetactedItiems.Length > 0)
        {
            print(DetactedItiems);
            EnemyAgent.SetDestination(DetactedItiems[0].transform.position);
            IsPlayerInsight = true;
            print(IsPlayerInsight);
            
        }
        else if (DetactedItiems.Length == 0)
        {
            IsPlayerInsight = false;
        }

        if (Time.time > timeBtwPoses && !IsPlayerInsight)
        {
            EnemyAgent.SetDestination(NewPos);
            timeBtwPoses = Time.time + 1;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            float Distence = Vector3.Distance(transform.position, other.transform.position);
            if (distenceToAttack == Distence)
            {
                EnemyAgent.speed = 0;
                print("Near Enough To Attack");
            }
            else
            {
                EnemyAgent.speed = speed;
            }
        }
        else
        {
            IsPlayerInsight = false;
            print(IsPlayerInsight);
        }
    }
    
}
