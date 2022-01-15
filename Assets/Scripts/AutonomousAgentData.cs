using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgentData", menuName = "AI/AutonomousAgentData")]
public class AutonomousAgentData : ScriptableObject
{
    [Header("Movement")]
    [Range(0, 20)] public float maxSpeed = 12;
    [Range(0, 20)] public float maxForce = 8;
    [Header("Weight")]
    [Range(0, 5)] public float seekWeight = 1;
    [Range(0, 5)] public float fleeWeight = 1;
    [Range(0, 5)] public float cohesionWeight = 1;
    [Range(0, 5)] public float separationWeight = 1;
    [Range(0, 5)] public float alignmentWeight = 1;
    [Range(0, 20)] public float obstacleWeight = 1;
    [Header("Misc")]
    [Range(0, 10)] public float separationRadius = 1;
}
