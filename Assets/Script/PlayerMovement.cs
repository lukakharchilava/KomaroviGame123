using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 1f;
    public float jumpPower = 10f;

    [Header("Player Health")]
    private float PlayerHealth = 200f;
    public float presentHealth;
    public GameObject playerDamage;
    public Health healthBar;

    [Header("player script UI and camera")]
    public GameObject EndGameUI;

    private float gravityFactor = -9.81f;
    public float currentVelY = 0;

    public bool isSprinting = false;
    public float sprintingMultiplier;

    public bool isCrouching = false;
    public float crouchingMulitplier;

    public CharacterController controller;
    public float standingHeight = 1.8f;
    public float crouchingHeight = 1.25f;

    public LayerMask groundMask;
    public Transform groundDetectionTransform;
    
    public float RayCastDistance = 0.6f;            


    public bool isGrounded;

    public void Start()
    {
      
        controller = GetComponent<CharacterController>();
        presentHealth = PlayerHealth;
        healthBar.GiveFullHealth(PlayerHealth);
    }

    public void CheckIsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, RayCastDistance, groundMask))
            isGrounded = true;
        else
            isGrounded = false; 
      
    }

    public void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        CheckIsGrounded();

        if (isGrounded == false)
        {
            currentVelY += gravityFactor * Time.deltaTime;
        }
        else if (isGrounded == true)
        {
            currentVelY = 0;
        }



        if (Input.GetKey(KeyCode.LeftControl))
            isCrouching = true;
        else
            isCrouching = false;

        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            isSprinting = true;
        }
        else
            isSprinting = false;

        Vector3 movement = new Vector3();

        movement = inputX * transform.right + inputY * transform.forward;

        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            movement *= crouchingMulitplier;
        }
        else
        controller.height = standingHeight;

        if (isSprinting == true)
            movement *= sprintingMultiplier;

        controller.Move(movement * movementSpeed * Time.deltaTime);
        controller.Move(new Vector3(0, currentVelY * Time.deltaTime, 0));
    }

    public void PlayerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        StartCoroutine(PlayerDamage());
        healthBar.SetHealth(presentHealth);

        if (presentHealth <= 0)
            playerDie();
    }
    private void playerDie()
    {
        EndGameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }

    IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(1f);
        playerDamage.SetActive(false);
    }
}
