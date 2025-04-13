using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private float speed;
    private bool grounded;

    private void Awake()
    {
        //Grab references for rigidbody and game components from the object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flipping the player based on left-right
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
        }
    
        //jump action of player
        if(Input.GetKey(KeyCode.Space) && grounded){
            Jump();
        }

        //animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
     }   

     private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
     }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
        }
    }
}
