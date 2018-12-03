using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    public void SetNumber(Sprite sprite) {
        var barImage = GetComponentInChildren<Image>();
        barImage.sprite = sprite;
    }
}
