
public class WorldMapTrack : WorldMapNode {
	private void OnMouseUpAsButton()
	{
		Navigation.StartTrack(gameObject.name);
	}

	private void Start()
	{
		Init();
		Current = 0;
	}
}
