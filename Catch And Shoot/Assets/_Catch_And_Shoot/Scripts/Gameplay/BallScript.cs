using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private GameObject parentbone;
    private Rigidbody rigid;
    private Collider coll;
    private Vector3 lastPos;
    private Vector3 curVel;

    // Start is called before the first frame update
    void Start()
    {
        parentbone = transform.parent.gameObject;
        rigid = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<SphereCollider>();
        rigid.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReleaseMe()
    {
        transform.parent = null;
        rigid.useGravity = true;
        coll.isTrigger = false;
        transform.rotation = parentbone.transform.rotation;
        var dir = GameManager.Instance.player.transform.forward + GameManager.Instance.player.transform.up.normalized;
        rigid.AddForce(dir * 500);
    }

    public void GotCaught(GameObject rightHand)
    {
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = true;
        rigid.useGravity = false;
        coll.isTrigger = true;
        transform.parent = rightHand.transform;
        transform.position = new Vector3(0.085f, 0.062f, 0.086f);
        rigid.isKinematic = false;
    }
}
