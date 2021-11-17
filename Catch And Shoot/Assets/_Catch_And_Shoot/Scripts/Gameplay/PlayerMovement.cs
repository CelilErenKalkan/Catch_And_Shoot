using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float varSpeed = 0.5f;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {
        if (GameManager.Instance.isPlayable && !GameManager.Instance.isPressed)
            Debug.Log("Throw");
        else if (GameManager.Instance.isPlayable)
        {
            Vector3 direction = Vector3.forward * varSpeed + Vector3.right * variableJoystick.Horizontal * varSpeed;
            transform.LookAt(transform.position + direction);
            transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}