using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatformer2DMovement
{
    bool wallJumpingLocal;
    /// <summary>
    /// Move the "objectToMove" without flipping and speed "speed".
    ///  Used this function in FixedUpdate function for best performance
    /// </summary>
    /// <param name="objectToMove"></param>
    /// <param name="speed"></param>
    public void BasicMoveWithoutFlip_FixedUpdate(GameObject objectToMove, float speed)
    {
        Rigidbody2D rb = objectToMove.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody2D component, please add it to the object");
            return;
        }
        // Move left-right mechanism
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    /// <summary>
    /// Move the "objectToMove" with speed "speed" and flipping towards the move direction.
    ///  Used this function in FixedUpdate function for best performance
    /// </summary>
    /// <param name="objectToMove"></param>
    /// <param name="speed"></param>
    public void BasicMoveWithFlip_FixedUpdate(GameObject objectToMove, float speed)
    {
        Rigidbody2D rb = objectToMove.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody2D component, please add it to the object");
            return;
        }
        // Move left-right mechanism
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        Flip(moveHorizontal, objectToMove);
    }

    void Flip(float moveHorizontal, GameObject objectToFlip)
    {
        Vector3 Scaler = objectToFlip.transform.localScale;
        if (moveHorizontal > 0)
            Scaler.x = 1;
        else if (moveHorizontal < 0)
            Scaler.x = -1;
        objectToFlip.transform.localScale = Scaler;
    }

    /// <summary>
    /// Apply single jump to the object "objectToJump" with the jump force "jumpForce". 
    ///  Used this function in Update function for best performance
    /// </summary>
    /// <param name="objectToJump"></param>
    /// <param name="jumpForce"></param>
    /// <param name="isGrounded"></param>
    /// <param name="jumpButton"></param>
    /// <param name="dust"></param>
    /// <param name="groundCheck"></param>
    public void BasicJump_Update(GameObject objectToJump, float jumpForce, bool isGrounded, KeyCode jumpButton = KeyCode.Space, GameObject dust = null, Transform groundCheck = null)
    {
        Rigidbody2D rb = objectToJump.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody2D component, please add it to the object");
            return;
        }
        if (isGrounded == true && Input.GetKeyDown(jumpButton))
        {
            if (dust != null && groundCheck != null)
                MonoBehaviour.Instantiate(dust, groundCheck.position, Quaternion.identity);
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    /// <summary>
    /// Return true if the object is grounded and false if not
    /// </summary>
    /// <param name="groundCheck"></param>
    /// <param name="groundCheckRadius"></param>
    /// <param name="whatIsGround"></param>
    /// <returns></returns>
    public bool CheckGrounded_FixedUpdate(Transform groundCheck, float groundCheckRadius, LayerMask whatIsGround)
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// Return true if the object is touch the front wall and false if not
    /// </summary>
    /// <param name="frontCheck"></param>
    /// <param name="frontCheckRadius"></param>
    /// <param name="whatIsGround"></param>
    /// <returns></returns>
    public bool CheckFront_FixedUpdate(Transform frontCheck, float frontCheckRadius, LayerMask whatIsGround)
    {
        return Physics2D.OverlapCircle(frontCheck.position, frontCheckRadius, whatIsGround);
    }

    /// <summary>
    /// Apply cool jump to the object "objectToJump" with the jump force "jumpForce".
    ///  Cool jump means "hold the jump button for longer jump". The duration you can jump is represented by "jumpTimeValue"
    ///  Used this function in Update function for best performance. Initially, assign jumpTimeValue to jumpTime in Awake() or Start() for the parameter "jumpTime". "isJumping" is also assign to FALSE in Awake() or Start().
    /// </summary>
    /// <param name="objectToJump"></param>
    /// <param name="jumpForce"></param>
    /// <param name="isGrounded"></param>
    /// <param name="isJumping"></param>
    /// <param name="jumpTime"></param>
    /// <param name="jumpTimeValue"></param>
    /// <param name="jumpButton"></param>
    public void BasicCoolJump_Update(GameObject objectToJump, float jumpForce, bool isGrounded, ref bool isJumping, ref float jumpTime, float jumpTimeValue, KeyCode jumpButton = KeyCode.Space, GameObject dust = null, Transform groundCheck = null)
    {
        Rigidbody2D rb = objectToJump.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody2D component, please add it to the object");
            return;
        }
        if (isGrounded == true && Input.GetKeyDown(jumpButton))
        {
            isJumping = true;
            jumpTime = jumpTimeValue;
            rb.velocity = Vector2.up * jumpForce;
            if (dust != null && groundCheck != null)
                MonoBehaviour.Instantiate(dust, groundCheck.position, Quaternion.identity);
        }

        if (isJumping == true && Input.GetKey(jumpButton))
        {
            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(jumpButton))
        {
            isJumping = false;
        }
    }

    /// <summary>
    /// Apply N-Jump to the object "objectToJump" with the jump force "jumpForce".
    ///  Jump "extraJumpValue - 1" times in the air. Initially, assign extraJumpValue to extraJump in Awake() or Start() for the parameter "extraJump"
    /// </summary>
    /// <param name="objectToJump"></param>
    /// <param name="jumpForce"></param>
    /// <param name="isGrounded"></param>
    /// <param name="extraJump"></param>
    /// <param name="extraJumpValue"></param>
    /// <param name="jumpButton"></param>
    /// <param name="dust"></param>
    /// <param name="groundCheck"></param>
    public void BasicJumpNTime_Update(GameObject objectToJump, float jumpForce, bool isGrounded, ref int extraJump, int extraJumpValue, KeyCode jumpButton = KeyCode.Space, GameObject dust = null, Transform groundCheck = null)
    {
        Rigidbody2D rb = objectToJump.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody2D component, please add it to the object");
            return;
        }
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }
        if (Input.GetKeyDown(jumpButton) && extraJump > 0)
        {
            if (dust != null && groundCheck != null)
                MonoBehaviour.Instantiate(dust, groundCheck.position, Quaternion.identity);
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if (Input.GetKeyDown(jumpButton) && extraJump == 0 && isGrounded == true)
        {
            if (dust != null && groundCheck != null)
                MonoBehaviour.Instantiate(dust, groundCheck.position, Quaternion.identity);
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    /// <summary>
    /// Apply the sliding mechanic when the object is touching the wall, slide down with speed "slideWallSpeed". Also apply the wall jumping mechanic with the force in 2 axis "xWallJumpForce" and "yWallJumpForce" out of the wall. Put this function into FixedUpdate() for best performance.
    /// </summary>
    /// <param name="objectToSlide"></param>
    /// <param name="isTouchingFront"></param>
    /// <param name="slidingWall"></param>
    /// <param name="slideWallSpeed"></param>
    /// <param name="xWallJumpForce"></param>
    /// <param name="yWallJumpForce"></param>
    public void WallSliding_FixedUpdate(GameObject objectToSlide, ref bool isTouchingFront, ref bool slidingWall, float slideWallSpeed, float xWallJumpForce, float yWallJumpForce)
    {
        Rigidbody2D rb = objectToSlide.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody2D component, please add it to the object");
            return;
        }
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (isTouchingFront == true && moveHorizontal != 0)
        {
            slidingWall = true;
        }
        else
        {
            slidingWall = false;
        }
        if (slidingWall == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -slideWallSpeed);
        }
        if (wallJumpingLocal == true)
        {
            rb.velocity = new Vector2(xWallJumpForce * -moveHorizontal, yWallJumpForce);
        }
    }

    /// <summary>
    /// Used this to complete the wall jumping mechanic together with the function "WallSliding_FixedUpdate". Put this function into Update() for best performance. "timeWallJumpingValue" is usually in range (0,1) second. Initially, assign timeWallJumpingValue to timeWallJumping in Awake() or Start() for the parameter "timeWallJumping" 
    /// </summary>
    /// <param name="slidingWall"></param>
    /// <param name="timeWallJumping"></param>
    /// <param name="timeWallJumpingValue"></param>
    public void WallJumping_Update(ref bool slidingWall, ref float timeWallJumping, float timeWallJumpingValue)
    {
        if (Input.GetKeyDown(KeyCode.Space) && slidingWall == true)
        {
            wallJumpingLocal = true;
        }
        if (wallJumpingLocal == true)
        {
            if (timeWallJumping > 0)
            {
                timeWallJumping -= Time.deltaTime;
            }
            else
            {
                wallJumpingLocal = false;
                timeWallJumping = timeWallJumpingValue;
            }
        }
    }

}
