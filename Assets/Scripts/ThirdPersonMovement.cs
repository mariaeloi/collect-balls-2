using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    private Vector3 velocity;

    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private bool active;
    private Vector3 spawnPosition;
    private Vector3 bounce = Vector3.zero;

    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] GameObject flagCheckpoint = null;

    void Start()
    {
        active = true;
        spawnPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (active)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if(bounce != Vector3.zero)
                {
                    moveDirection =  bounce;
                    bounce = Vector3.zero;
                }
                controller.Move(moveDirection * speed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // define checkpoint por 100 moedas
            if (Input.GetKeyDown(KeyCode.C))
            {
                int playerCoins = PlayerPrefs.GetInt("playerCoins", 0);
                if (playerCoins >= 100)
                {
                    if (flagCheckpoint != null)
                    {
                        Destroy(flagCheckpoint);
                    }
                    flagCheckpoint = Instantiate(checkpointPrefab, transform.position, transform.rotation);
                    spawnPosition = flagCheckpoint.transform.position;
                    playerCoins -= 100;
                    PlayerPrefs.SetInt("playerCoins", playerCoins);
                    FindObjectOfType<GameController>().SetPlayerCoins(playerCoins);
                }
            }
        }
    }

    public void Fall()
    {
        active = false;
        FindObjectOfType<GameController>().PlayerFall();
    }

    public void Reappear()
    {
        transform.position = spawnPosition;
        Invoke("MakeActive", 0.5f);
    }

    private void MakeActive()
    {
        active = true;
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Bounce"))
        {
            bounce = hit.normal * 50;
        }
    }
}
