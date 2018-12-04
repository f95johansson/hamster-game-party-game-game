using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public HamsterController hamsterController;
    public GameObject objectTypeToDestroy;

    public GameObject objectTypeToGenerate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == objectTypeToDestroy.name + "(Clone)"  )
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == objectTypeToGenerate.name+"(Clone")
        {
            hamsterController.SpawnOneHamster(other.gameObject);
        }
        
    }
}