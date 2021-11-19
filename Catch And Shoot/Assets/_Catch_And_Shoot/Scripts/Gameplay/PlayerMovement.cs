using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float varSpeed = 0.75f;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {
        if (GameManager.Instance.isPlayable && GameManager.Instance.isPressed)
        {
            Vector3 direction = Vector3.forward + Vector3.right * variableJoystick.Horizontal * varSpeed;
            var toLook = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toLook.normalized, Time.fixedDeltaTime * 2);
            transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}