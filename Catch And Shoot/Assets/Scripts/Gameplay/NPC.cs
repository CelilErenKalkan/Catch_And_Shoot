using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator _anim;
    private float _range = 25.0f;
    private float _catchDis = 2.0f;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        target = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayable && GameManager.Instance.isPressed)
        {
            Catch();
        }
    }

    void Catch() // Chasing the player
    {
        float Distance = Vector3.Distance(transform.position, target.transform.position);

        if (Distance <= _catchDis)
        {
            _anim.SetTrigger("Tackle");
            agent.enabled = false;
            target = null;
        }
        else if(Distance <= _range && target.transform.position.z < transform.position.z)
        {
            _anim.SetBool("isRunning", true);
            if(target != null && agent.enabled)
                agent.SetDestination(target.transform.position);
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }
    }
}
