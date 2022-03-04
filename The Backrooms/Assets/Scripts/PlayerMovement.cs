using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] [Range(1f, 50f)]
    private float speed = 5f;
    private float originalSpeed;

    private bool isMoving = false;
    private Vector3 oldPos;

    [Header("Sprint Config")]

    [SerializeField] [Range(1f, 50f)]
    private float sprintSpeed = 7f;

    [SerializeField]
    private Image staminaBar;
    [SerializeField]
    private TextMeshProUGUI staminaText;

    private float maxStamina = 100f;
    [SerializeField]
    private float currentStamina;

    private bool canSprint = true;
    private bool sprinting = false;

    [SerializeField]
    private Gradient staminaBarColor;
    [SerializeField]
    private GameObject tiredText;

    private CameraBobbing cameraBobbing;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraBobbing = FindObjectOfType<CameraBobbing>();

        tiredText.SetActive(false);

        originalSpeed = speed;
        currentStamina = maxStamina;
        oldPos = new Vector3(0f, 0f, 0f);
        UpdateStaminaBar();
    }

    private void Update()
    {
        PlayerMove();
        HandleSprint();
        RechargeSprint();
        CheckMovement();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(speed * Time.deltaTime * move);
    }

    private void HandleSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && canSprint && isMoving)
        {
            sprinting = true;
            cameraBobbing.SetMovingState(true, 7f);

            speed = sprintSpeed;

            currentStamina -= 0.35f;
            UpdateStaminaBar();
        }
        else
        {
            sprinting = false;
            speed = originalSpeed;
        }
    }

    private void RechargeSprint()
    {
        if (!sprinting && currentStamina < maxStamina)
        {
            currentStamina += 0.2f;
        }

        if (currentStamina < 0.05f)
        {
            StartCoroutine(PauseSprint());
        }

        if (currentStamina > maxStamina)
        {
            currentStamina = 100f;
        }

        UpdateStaminaBar();
    }

    private IEnumerator PauseSprint()
    {
        canSprint = false;
        tiredText.SetActive(true);

        yield return new WaitUntil(() => currentStamina == maxStamina);

        canSprint = true;
        tiredText.SetActive(false);
    }

    private void CheckMovement()
    {
        if (transform.position != oldPos)
        {
            isMoving = true;

            if (!sprinting)
            {
                cameraBobbing.SetMovingState(true, 5f);
            }
        }
        else
        {
            isMoving = false; 
            cameraBobbing.SetMovingState(false, 5f);
        }

        oldPos = transform.position;
    }

    private void UpdateStaminaBar()
    {
        float newFill = currentStamina / maxStamina;
        Color newColor = staminaBarColor.Evaluate(newFill);

        staminaBar.fillAmount = newFill;
        staminaBar.color = newColor;
        staminaText.color = newColor;
    }
}
