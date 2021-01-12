using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerStageController : MonoBehaviour
{
    private Vector2 moveDir;
    private float prevSign = 0;
    private SpriteRenderer plSR;
    public Rigidbody2D playerRB;
    public Animator amr;
    public float movespeed;
    public float jumpForce;
    private bool jumped;
    private bool forceUpdate;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        plSR = GetComponent<SpriteRenderer>();
        amr = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerStateManager.Die();
        }
        moveDir.x = Input.GetAxisRaw("Horizontal");
        float newSign = moveDir.x.Sign();
        if (forceUpdate)
        {
            switch (newSign)
            {
                case 1:
                    plSR.flipX = false;
                    PlayAnimation("run");
                    break;
                case 0:
                    PlayAnimation("Idle");
                    break;
                case -1:
                    plSR.flipX = true;
                    PlayAnimation("run");
                    break;
            }
            forceUpdate = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jumped)
        {
            playerRB.AddForce(jumpForce * Vector2.up);
            jumped = true;
            PlayAnimation("jump");
        }
        if (jumped)
        {
            plSR.flipX = newSign >= 0 ? false : true;
        }
        if (!jumped && prevSign != newSign)
        {
            switch (newSign)
            {
                case 1:
                    plSR.flipX = false;
                    PlayAnimation("run");
                    break;
                case 0:
                    PlayAnimation("Idle");
                    break;
                case -1:
                    plSR.flipX = true;
                    PlayAnimation("run");
                    break;
            }
        }
        prevSign = newSign;
    }
    private void PlayAnimation(string name)
    {
        amr.Play("Base Layer." + name);
    }
    private void FixedUpdate()
    {
        playerRB.transform.position += (Vector3)(movespeed * moveDir);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Confetti":
                collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                break;
            case "NextLevel":
                collision.GetComponent<Animator>().Play("Open");
                Invoke("LoadNextLevel", 2f);
                break;
        }
    }
    private void LoadNextLevel()
    {
        LevelLoader.LoadNextLevel();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Confetti")
        {
            PlayerStateManager.Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        switch (collision.collider.tag)
        {
            case "Trap":
                PlayerStateManager.Die();
                break;
            case "Ground":
                if (jumped) { jumped = false; PlayAnimation("Idle"); forceUpdate = true; Update(); }
                break;
        }
    }
}
