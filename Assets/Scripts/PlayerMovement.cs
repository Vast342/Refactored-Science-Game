using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // adds a public check box to determine if the movement is smooth or snappy
    public bool SmoothMovement;
    // adds a public value for movement speed
    public float movementSpeedX;
    // adds a public value for jump strength which impacts jump hight.
    public float jumpStrength = 20f;
    // adds a value for the strength of the dive, which affects how fast the player falls after diving.
    public float diveForce = -20;
    public Transform feet;
    public LayerMask groundLayers;
    // imports the rigidbody controller from unity itself to be used later on.
    public Rigidbody2D rb;
    // creates a float titled mx or movement x
    float mx;
    // creates a variable to store the number of times the player can jump.
    float jumpCharges;
    static public bool groundCheck = false;
    private void Update() {
        if(SmoothMovement == true) {
            // imports the controls for the horizontal axis, a and d to the float mx
            mx = Input.GetAxis("Horizontal");
        } else {
            mx = Input.GetAxisRaw("Horizontal");
        }
        if  (Input.GetButtonDown("Jump") && jumpCharges >= 1f)
        {
            Jump();
            jumpCharges--;
        }
        if  (Input.GetButtonDown("Fire2"))
        {
            Dive();
        }
        if(isGrounded()){
            jumpCharges = 2f;
        }

        if(!isGrounded() && jumpCharges == 2f) {
            jumpCharges = 1f;
        }
    }
    private void FixedUpdate() {
        // creates a new vector to control horizontal speed, which is equal to movement x times movement speed x. 
        Vector2 movement = new Vector2(mx * movementSpeedX, rb.velocity.y);
            // makes the velocity in the rigidbody2d initialized earlier equal to the movementx vector.
        rb.velocity = movement;
    }
    void Dive() {
        Vector2 movement = new Vector2(rb.velocity.x, diveForce);
        rb.velocity = movement;
    }
    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpStrength);
            rb.velocity = movement;
    }
    public bool isGrounded()
    {
        Collider2D groundCheck1 = Physics2D.OverlapCircle(feet.position, 0.25f, groundLayers);
        if (groundCheck1 != null)
        {
            return true;
        }
        return false;
    }
}

