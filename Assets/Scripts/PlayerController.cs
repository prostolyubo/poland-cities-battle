using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;
    public Transform feet, root;

    public Rigidbody2D body;
    public float maxJumpVelocity = 5f;

    bool isFlipped;

    ContactPoint2D[] contacts = new ContactPoint2D[1];

    public Camera cam;

    public PlayerActor target;

    public ControlSchemeDealer controls;

    void FixedUpdate()
    {
        if (!(Input.anyKey || Input.anyKeyDown))
            return;

        isGrounded = body.GetContacts(contacts) > 0;

        bool move = true;

        PlayerActor.Direction direction = PlayerActor.Direction.NONE;
        if (Input.GetKey(controls.scheme.left))
        {
            direction = PlayerActor.Direction.LEFT;
            target.SetFlip(true);
        }
        else if (Input.GetKey(controls.scheme.right))
        {
            direction = PlayerActor.Direction.RIGHT;
            target.SetFlip(false);
        }
        else
            move = false;
        target.Move(direction, isGrounded, false);
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(controls.scheme.jump))
            target.Move(PlayerActor.Direction.NONE, isGrounded, true);

        if (body.velocity.y > maxJumpVelocity)
            body.velocity = new Vector2(body.velocity.x, maxJumpVelocity);

        /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (feet.position.x > mousePos.x)
            isFlipped = true;
        else
            isFlipped = false;*/

        //target.SetFlip(isFlipped);
    }
}
