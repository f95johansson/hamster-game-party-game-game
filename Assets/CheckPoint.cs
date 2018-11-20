using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
	public class IsHere : UnityEvent<int>
	{
		private readonly CheckPoint _cp;
		public IsHere(CheckPoint cp)
		{
			_cp = cp;
		}

		public int Cp
		{
			get { return _cp.Index; }
		}
	}
	
	public IsHere HamsterIsHere;
	public int Index;

	public void Awake()
	{
		HamsterIsHere = new IsHere(this);
	}

	public void Collided()
	{
		HamsterIsHere.Invoke(Index);
	}
}
