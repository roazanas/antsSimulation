using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector2 targetPoint;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.LerpUnclamped(transform.position.x, targetPoint.x, moveSpeed * Time.deltaTime);
        float y = Mathf.LerpUnclamped(transform.position.y, targetPoint.y, moveSpeed * Time.deltaTime);
        transform.position = new Vector2(x, y);
    }
}
