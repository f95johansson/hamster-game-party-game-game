using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public HamsterController HamsterController;
    public GameObject objectTypeToDestroy;

    public bool CareAboutHamsters = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hamster = other.gameObject.GetComponent<HamsterPrefab>();
        if (CareAboutHamsters && hamster)
        {
            HamsterController.SpawnOneHamster(hamster);
        }
        else if (other.gameObject.name.StartsWith(objectTypeToDestroy.name))
        {
            Destroy(other.gameObject);
        }
    }
}