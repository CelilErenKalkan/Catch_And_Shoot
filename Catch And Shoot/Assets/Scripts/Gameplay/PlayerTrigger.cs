using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private GameObject _ball;
    private GameObject _player;
    private PlayerMovement _playerScript;
    private Animator _anim;
    private int failCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        _ball = GameManager.Instance.ball;
        _player = gameObject;
        _playerScript = _player.GetComponent<PlayerMovement>();
        _anim = _player.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            other.GetComponent<Animator>().SetTrigger("CoinGet");
            GameManager.Instance.ScoreChange(10);
        }
        else if (other.CompareTag("FinishLine") && GameManager.Instance.isPlayable)
        {
            _playerScript.speed = 0;
            _anim.SetBool("isRunning", false);
            GameManager.Instance.confetties.SetActive(true);
        }
        else if (other.CompareTag("Npc"))
        {
            failCounter++;
            Debug.Log(failCounter);
            if(failCounter >= 5)
            {
                GameManager.Instance.LevelCheck();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
