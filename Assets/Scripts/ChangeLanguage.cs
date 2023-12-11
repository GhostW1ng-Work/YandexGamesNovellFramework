using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Agava.YandexGames;
using System.Collections;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;

	private Button _button;
	private bool _isEng = true;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(SetLanguage);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(SetLanguage);
	}

	private void SetLanguage()
	{
		_isEng = !_isEng;
		if (_isEng)
			ArticyDatabase.Localization.SetLanguage("en");
		else
			ArticyDatabase.Localization.SetLanguage("ru");
	}
}
