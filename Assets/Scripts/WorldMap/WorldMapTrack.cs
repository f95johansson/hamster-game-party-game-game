
public class WorldMapTrack : WorldMapNode {
	private void OnMouseUpAsButton()
	{
		Navigation.StartTrack(gameObject.name);
	}
}
