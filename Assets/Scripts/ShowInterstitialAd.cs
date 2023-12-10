using UnityEngine;
using Agava.YandexGames;

public class ShowInterstitialAd : MonoBehaviour
{
	[SerializeField] private float _maxTime;
	[SerializeField] private float _minTime;

	private float _currentTimeAfterAd;

	private void Start()
	{
		_currentTimeAfterAd = Random.Range(_minTime, _maxTime);
	}

	private void Update()
	{
		if (_currentTimeAfterAd > 0)
		{
			_currentTimeAfterAd -= Time.deltaTime;
		}
		else
		{
#if UNITY_EDITOR || !UNITY_WEBGL
			print("Показать рекламу");
			_currentTimeAfterAd = Random.Range(_minTime, _maxTime);
#else
			ShowInterstitial();
			_currentTimeAfterAd = Random.Range(_minTime, _maxTime);
#endif
		}
	}

	private void ShowInterstitial()
	{
		InterstitialAd.Show();
	}
}
