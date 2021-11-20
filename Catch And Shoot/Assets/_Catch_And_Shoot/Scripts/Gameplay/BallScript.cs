﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private GameObject parentbone;
    private Rigidbody rigid;
    private Collider coll;
    [SerializeField]private bool hasThrown;
    private float constantSpeed = 30.0f;

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

        if (hasThrown)
        {
            transform.parent = null;
            rigid.useGravity = true;
            coll.isTrigger = false;
            transform.rotation = parentbone.transform.rotation;
            var dir = GameManager.Instance.player.transform.forward;
            rigid.velocity = constantSpeed * (dir);

            Ray ray = new Ray(GameManager.Instance.player.transform.position, GameManager.Instance.player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.isTrigger && hit.collider.CompareTag("Member"))
                {
                    GameManager.Instance.closest.GetComponent<PlayerNPC>().destination = hit.point;
                }
            }
        }
    }

    public void ReleaseMe()
    {
        if (!hasThrown)
            hasThrown = true;
        else if (hasThrown)
            hasThrown = false;
    }

    public void GotCaught()
    {
        ReleaseMe();
        coll.isTrigger = true;
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
    }

    public bool GetThrown()
    {
        return hasThrown;
    }
}
