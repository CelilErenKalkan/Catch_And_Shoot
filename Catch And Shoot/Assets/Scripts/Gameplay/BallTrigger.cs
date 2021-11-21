using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    private GameObject _ball;
    private BallScript _ballScript;
    private Rigidbody rig;
    private bool _once;

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
            _ball.GetComponent<Rigidbody>().useGravity = true;
            _ballScript.ReleaseMe();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bonus") && !_once)
        {
            rig.isKinematic = true;
            rig.velocity = Vector3.zero;
            rig.useGravity = false;
            //_ball.GetComponent<SphereCollider>().enabled = false;
            if (collision.collider.GetComponent<BonusMultiplier>().anim != null)
            collision.collider.GetComponent<Animator>().enabled = false;
            var x = collision.collider.GetComponent<BonusMultiplier>().bonusMultiplier;
            GameManager.Instance.ScoreChange(x);
            GameManager.Instance.success = true;
            GameManager.Instance.LevelCheck();
            _once = true;
        }
    }
}
