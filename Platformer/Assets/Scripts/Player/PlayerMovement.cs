using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private AudioClip sound;
    private AudioSource soundSource;

    AudioSource audioSrc;

    private float jumpCD;
    private float horizontal;

    private bool canDoubleJump = true;

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask sideBorderLayer;
    [SerializeField] private Image doubleJumpImg;

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

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        Vector3 characterScale = transform.localScale;

        // Flips the sprite depending on the direction the player is moving
        if(horizontal > 0) {
            playMovementAudio();
            characterScale.x = 1;
        }
        else if(horizontal < 0) {
            playMovementAudio();
            characterScale.x = -1;
        }
        else {
            audioSrc.Stop();
        }

        transform.localScale = characterScale;      

        // Sets the animation to idle if the player is not moving
        anim.SetBool("isWalking", horizontal != 0);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetBool("onWall", onWall());

        // Wall jump logic
        if(jumpCD > 0.3f){
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
            jumpCD += Time.deltaTime;
        }

        if(onWall() || onBorder())
            wallSlide();

        setDoubleJumpFlag();
    }

    private void wallSlide(){
        if(body.velocity.y <= 0){
            body.velocity = new Vector2(body.velocity.x, -3);
        }
    }

    private void Jump() {
        // Normal Jump
        if(isGrounded()){
            anim.SetTrigger("jump");
            body.velocity = new Vector2(body.velocity.x * Mathf.Sign(Time.deltaTime), jumpHeight);
            jumpCD = 0;
        }
        // Wall Jump
        else if(onWall() && !isGrounded()) {
            anim.SetTrigger("jump");
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2, jumpHeight/2);
            jumpCD = 0;
        }
        // Double Jump
        else if(!onWall() && !isGrounded() && canDoubleJump){
            doubleJumpImg.color = Color.grey;
            anim.SetTrigger("doubleJump");
            canDoubleJump = false;
            jumpCD = 0;
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
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

    // Checks if the player is touching a wall with raycast box collider
    private bool onBorder() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.1f, sideBorderLayer);
        return raycastHit.collider != null;
    }

    private void setDoubleJumpFlag(){
        if(isGrounded()){
            canDoubleJump = true;
            doubleJumpImg.color = Color.white;
        }
    }

    private void playMovementAudio() {
        if(isGrounded() && !audioSrc.isPlaying){
            audioSrc.Play();
        } else if(!isGrounded()) {
            audioSrc.Stop();
        }
    }
}
