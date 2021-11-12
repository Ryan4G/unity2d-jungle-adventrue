using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float topLimit = 10.0f;

    public float bottomLimit = -10.0f;

    public float followSpeed = 0.5f;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = transform.position;

            newPosition.y = Mathf.Lerp(newPosition.y, target.position.y, followSpeed);

            newPosition.y = Mathf.Max(Mathf.Min(newPosition.y, topLimit), bottomLimit);

            transform.position = newPosition;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 topPoint = new Vector3(transform.position.x, topLimit, transform.position.z);

        Vector3 bottomPoint = new Vector3(transform.position.x, bottomLimit, transform.position.z);

        Gizmos.DrawLine(topPoint, bottomPoint);
    }
}
