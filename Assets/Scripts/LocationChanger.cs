using Articy.Testproect;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class LocationChanger : MonoBehaviour
{
	[SerializeField] private Image _backgroundImage;
	[ArticyTypeConstraint(typeof(ILocationImage))]
	[SerializeField] private ArticyRef[] _locations;

	[ArticyTypeConstraint(typeof(ILocationImage))]
	[SerializeField] private ArticyRef _image;

	private void Start()
	{
		ChangeLocation(0);
	}

	public void ChangeLocation(int index)
	{
		IAsset asset = null;
		asset = _locations[index].GetObject<LocationImage>().PreviewImage.Asset;
		_backgroundImage.sprite = asset.LoadAssetAsSprite();
	}
}
