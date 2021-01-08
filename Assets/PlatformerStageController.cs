using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerStageController : MonoBehaviour
{
    private Vector2 moveDir;
    public Rigidbody2D playerRB;
    public float movespeed;
    public float jumpForce;
    private bool jumped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && !jumped)
        {
            playerRB.AddForce(jumpForce * Vector2.up);
            jumped = true;
        }
    }
    private void FixedUpdate()
    {
        playerRB.transform.position += (Vector3)(movespeed * moveDir);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Trap":
                PlayerStateManager.Die();
                break;
            case "Ground":
                if (jumped) jumped = false;
                break;
        }
    }
}
