using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
        public void ThrowBall()
    {
        Debug.Log("Throwing");
        BallScript ballscript = GameManager.Instance.ball.GetComponent<BallScript>();
        ballscript.ReleaseMe();
    }
}
