using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    private GameObject _ball;
    private BallScript _ballScript;
    private Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        _ball = gameObject;
        _ballScript = _ball.GetComponent<BallScript>();
        rig = _ball.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin") && _ballScript.GetThrown())
        {
            other.GetComponent<Animator>().SetTrigger("CoinGet");
            GameManager.Instance.ScoreChange(10);
        }
        else if (other.CompareTag("Edge") && _ballScript.GetThrown())
        {
            GameManager.Instance.LevelCheck();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bonus"))
        {
            rig.isKinematic = true;
            rig.velocity = Vector3.zero;
            rig.useGravity = false;
            var x = collision.collider.GetComponent<BonusMultiplier>().bonusMultiplier;
            GameManager.Instance.ScoreChange(x);
            GameManager.Instance.success = true;
            GameManager.Instance.LevelCheck();
        }
    }
}
