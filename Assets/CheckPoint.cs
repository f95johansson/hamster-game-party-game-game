using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
	public class IsHere : UnityEvent<uint>
	{
		private readonly CheckPoint _cp;
		public IsHere(CheckPoint cp)
		{
			_cp = cp;
		}

		public uint Cp
		{
			get { return _cp.Index; }
		}
	}
	
	public IsHere HamsterIsHere;
	public uint Index;

	public void Awake()
	{
		HamsterIsHere = new IsHere(this);
	}

	public void Collided()
	{
		HamsterIsHere.Invoke(Index);
	}
}
