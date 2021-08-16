# Basic Movement 2D Platformer Game In Unity
My basic movement mechanic for 2d platformer game (using keyboard) in Unity Engine.
## INTRODUCTION 
### The code contains several basic movement mechanics for 2d platformer game :
1. Basic horizontal movement. (with and without flipping the object).
2. Basic single jump.
3. Basic cool jump. (hold jump button for longer jump).
4. Basic multiple jump. (multiple jump on air).
5. Basic wall jumping. (jump when touching the wall).
6. Coming soon...
## SETTING UP THE CODE
Place the folder "Platformer2DController" inside the "Assets" folder.

## HOW TO USE ?
### 1. Basic horizontal movement with and without flipping the object :
Aplly the following sample code to your object to move :
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    // Initialize the BasicPlatformer2DMovement object
    BasicPlatformer2DMovement basicMovement = new BasicPlatformer2DMovement();
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void FixedUpdate()
    {
        basicMovement.BasicMoveWithFlip_FixedUpdate(gameObject, speed);
    }
}
```
With non-flipping movement, you just replace the funciton BasicMoveWithFlip_FixedUpdate by BasicMoveWithoutFlip_FixedUpdate.

### 2. Basic single jump with movement :
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    // Initialize the BasicPlatformer2DMovement object
    BasicPlatformer2DMovement basicMovement = new BasicPlatformer2DMovement();
    public float speed;
    
    public float jumpForce;
    private bool isGrounded;
    // A small circle checks whether the player is grounded or not.
    public Transform groundCheck;
    // The radius use for groundCheck
    // Recommended value : 0.03f to 0.5f.
    public float groundRadius;
    // Change the layer of the ground to "Ground", for example.
    // And assign this properties to "Ground" in the inspector to function the mechanic.
    public LayerMask whatIsGround;
    
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        basicMovement.BasicJump_Update(gameObject, jumpForce, isGrounded);
    }

    private void FixedUpdate()
    {
        isGrounded = basicMovement.CheckGrounded_FixedUpdate(groundCheck, groundRadius, whatIsGround);
        basicMovement.BasicMoveWithFlip_FixedUpdate(gameObject, speed);
    }
}
```

### 3. Basic cool jump with movement :
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    // Initialize the BasicPlatformer2DMovement object
    BasicPlatformer2DMovement basicMovement = new BasicPlatformer2DMovement();
    public float speed;
    
    public float jumpForce;
    private bool isGrounded;
    // A small circle checks whether the player is grounded or not.
    public Transform groundCheck;
    // The radius use for groundCheck
    // Recommended value : 0.03f to 0.5f.
    public float groundRadius;
    // Change the layer of the ground to "Ground", for example.
    // And assign this properties to "Ground" in the inspector to function the mechanic.
    public LayerMask whatIsGround;
    
    // Check if the object is jumping or not
    private bool isJumping;
    // Count the duration the object is in air
    private float jumpTime;
    // The maximum time allowed object to stay in the air
    // Recommended value : 0.35f to 0.5f
    public float jumpTimeValue;
   
    // Start is called before the first frame update
    void Start()
    {
        jumpTime = jumpTimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        basicMovement.BasicCoolJump_Update(gameObject, jumpForce, isGrounded, ref isJumping, ref jumpTime, jumpTimeValue);
    }

    private void FixedUpdate()
    {
        isGrounded = basicMovement.CheckGrounded_FixedUpdate(groundCheck, groundRadius, whatIsGround);
        basicMovement.BasicMoveWithFlip_FixedUpdate(gameObject, speed);
    }
}
```

### 4. Basic multiple jump with movement :
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    // Initialize the BasicPlatformer2DMovement object
    BasicPlatformer2DMovement basicMovement = new BasicPlatformer2DMovement();
    public float speed;
    
    public float jumpForce;
    private bool isGrounded;
    // A small circle checks whether the player is grounded or not.
    public Transform groundCheck;
    // The radius use for groundCheck
    // Recommended value : 0.03f to 0.5f.
    public float groundRadius;
    // Change the layer of the ground to "Ground", for example.
    // And assign this properties to "Ground" in the inspector to function the mechanic.
    public LayerMask whatIsGround;
    
    // Count the duration the object is in air
    private int extraJump;
    // The maximum time allowed object to stay in the air
    // Recommended value : 1 to n --> Jump 1 to n additional times in air
    public int extraJumpValue;
   
    // Start is called before the first frame update
    void Start()
    {
        extraJump = extraJumpValue;
    }

    // Update is called once per frame
    void Update()
    {
        basicMovement.BasicJumpNTime_Update(gameObject, jumpForce, isGrounded, ref extraJump, extraJumpValue);
    }

    private void FixedUpdate()
    {
        isGrounded = basicMovement.CheckGrounded_FixedUpdate(groundCheck, groundRadius, whatIsGround);
        basicMovement.BasicMoveWithFlip_FixedUpdate(gameObject, speed);
    }
}
```

### 5. Basic wall jumping setting up (mixed with single jump and movement) :
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    // Initialize the BasicPlatformer2DMovement object
    BasicPlatformer2DMovement basicMovement = new BasicPlatformer2DMovement();
    public float speed;
    
    public float jumpForce;
    private bool isGrounded;
    // A small circle checks whether the player is grounded or not.
    public Transform groundCheck;
    // The radius use for groundCheck
    // Recommended value : 0.03f to 0.5f.
    public float groundRadius;
    // Change the layer of the ground to "Ground", for example.
    // And assign this properties to "Ground" in the inspector to function the mechanic.
    public LayerMask whatIsGround;
    
    // Check if the object is touching front or not
    private bool isTouchingFront;
    // A small circle in the front of the object checks whether the object is touching the wall or not.
    public Transform frontCheck;
    // Radius for the frontCheck
    // Recommended value : equals to the groundRadius
    public float frontRadius;
    // Check if the object is sliding along the wall or not.
    private bool slidingWall;
    // The sliding speed.
    // Recommended value : equals to jumpForce.
    public float slideWallSpeed;
    // Check if the object is jumping along the wall or not.
    private bool wallJumping;
    // The x-axis of the wall jumping force applied to the object.
    // Recommended value : equals to jumpForce.
    public float xWallJumpForce;
    // The y-axis of the wall jumping force applied to the object.
    // Recommended value : equals to jumpForce.
    public float yWallJumpForce;
    // The time the object can jump along the wall.
    // Recommended value : 0.5f to 0.2f.
    public float timeWallJumpingValue;
    // Counter for timeWallJumpingValue variable.
    private float timeWallJumping;
   
    // Start is called before the first frame update
    void Start()
    {
        timeWallJumping = timeWallJumpingValue;
    }

    // Update is called once per frame
    void Update()
    {
        basicMovement.WallJumping_Update(ref slidingWall, ref timeWallJumping, timeWallJumpingValue);
        basicMovement.BasicJump_Update(gameObject, jumpForce, isGrounded);
    }

    private void FixedUpdate()
    {
        isTouchingFront = basicMovement.CheckFront_FixedUpdate(frontCheck, frontRadius, whatIsGround);
        isGrounded = basicMovement.CheckGrounded_FixedUpdate(groundCheck, groundRadius, whatIsGround);
        basicMovement.BasicMoveWithFlip_FixedUpdate(gameObject, speed);
        basicMovement.WallSliding_FixedUpdate(gameObject, ref isTouchingFront, ref slidingWall, slideWallSpeed, xWallJumpForce, yWallJumpForce);
    }
}
```
## NOTE
About the "Recommended value", this is just my recommendation for the fittest value itself for best performance. You can change it whatever you want.

## CONCLUSION
This source code is mostly referenced on the internet and was initially written by me for simplifying my code.\
Recently, I found it helpful if I share it to other people to reuse it as well as changing it if possible.\
You guys can stick around with these function and combined them whatever you want.\
Feel free to change to source code and change them by your own.

Last but not least, if you find any bugs or you have any suggestions to my code, feel free to contact me via email : lthieu1599@gmail.com. Thanks!
