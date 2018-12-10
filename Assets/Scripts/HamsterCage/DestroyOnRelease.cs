using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DestroyOnRelease : MonoBehaviour {
    private Camera _camera;
    private Sprite _sprite;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _camera = Camera.main;
        Assert.IsNotNull(_camera);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sprite = _spriteRenderer.sprite;
    }

    private void Update()
    {
        Vector2 pos = _camera.ViewportToWorldPoint(new Vector2(0, 1));
        var off = _sprite.pivot / _sprite.pixelsPerUnit;
        off.y = - off.y;
        off.x *= transform.lossyScale.x;
        off.y *= transform.lossyScale.y;

        pos += off;

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsHamster(other))
        {
            _spriteRenderer.color = Color.black;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (IsHamster(other))
        {
            _spriteRenderer.color = Color.white;
        }
    }

    private static bool IsHamster(Collision2D other)
    {
        return other.gameObject.GetComponent<HamsterPrefab>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        var hamsterPrefab = other.gameObject.GetComponent<HamsterPrefab>();
        
        if (hamsterPrefab && Input.GetMouseButtonUp(0))
        {
            GameControl.Control.Inventory.hamsterStates[hamsterPrefab.GetIndex()] = null;
            Destroy(other.gameObject);
            GameControl.Control.SaveInventory();
            OnCollisionExit2D(other);
        }
    }
}
