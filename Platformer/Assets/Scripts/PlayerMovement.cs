using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCD;
    private float horizontal;

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    // Start is called before the first frame update
    void Start() {
        // Gets the players sprite
        sprite = GetComponent<SpriteRenderer>();
        
        if(sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }

        // Gets the players rigidbody
        body = GetComponent<Rigidbody2D>();

        // Gets the reference to the animator
        anim = GetComponent<Animator>();    

        // Gets the reference to the box collider
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        Vector3 characterScale = transform.localScale;

        // Flips the sprite depending on the direction the player is moving
        if(horizontal > 0)
        {
            characterScale.x = 1;
        }
        else if(horizontal < 0)
        {
            characterScale.x = -1;
        }
        transform.localScale = characterScale;

        

        // Sets the animation to idle if the player is not moving
        anim.SetBool("isWalking", horizontal != 0);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetBool("onWall", onWall());

        // Wall jump logic
        if(wallJumpCD > 0.2f){
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);

            if(onWall() && !isGrounded()) {
                body.gravityScale = 0;
            } else {
                body.gravityScale = 3;
            }

            if(Input.GetKey(KeyCode.Space)){
                Jump();
            }
        } else {
            wallJumpCD += Time.deltaTime;
        }
    }

    private void Jump() {
        if(isGrounded()){
            anim.SetTrigger("jump");
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
        } else if(onWall() && !isGrounded()) {
            anim.SetTrigger("jump");
            // Mathf.Sign returns the direction the character is facing (+ right, - left)
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, jumpHeight/2);
            wallJumpCD = 0;
        }
    }

    // Checks if the player is grounded with raycast box collider   
    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Checks if the player is touching a wall with raycast box collider
    private bool onWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
