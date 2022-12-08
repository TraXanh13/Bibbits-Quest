using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMovement : MonoBehaviour
{
    public Transform Player;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

         if(sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }

        // Gets the rigidbody
        body = GetComponent<Rigidbody2D>();

        // Gets the reference to the animator
        anim = GetComponent<Animator>();    

        // Gets the reference to the box collider
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // print(Transform.LookAt(Player));
    }
}
