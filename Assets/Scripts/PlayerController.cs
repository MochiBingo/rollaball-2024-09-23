using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject winTextObject;
    public GameObject deathText;
    bool stopwatchActive = true;
    float currentTime;
    public TextMeshProUGUI currentTimeText;
    public float speed;
    public float jumpforce = 2.0f;
    public Vector3 jump;
    public bool isGrounded;

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        currentTime = 0;
        jump = new Vector3(0.0f, 3f, 0.0f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnCollisionStay()
    {
        isGrounded = true;
    }
    private void Update()
    {
        if (stopwatchActive==true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpforce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        movement = Camera.main.transform.rotation * movement;
        rb.AddForce(movement);
        rb.AddForce(movement * speed);
        if (movement.sqrMagnitude > 0.1f && isGrounded)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 1f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            winTextObject.SetActive(true);
            stopwatchActive = false;
            rb.isKinematic = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FLOOR"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            stopwatchActive = false;
            rb.isKinematic = true;
            deathText.SetActive(true);
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
