using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{ 
  

   void Start()
    {

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collider collOther = other.gameObject.GetComponent<Collider>();
            Collider collThis = GetComponent<Collider>();
            Physics.IgnoreCollision(collOther, collThis);
        }
    }
}
