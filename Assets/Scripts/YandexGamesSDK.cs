using Agava.YandexGames;
using Articy.Unity;
using System.Collections;
using UnityEngine;

public class YandexGamesSDK : MonoBehaviour
{
	[SerializeField] private ChangeLanguage _changeLanguage;
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#else
		// Always wait for it if invoking something immediately in the first scene.
		yield return YandexGamesSdk.Initialize();
		if (YandexGamesSdk.Environment.i18n.lang == "ru")
		{
			Lean.Localization.LeanLocalization.SetCurrentLanguageAll("ru");
			ArticyDatabase.Localization.SetLanguage("ru");
			_changeLanguage.CheckLanguage();
		}
		else if (YandexGamesSdk.Environment.i18n.lang == "en")
		{
			Lean.Localization.LeanLocalization.SetCurrentLanguageAll("en");
			ArticyDatabase.Localization.SetLanguage("en");
			_changeLanguage.CheckLanguage();
		}
#endif
	}
}
