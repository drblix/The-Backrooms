using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Transform cameraTransform;

    [Header("Bobbing Config")]

    [SerializeField]
    private float bobFrequency = 5f;
    [SerializeField]
    private float bobHorizontalAmp = 0.1f;
    [SerializeField]
    private float bobVerticalAmp = 0.1f;
    [SerializeField] [Range(0, 1)]
    private float bobSmoothing = 0.1f;

    [SerializeField]
    private bool isMoving = false;
    private float walkingTime;
    private Vector3 targetCamPos;


    private void Update()
    {
        if (!isMoving)
        {
            walkingTime = 0f;
        }
        else
        {
            walkingTime += Time.deltaTime;
        }

        targetCamPos = headTransform.position + CalculateOffset(walkingTime);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCamPos, bobSmoothing);

        if ((cameraTransform.position - targetCamPos).magnitude <= 0.001f)
        {
            cameraTransform.position = targetCamPos;
        }
    }

    private Vector3 CalculateOffset(float time)
    {
        Vector3 offset = Vector3.zero;

        if (time > 0f)
        {
            float horzOffset = Mathf.Cos(time * bobFrequency) * bobHorizontalAmp;
            float vertOffset = Mathf.Sin(time * bobFrequency * 2) * bobVerticalAmp;

            offset = headTransform.right * horzOffset + headTransform.up * vertOffset;
        }

        return offset;
    }

    public void SetMovingState(bool state, float frequency)
    {
        if (state)
        {
            isMoving = state;
            bobFrequency = frequency;
        }
        else
        {
            isMoving = state;
            bobFrequency = 5f;
        }
    }
}
