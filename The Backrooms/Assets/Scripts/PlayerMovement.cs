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
        RechargeSprint();
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
            sprinting = true;
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

        yield return new WaitUntil(() => currentStamina == maxStamina);

        canSprint = true;
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
