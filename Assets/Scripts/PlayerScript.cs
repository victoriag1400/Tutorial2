using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public Text win;
    private int scoreValue = 0;
    private int livesValue = 3;
    Animator anim; 
    private bool facingRight = true;
    private bool isJumping;
   
   

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
       lives.text = livesValue.ToString();
        win.text = "";
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
       
       if (Input.GetKeyDown(KeyCode.D))
       {
           anim.SetInteger("State", 1);
       }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
       {
           anim.SetInteger("State", 1);
       }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
         if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
       
       

        if (verMovement != 0)
       {
           isJumping = true;
            anim.SetInteger("State", 2);
       }
      
       if (verMovement == 0)
       {
           isJumping = false;
            anim.SetInteger("State", 0);
       }
       else if (verMovement == 0 && isJumping == true)
       {
            anim.SetInteger("State", 2);
       }

        if (facingRight == false && hozMovement > 0)
         {
             Flip();
         }
        else if (facingRight == true && hozMovement < 0)
         {
             Flip();
         }


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1; 
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
        {
           win.text = "You Win! Game created by Victoria Gillis";
           Destroy(this);
        }
        }
         if (collision.collider.tag == "Enemy")
        {
           livesValue -=1;
           lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue == 0)
            {
                win.text = "You Lose! Game created by Victoria Gillis";
                Destroy(this);
            }
            
        }
        
    }
    
   

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            
        }
    }
  
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
