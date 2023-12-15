using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Agava.YandexGames;
using System.Collections;
using TMPro;

[RequireComponent(typeof(Image), typeof(Button))]
public class ChangeLanguage : MonoBehaviour
{
	[SerializeField] private Sprite[] _languageImages;

	private Image _currentLanguageImage;
	private Button _button;

	private void Awake()
	{
		_currentLanguageImage = GetComponent<Image>();
		_button = GetComponent<Button>();
	}

	private void Start()
	{
		CheckLanguage();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(SetLanguage);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(SetLanguage);
	}

	public void CheckLanguage()
	{
		if (ArticyDatabase.Localization.Language == "en")
		{
			_currentLanguageImage.sprite = _languageImages[1];
		}
		else
		{
			_currentLanguageImage.sprite = _languageImages[0];
		}
	}

	private void SetLanguage()
	{
		if (ArticyDatabase.Localization.Language == "en")
		{
			ArticyDatabase.Localization.SetLanguage("ru");
			Lean.Localization.LeanLocalization.SetCurrentLanguageAll("ru");
			_currentLanguageImage.sprite = _languageImages[0];
		}
		else
		{
			ArticyDatabase.Localization.SetLanguage("en");
			Lean.Localization.LeanLocalization.SetCurrentLanguageAll("en");
			_currentLanguageImage.sprite = _languageImages[1];
		}
	}
}
