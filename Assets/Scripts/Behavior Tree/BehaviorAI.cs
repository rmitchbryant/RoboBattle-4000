using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script serves as an interface to implement AI information

public interface BehaviorAI
{

    Vector3 SetTargetPosition(Vector3 targetPosition);
    Transform GetAgentTransform();
    Vector3 GetTargetPosition();
    GameObject GetTarget();
    GameObject SetTarget(GameObject gameObject);
    Transform GetTransform();
}
