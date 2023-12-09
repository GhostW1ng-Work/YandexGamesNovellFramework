using Articy.Testproect;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ArticyDebugBranch : MonoBehaviour
{
	private Text dialogText;

	private Branch branch;

	private ArticyFlowPlayer processor;
	private Entity _speaker;


	public void AssignBranch(ArticyFlowPlayer aProcessor, Branch aBranch)
	{

		GetComponentInChildren<Button>().onClick.AddListener(OnBranchSelected);
		dialogText = GetComponentInChildren<Text>();

		branch = aBranch;
		processor = aProcessor;

	
		dialogText.color = aBranch.IsValid ? Color.black : Color.red;

		var target = aBranch.Target;
		dialogText.text = "";

		var obj = target as IObjectWithMenuText;

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
		processor.Play(branch);
	}

	public void SetCurrentSpeaker(Entity speaker)
	{
		_speaker = speaker;
	}
}
