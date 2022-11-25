using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{ 
    public Transform bodyPrefab;

    void Start()
    {
        Transform body = Instantiate(bodyPrefab) as Transform;
        Physics.IgnoreCollision(body.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
