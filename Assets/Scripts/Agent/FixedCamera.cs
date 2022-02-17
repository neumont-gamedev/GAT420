using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform targetTransform;
    [Range(0, 10)] public float sensitivity = 0.1f;
    [Range(1, 10)] public float rate = 5;
    [Range(1, 10)] public float minDistance = 5;
    [Range(1, 40)] public float maxDistance = 20;

    Vector3 direction;
    float distance;

    private void Start()
    {
        direction = transform.position - targetTransform.position;
        distance = direction.magnitude;
    }

    private void Update()
    {
        distance -= Input.mouseScrollDelta.y * sensitivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 target = targetTransform.position + direction.normalized * distance;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * rate);
    }
}
