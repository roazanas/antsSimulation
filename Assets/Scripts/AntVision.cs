using System.Collections.Generic;
using UnityEngine;

public class AntVision : MonoBehaviour
{
    [Header("Raycasting Vision")]
    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;
    [SerializeField] private uint raysCount;

    private float angleStep;
    private AntController controller;

    private void Start()
    {
        viewDistance *= transform.localScale.y;
        angleStep = viewAngle / raysCount;
        controller = GetComponent<AntController>();
    }

    private void Update()
    {
        List<RaycastHit2D> hits = new();

        for (float angle = -viewAngle / 2; angle < viewAngle / 2; angle += angleStep)
        {
            Vector2 rayDirection = Quaternion.Euler(0, 0, angle) * transform.up;
            hits.Add(CastVisionRay(rayDirection));
        }

        HandleRayHits(hits);
        hits.Clear();
    }

    private RaycastHit2D CastVisionRay(Vector2 rayDirection)
    {
        Vector3 rayOrigin = transform.position + transform.up * transform.localScale.y;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, viewDistance);
        Debug.DrawRay(rayOrigin, rayDirection * viewDistance, Color.green);

        return hit;
    }

    private void HandleRayHits(List<RaycastHit2D> hits)
    {
        float targetAngle = float.PositiveInfinity;
        RaycastHit2D closestFoodHit = default;

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.CompareTag("Food"))
            {
                if (closestFoodHit.collider == null || hit.distance < closestFoodHit.distance)
                {
                    closestFoodHit = hit;
                }
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                Vector2 wallNormal = hit.normal;
                targetAngle = Mathf.Atan2(wallNormal.y, wallNormal.x) * Mathf.Rad2Deg - 90f;
            }
        }

        if (closestFoodHit.collider != null)
        {
            targetAngle = Mathf.Atan2(closestFoodHit.point.y - transform.position.y,
                                       closestFoodHit.point.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        }

        if (targetAngle == float.PositiveInfinity) { return; }
        controller.targetRotationAngle = targetAngle;
        controller.ResetTimer();
    }
}
