using UnityEngine;

public class AntController : MonoBehaviour
{
    private Rigidbody2D body;
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float directionChangeInterval; 
    [SerializeField] private float smoothRotationTime;
    [SerializeField] private float possibleRotationAngle;

    private float rotationInput;
    private float directionChangeTimer;
    [HideInInspector] public float targetRotationAngle;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        SetRandomRotationInput();
    }

    private void Update()
    {
        WalkForward();
        HandleDirectionTimer();
        SmoothRotateAnt();
    }

    private void WalkForward()
    {
        Vector2 forward = transform.up;
        body.velocity = forward * moveSpeed;
    }

/*    void RotateAnt(float rotationInput = 0f)
    {
        if (rotationInput == 0f)
            rotationInput = Random.value < 0.5f ? -Random.value : Random.value; 
        float rotationAmount = -rotationInput * rotationSpeed * Time.deltaTime; 
        transform.Rotate(0, 0, rotationAmount);
    }*/

    private void SmoothRotateAnt()
    {
        float currentRotationAngle = transform.eulerAngles.z;
        float newRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, Time.deltaTime / smoothRotationTime);
        transform.rotation = Quaternion.Euler(0, 0, newRotationAngle);
    }

    private void SetRandomRotationInput()
    {
        rotationInput = Random.value < 0.5f ? -Random.Range(0.5f, 1f) : Random.Range(0.5f, 1f);
        targetRotationAngle = transform.eulerAngles.z + rotationInput * possibleRotationAngle;
    }

    public void ResetTimer()
    {
        directionChangeTimer = directionChangeInterval + Random.Range(-0.5f, 0.5f);
    }

    private void HandleDirectionTimer()
    {
        directionChangeTimer -= Time.deltaTime;

        if (directionChangeTimer <= 0)
        {
            SetRandomRotationInput();
            ResetTimer();
        }
    }
}
