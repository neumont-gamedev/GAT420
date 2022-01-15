using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] Agent[] agents;
    [SerializeField] LayerMask layerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                Instantiate(agents[0], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
            }
        }

    }
}
