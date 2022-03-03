using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] [Range(1f, 50f)]
    private float speed = 5f;
    private float originalSpeed;

    [Header("Sprint Config")]

    [SerializeField] [Range(1f, 50f)]
    private float sprintSpeed = 7f;

    [SerializeField]
    private Image staminaBar;

    private float maxStamina = 100f;
    private float currentStamina;

    private bool canSprint = true;

    [SerializeField]
    private Gradient staminaBarColor;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        originalSpeed = speed;
        currentStamina = maxStamina;
        UpdateStaminaBar();
    }

    private void Update()
    {
        PlayerMove();
        HandleSprint();
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
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && canSprint)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = originalSpeed;
        }
    }

    private void UpdateStaminaBar()
    {
        float newFill = currentStamina / maxStamina;
        Color newColor = staminaBarColor.Evaluate(newFill);

        staminaBar.fillAmount = newFill;
        staminaBar.color = newColor;
    }
}
