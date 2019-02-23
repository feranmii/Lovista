using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : ObjectPhysics
{
    public float maxSpeed = 4;
    public float jumpForce = 5;
    private GameObject playerGameObject;
    private Vector2 originalLocalScale;
    [SerializeField] Vector2 launchPower;
    [SerializeField] float launchRecovery;
    private float launch;
    public Transform footTransform;


    public void Start()
    {
        originalLocalScale = transform.localScale;
        playerGameObject = gameObject;
    }
    protected override void ComputeVelocity()
    {
        
        Vector2 move = Vector2.zero;

        launch += (0 - launch) * Time.deltaTime*launchRecovery;

        move.x = Input.GetAxis("Horizontal") + launch;

        if (move.x > 0.01f) {
            if (playerGameObject.transform.localScale.x < 0) {
                playerGameObject.transform.localScale = new Vector3 (originalLocalScale.x, transform.localScale.y, transform.localScale.z);
            }
        } else if (move.x < -0.01f) {
            if (playerGameObject.transform.localScale.x > 0) {
                playerGameObject.transform.localScale = new Vector3 (-originalLocalScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpForce;
            ParticleSystem o = Instantiate(PlayerManager.Instance.jumpParticles, footTransform.position, Quaternion.identity);
            Destroy(o.gameObject , 1.5f);
        }

        else if(Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }

        PlayerManager.Instance.anim.SetBool ("grounded", grounded);
        PlayerManager.Instance.anim.SetFloat ("velocity", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;

    }

    public void KnockBack(int launchDirection)
    {
        velocity.y = launchPower.y;
        launch = launchDirection * launchPower.x;
        
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(other.collider.gameObject);
        }
    }
    
    
}
