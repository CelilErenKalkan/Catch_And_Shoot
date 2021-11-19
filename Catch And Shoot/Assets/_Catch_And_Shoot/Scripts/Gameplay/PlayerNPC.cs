using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNPC : MonoBehaviour
{
    public bool isClosest;
    private GameObject guideLine;
    private GameObject rightHand;
    private GameObject ball;
    private NavMeshAgent agent;
    private Animator _anim;
    private PlayerMovement playerMovement;

    private float _range = 25.0f;
    private float _catchDis = 2.0f;
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        guideLine = gameObject.transform.GetChild(1).gameObject;
        rightHand = gameObject.transform.GetChild(0).transform.GetChild(0)
            .transform.GetChild(0).transform.GetChild(2).transform.GetChild(0)
            .transform.GetChild(0).transform.GetChild(2).transform.GetChild(0)
            .transform.GetChild(0).transform.GetChild(0).gameObject;
        ball = GameManager.Instance.ball;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPlayable && !GameManager.Instance.isPressed && isClosest)
        {
            Catch();
        }

        if (ball.transform.position.z > transform.position.z)
        {
            GameManager.Instance.closest = null;
            isClosest = false;
        }
    }

    void Catch()
    {
        ball = GameManager.Instance.ball;
        if (transform.position.x != ball.transform.position.x)
        {
            float Distance = Vector3.Distance(transform.position, ball.transform.position);
            if (Distance > _range)
            {
                _anim.SetBool("isRunning", true);
                var runaway = (transform.position.x - ball.transform.position.x);
                var newPos = transform.position.x - runaway;
                if (-1 > runaway || runaway > 1)
                {
                    //destination = DestinationCalculator();
                    agent.SetDestination(destination);
                }
                else
                {
                    _anim.SetBool("isRunning", false);
                    transform.LookAt(ball.transform);
                }
            }
            else if (Distance <= _range && Distance > _catchDis)
            {
                agent.SetDestination(ball.transform.position);
            }
            else if (Distance <= _catchDis)
            {
                var y = ball.transform.position.y - transform.position.y;
                if (ball.transform.position.x < transform.position.x || ball.transform.position.x > ball.transform.position.x)
                {
                    var x = ball.transform.position.x - transform.position.x;
                    if (x < -3)
                    {
                        Debug.Log("Jump Right");
                        _anim.Play("Jump Right");
                    }
                    else if (x > 3)
                    {
                        Debug.Log("Jump Left");
                        _anim.Play("Jump Left");
                    }
                }
                else if (y <= 3)
                {
                    Debug.Log("Catch the Ball");
                    _anim.Play("Catch");
                }
                else if (y > 3)
                {
                    Debug.Log("Jump Up");
                    _anim.Play("Jump Up");
                }

                _anim.SetBool("isRunning", false);
                ball.GetComponent<BallScript>().GotCaught();
                //ball.GetComponent<SphereCollider>().enabled = false;
                ball.transform.parent = rightHand.transform;
                ball.transform.localPosition = new Vector3(0.067f, 0.056f, 0.1f);
                agent.enabled = false;
                playerMovement.enabled = true;
                GameManager.Instance.PlayerChange(gameObject);
            }
        }
    }

    //Vector3 DestinationCalculator()
    //{
    //    var z = Mathf.Abs(transform.position.z - ball.transform.position.z);
    //    var angle = Vector3.Angle(GameManager.Instance.player.transform.forward, transform.forward * -1);
    //    var x = Mathf.Tan(angle) * z;
    //    return new Vector3(x, transform.position.y, transform.position.z);
    //}
}
