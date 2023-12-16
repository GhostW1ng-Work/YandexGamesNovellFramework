using Articy.Testproect;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;
using System.Collections;

public class ArticyDebugBranch : MonoBehaviour
{
	[SerializeField] private Image _buttonImage;
	[SerializeField] private Image _adImage;
	[SerializeField] private Color _adImageColor;

	private TMP_Text dialogText;

	private Branch branch;

	private ArticyFlowPlayer processor;
	private Entity _speaker;
	private bool _isRewarded;

	private IEnumerator Start()
	{
#if !UNITY_WEBGL || UNITY_EDITOR
		yield break;
#endif

		// Always wait for it if invoking something immediately in the first scene.
		yield return YandexGamesSdk.Initialize();
	}

	public void AssignBranch(ArticyFlowPlayer aProcessor, Branch aBranch)
	{
		GetComponentInChildren<Button>().onClick.AddListener(OnBranchSelected);
		dialogText = GetComponentInChildren<TMP_Text>();

		branch = aBranch;
		processor = aProcessor;

		dialogText.color = aBranch.IsValid ? Color.black : Color.red;

		var target = aBranch.Target;
		dialogText.text = "";

		var obj = target as IObjectWithMenuText;
		var objectWithRewardDialogue = target as IObjectWithFeatureRewardDialogue;

		if (objectWithRewardDialogue != null)
		{
			EnableRewardVisual();
		}
		ArticyObject player = ArticyDatabase.GetObject(_speaker.TechnicalName);
		var characterFeature = player as IObjectWithFeatureTestCharacter;

		if (obj != null)
		{
			dialogText.text = obj.MenuText;
			if (characterFeature.GetFeatureTestCharacter().IsPlayer)
			{
				dialogText.text = ">>>";
			}

			if (dialogText.text == "")
			{
				if (obj is IObjectWithText objectWithText)
					dialogText.text = objectWithText.Text;
				else if (obj is IObjectWithLocalizableText objectWithLocalizableText)
					dialogText.text = objectWithLocalizableText.Text;
				else
					dialogText.text = "...";
			}
		}
	}

	public void OnBranchSelected()
	{
		var target = branch.Target;
		var objectWithRewardDialogue = target as IObjectWithFeatureRewardDialogue;

		if (objectWithRewardDialogue != null)
		{
			_isRewarded = false;
#if UNITY_EDITOR || !UNITY_WEBGL
			print("показать видеорекламу");
#else
			VideoAd.Show(onRewardedCallback: OnRewarded, onCloseCallback:PlayNextLine);
#endif
		}
		else
		{
			processor.Play(branch);
		}
	}

	private void OnRewarded()
	{
		_isRewarded = true;
	}


	private void PlayNextLine()
	{
		if(_isRewarded)
			processor.Play(branch);
	}

	public void SetCurrentSpeaker(Entity speaker)
	{
		_speaker = speaker;
	}

	public void EnableRewardVisual()
	{
		_adImage.enabled = true;
		_buttonImage.color = _adImageColor;
	}
}
