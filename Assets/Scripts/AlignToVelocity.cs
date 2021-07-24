using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToVelocity : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D body;
    // Update is called once per frame
    void Update()
    {
        target.rotation = Quaternion.LookRotation(body.velocity, Vector3.right);
    }
}
