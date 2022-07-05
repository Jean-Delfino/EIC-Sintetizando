using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatExample : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform bridge;
    
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

        float distance = Vector2.Distance(target.position, this.transform.position);
        print("dista: " + distance);

        bridge.localScale = new Vector3(distance, bridge.localScale.y, bridge.localScale.z);
    }
}

