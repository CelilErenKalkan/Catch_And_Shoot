using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMultiplier : MonoBehaviour
{
    public Animator anim;
    public int bonusMultiplier;

    private void Start()
    {
        if (TryGetComponent(out Animator anim))
        {
            anim = anim;
        }
        //_anim = gameObject.GetComponent<Animator>();

        if (bonusMultiplier == 1000)
            anim.speed = 1.25f;
        else if (bonusMultiplier == 500)
            anim.speed = 1;
        else if (bonusMultiplier == 100)
            anim.speed = 0.75f;
        else if (bonusMultiplier == 100)
            anim.speed = 0.5f;
    }
}
