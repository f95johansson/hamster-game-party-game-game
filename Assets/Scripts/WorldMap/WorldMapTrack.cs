
public class WorldMapTrack : WorldMapNode {
	private void OnMouseUpAsButton()
	{
		Navigation.StartTrack(gameObject.name);
	}

	private void Start()
	{
		base.Start();
		Current = 0;
	}
}
