using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Track : MonoBehaviour
{
	private SpriteRenderer _spriteRenderer;
	private Texture2D _pixels;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_pixels = _spriteRenderer.sprite.texture;
	}

	public bool GroundAt(IEnumerable<Vector3> positions)
	{
		return positions.Any(GroundAt);
	}
	
	public bool GroundAt(Vector3 position)
	{
		Vector4 position4 = position;
		position4.w = 1; // position has w = 1, direction has w = 0. We are dealing with a position
		
		var local = transform.worldToLocalMatrix * position4;

		var sprite = _spriteRenderer.sprite;
		local.x /= (sprite.rect.width / sprite.pixelsPerUnit);
		local.y /= (sprite.rect.height / sprite.pixelsPerUnit);
		
		var tx = (int) (local.x * _pixels.width + (sprite.rect.center.x));
		var ty = (int) (local.y * _pixels.height + (sprite.rect.center.y));

		if (tx < 0 || ty < 0)
		{
			return false;
		}

		if (tx >= sprite.rect.width || ty >= sprite.rect.height)
		{
			return false;
		}
		
		var color = _pixels.GetPixel(tx, ty);
		return color.a > 0.9;
	}
}
