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
            GameControl.Control.Inventory.hamsterStates[other.gameObject.GetComponent<HamsterPrefab>().getIndex()] = null;
            Destroy(other.gameObject);
        }

    }

    public void HamsterOnYou(HamsterPrefab hamster) {
        this.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        if (Input.GetMouseButtonUp(0))
        {
            GameControl.Control.Inventory.hamsterStates[hamster.getIndex()] = null;
            Destroy(hamster.gameObject);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("hi");
    }

    public void OnHamsterLeave()
    {
        this.GetComponent<Image>().color = new Color32(255,255,255,255);
    }
}
