using Agava.YandexGames;
using Articy.Unity;
using System.Collections;
using UnityEngine;

public class YandexGamesSDK : MonoBehaviour
{
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#else
		// Always wait for it if invoking something immediately in the first scene.
		yield return YandexGamesSdk.Initialize();
		if (YandexGamesSdk.Environment.i18n.lang == "ru")
		{
			ArticyDatabase.Localization.SetLanguage("ru");
		}
		else if (YandexGamesSdk.Environment.i18n.lang == "en")
		{
			ArticyDatabase.Localization.SetLanguage("en");
		}
#endif
	}
}
