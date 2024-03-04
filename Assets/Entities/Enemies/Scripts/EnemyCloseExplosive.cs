using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCloseExplosive : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int HP = 1;
    [SerializeField] float speed = 3.5f;
    [SerializeField]  float range = 0.45f;
    [SerializeField] GameObject explsionHitbox;
    VariableTimer attackTimer;
    VariableTimer explosionTimer;
    VariableTimer stunTimer;
    bool startExp = false;
    bool getHit = false;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        stunTimer = gameObject.AddComponent(typeof(VariableTimer)) as VariableTimer;
        explosionTimer = gameObject.AddComponent(typeof(VariableTimer)) as VariableTimer;
        attackTimer = gameObject.AddComponent(typeof(VariableTimer)) as VariableTimer;
    }

    private void Update() {
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(transform.position, target.position);
        Debug.Log(distance);
        if (distance <= range){
            startExp = true;
            
        }
        if(startExp){
            agent.speed = 0;
            agent.velocity = new Vector3(0,0,0);
            if(attackTimer.started == false) attackTimer.StartTimer(0.2f);
            if(attackTimer.finished == true){
                if(explosionTimer.started == false){
                    explosionTimer.StartTimer(0.2f);
                } 
                //Debug.Log("chujj");
                explsionHitbox.SetActive(true);
                explsionHitbox.GetComponent<Attack>().Setup(new Vector3(0,0,0), 2, 0.2f, false);
                //start animation
                
                if(explosionTimer.finished == true){
                    Destroy(gameObject);
                }
            }
        }
        if(stunTimer.finished == true){
            stunTimer.ResetTimer();
            
        }
        if(attackTimer.finished == true){
            attackTimer.ResetTimer();
        }
        if(HP <= 0){
            Destroy(gameObject);
        }
    }

    public void Damage(int dmgValue, float stunTime = 0.2f){
        HP -= dmgValue;
        //stunTimer.StartTimer(stunTime);
        agent.velocity = new Vector3(0,0,0);
    }
}
