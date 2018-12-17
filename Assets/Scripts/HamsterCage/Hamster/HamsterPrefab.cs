using UnityEngine;
using UnityEngine.UI;


public class HamsterPrefab : MonoBehaviour
{
    private uint _index;
    public GameObject objectTypeToEat;
    public Slider foodBar;
    private Camera _camera;

    private bool _isGrabbed;

    public void Start()
    {
        foodBar = gameObject.GetComponentInChildren<Slider>();
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _isGrabbed = true;
    }

    private void Update()
    {
        foodBar.value = GameControl.Control.Inventory.hamsterStates[_index].foodLevel;

        if (_isGrabbed)
        {
            var mousePos = Input.mousePosition;
            var mousePosWorld = VectorMath.ToWorldPoint(_camera, mousePos, Vector3.zero, new Vector3(0, 0, 1));
            gameObject.transform.position = (Vector2)mousePosWorld;

            if (Input.GetMouseButtonUp(0))
            {
                _isGrabbed = false;
            }
        }
    }

    public uint GetIndex() {
        return _index;
    }

    public void UpdateScaleWeight()
    {
        var yScale = gameObject.transform.localScale.y;

        if (GameControl.Control.Inventory.hamsterStates[_index].WeightLevel == 0)
        {
            gameObject.transform.localScale = new Vector3(yScale - yScale / 2, yScale, 1);
        }
        else if (GameControl.Control.Inventory.hamsterStates[_index].WeightLevel == 1)
        {
            gameObject.transform.localScale = new Vector3(yScale, yScale, 1);
        }
        else if (GameControl.Control.Inventory.hamsterStates[_index].WeightLevel == 2)
        {
            gameObject.transform.localScale = new Vector3(yScale + yScale / 2, yScale, 1);
        }
    }

    //FOOD
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.StartsWith(objectTypeToEat.name))
        {
            if (GameControl.Control.Inventory.hamsterStates[_index].foodLevel < 5)
            {
                GameControl.Control.Inventory.hamsterStates[_index].foodLevel++;
                GameControl.Control.Inventory.RemoveFood(1);
            }
            Destroy(other.gameObject);
        }
    }


    public void SetWeightLevel(uint weight) {
        GameControl.Control.Inventory.hamsterStates[_index].WeightLevel = weight;
    }

    public void SetIndex(uint i)
    {
        _index = i;
    }
}
