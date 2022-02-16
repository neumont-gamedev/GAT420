using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityController : MonoBehaviour
{
    [SerializeField] Agent agent;
    [SerializeField] LayerMask layerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, layerMask))
            {
                agent.movement.MoveTowards(raycastHit.point);
            }
        }
    }
}