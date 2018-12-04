using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnRelease : MonoBehaviour {

    public GameObject objectTypeToDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.GetComponent<Image>().color = new Color32(0,0,0,255);
        if (other.gameObject.name == objectTypeToDestroy.name + "(Clone)" && Input.GetMouseButtonUp(0))
        {
            
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<Image>().color = new Color32(255,255,255,255);
    }
}
