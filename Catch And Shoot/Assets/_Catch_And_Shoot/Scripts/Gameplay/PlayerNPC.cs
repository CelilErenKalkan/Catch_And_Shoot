using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNPC : MonoBehaviour
{
    private GameObject guideLine;
    private GameObject rightHand;
    private NavMeshAgent agent;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        guideLine = gameObject.transform.GetChild(1).gameObject;
        rightHand = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayable && !GameManager.Instance.isPressed)
        {
            Catch();
        }
    }

    void Catch()
    {
        if (transform.position.x != GameManager.Instance.ball.transform.position.x)
        {
            _anim.SetBool("isRunning", true);
            var runaway = (transform.position.x - GameManager.Instance.ball.transform.position.x);
            var newPos = transform.position.x - runaway;
            var destination = new Vector3(newPos, transform.position.y, transform.position.z);
            agent.SetDestination(destination);
        }
    }
}
