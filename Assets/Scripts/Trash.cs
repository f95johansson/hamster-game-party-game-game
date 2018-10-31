using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Trash : MonoBehaviour
{
	private Vector3 _trashStart;
	private Image _image;
	private bool _close;

	public Sprite TrashOpen;
	public Sprite TrashClosed;

	[Range(0, 1)]
	public float AnimationSpeed = 0.1f;
	public float CloseRadius = 8;
	public float AnimatingRadius = 20;

	private void Start()
	{
		_trashStart = transform.position;
		_image = GetComponent<Image>();
	}

	public void UpdateTrashCan(Vector3 canvasMousePos, bool isSomethingGrabbed)
	{
		var distance = Vector3.Distance(canvasMousePos, transform.position);
		
		_close = isSomethingGrabbed && distance < CloseRadius;
		
		_image.sprite = (_close && isSomethingGrabbed) ? TrashOpen : TrashClosed;

		Vector3 pos;
		if (isSomethingGrabbed && distance < AnimatingRadius)
		{	
			var dir = (canvasMousePos - _trashStart) * 0.2f;
			var magnitude = dir.magnitude;

			if (magnitude > CloseRadius)
			{
				dir *= (CloseRadius / magnitude);
			}
		
			pos = dir;
		}
		else
		{
			pos = Vector3.zero;
		}
		
		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, AnimationSpeed);
	}

	public bool IsClose()
	{
		return _close;
	}
}
