using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    [Header("Control Properties")]

    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detetcion Propeties")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Minimap")]
    public GameObject minimap;

    [Header("Player Sounds")]
    public AudioSource jumpSounds;
    public AudioSource hitSound;

    [Header("HealthBar")]
    public HealthBarScreenSpaceController healthBar;

    [Header("Player Abilities")]
    [Range(0, 100)]
    public int health = 100;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        minimap.SetActive(false);
    }

    // Update is called once per frame - once every 16.6666ms

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        // if (isGrounded && velocity.y < 0)
        // {
        //    velocity.y = -2.0f;
        // }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * maxSpeed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            jumpSounds.Play();
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.M)){
            // Toggle Minimap on and off
            minimap.SetActive(!minimap.activeInHierarchy);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hitSound.Play();
        healthBar.TakeDamage(damage);
        if(health < 0){
            health = 0;
        }
    }

   
}