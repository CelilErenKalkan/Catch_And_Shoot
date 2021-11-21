using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private GameObject parentbone;
    private Rigidbody rigid;
    private Collider coll;
    [SerializeField]private bool hasThrown;
    private float constantSpeed = 20.0f;

    private int layer_mask;

    // Start is called before the first frame update
    void Start()
    {
        parentbone = transform.parent.gameObject;
        rigid = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<SphereCollider>();
        rigid.useGravity = false;

         layer_mask = LayerMask.GetMask("Detector");
    }

    // Update is called once per frame
    void Update()
    {

        if (hasThrown) // Calculation of where to the ball has thrown
        {
            transform.parent = null;
            rigid.useGravity = true;
            rigid.isKinematic = false;
            coll.isTrigger = false;
            transform.rotation = parentbone.transform.rotation;
            var dir = GameManager.Instance.player.transform.forward;
            rigid.velocity = constantSpeed * (dir);

            Ray ray = new Ray(GameManager.Instance.player.transform.position, GameManager.Instance.player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, layer_mask))
            {
                if (hit.collider.isTrigger && hit.collider.CompareTag("Detector"))
                {
                    if (GameManager.Instance.closest != null)
                    {
                        GameManager.Instance.closest.GetComponent<PlayerNPC>().destination = hit.point;
                        hit.collider.isTrigger = true;
                    }
                }
            }
        }
    }

    #region Throw Mechanics

    public void ReleaseMe() // Detects if ball has thrown or not
    {
        if (!hasThrown)
            hasThrown = true;
        else if (hasThrown)
            hasThrown = false;
    }

    public void GotCaught() // Lets the player to catch the ball
    {
        ReleaseMe();
        coll.isTrigger = true;
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
        rigid.isKinematic = true;
    }

    public bool GetThrown()
    {
        return hasThrown;
    }

    #endregion
}
