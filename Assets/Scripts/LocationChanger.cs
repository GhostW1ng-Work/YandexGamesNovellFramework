using Articy.Testproect;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class LocationChanger : MonoBehaviour
{
	private const string CURRENT_LOCATION_INDEX = "CurrentLocationIndex";

	[SerializeField] private Image _backgroundImage;
	[ArticyTypeConstraint(typeof(ILocationImage))]
	[SerializeField] private ArticyRef[] _locations;

	private void Start()
	{
		if (PlayerPrefs.HasKey(CURRENT_LOCATION_INDEX))
		{
			ChangeLocation(PlayerPrefs.GetInt(CURRENT_LOCATION_INDEX));
		}
		else
		{
			ChangeLocation(0);
		}
	}

	public void ChangeLocation(int index)
	{
		IAsset asset = null;
		asset = _locations[index].GetObject<LocationImage>().PreviewImage.Asset;
		_backgroundImage.sprite = asset.LoadAssetAsSprite();
	}
}
