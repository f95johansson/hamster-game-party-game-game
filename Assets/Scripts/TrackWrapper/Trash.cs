using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Trash : MonoBehaviour
{
	private Vector3 _trashStart;
	private Image _image;
	private bool _close;

	[FormerlySerializedAs("TrashOpen")] public Sprite ImageOpen;
	private Sprite _imageClosed;

	[Range(0, 1)]
	public float AnimationSpeed = 0.1f;
	public float CloseRadius = 8;
	public float AnimatingRadius = 20;

	private void Start()
	{
		_trashStart = transform.position;
		_image = GetComponent<Image>();
		_imageClosed = _image.sprite;
	}

	public void UpdateTrashCan(Vector3 mousePos, bool isSomethingGrabbed)
	{
		var canvasMousePos = ToCanvasSpace(mousePos);
		
		var distance = Vector3.Distance(canvasMousePos, transform.position);
		_close = isSomethingGrabbed && distance < CloseRadius;
		_image.sprite = (_close && isSomethingGrabbed) ? ImageOpen : _imageClosed;

		Vector3 pos;
		if (isSomethingGrabbed && distance < AnimatingRadius)
		{	
			var dir = (canvasMousePos - _trashStart) * 0.2f;
			var magnitude = dir.magnitude;

			if (magnitude > CloseRadius)
			{
				dir *= (CloseRadius / magnitude);
			}
		
			pos = _trashStart + dir;
		}
		else
		{
			pos = _trashStart;
		}
		
		transform.position = Vector3.Lerp(transform.position, pos, AnimationSpeed);
	}

	private static Vector3 ToCanvasSpace(Vector3 mousePos)
	{
		return mousePos;
	}

	public bool IsClose()
	{
		return _close;
	}
}
