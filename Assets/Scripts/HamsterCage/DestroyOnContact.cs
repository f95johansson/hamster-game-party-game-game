using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public GameObject objectTypeToDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == objectTypeToDestroy.name + "(Clone)"  )
        {
            Destroy(other.gameObject);
        }
        
    }
}