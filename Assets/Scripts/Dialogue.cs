using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using Articy.Testproect;

[RequireComponent(typeof(ArticyFlowPlayer))]
public class Dialogue : MonoBehaviour, IArticyFlowPlayerCallbacks
{
	[SerializeField] private LocationChanger _locationChanger;
	[SerializeField] private RewardAdShower _rewardAdShower;
	[SerializeField] private ArticyDebugBranch _brunchTemplate;
	[SerializeField] private CanvasGroup _dialoguePanel;
	[SerializeField] private RectTransform _branchesLayout;
	[SerializeField] private TMP_Text _dialogueText;
	[SerializeField] private TMP_Text _dialogueSpeaker;

	private bool _isDialogActive = false;
	private ArticyFlowPlayer _flowPlayer;
	private Entity _currentSpeaker;

	public bool IsDialogActive => _isDialogActive;

	public bool IsInShadowState => throw new System.NotImplementedException();

	private void Start()
	{
		_flowPlayer = GetComponent<ArticyFlowPlayer>();
	}

	private void ChangePanelActivity(bool isActive)
	{
		_dialoguePanel.interactable = isActive;
		_dialoguePanel.blocksRaycasts = isActive;
		if (isActive)
			_dialoguePanel.alpha = 1;
		else
			_dialoguePanel.alpha = 0;
	}

	public void StartDialogue(string dialogueLine, string dialogueSpeaker)
	{
		_isDialogActive = true;
		ChangePanelActivity(_isDialogActive);

		_dialogueText.text = dialogueLine;
		_dialogueSpeaker.text = dialogueSpeaker;
	}

	public void CloseDialogue()
	{
		_isDialogActive = false;
		ChangePanelActivity(_isDialogActive);
	}

	public void OnFlowPlayerPaused(IFlowObject aObject)
	{
		if (aObject is IObjectWithSpeaker objectWithSpeaker)
		{
			if (objectWithSpeaker != null)
			{
				Entity speakerEntity = objectWithSpeaker.Speaker as Entity;
				if (speakerEntity != null)
				{
					_currentSpeaker = speakerEntity;
					_dialogueSpeaker.text = speakerEntity.DisplayName;
				}

				IObjectWithPreviewImage objectWithImage = speakerEntity;
				if (objectWithImage != null)
				{
					var objectWithTestCharacter = objectWithImage as IObjectWithFeatureTestCharacter;
					if (objectWithTestCharacter.GetFeatureTestCharacter().IsNarrator)
					{
						_locationChanger.ChangeLocation(ArticyDatabase.DefaultGlobalVariables.GetVariableByString<int>("Locations.LocationIndex"));
					}
				}
			}
		}
		else
			_dialogueSpeaker.text = string.Empty;



		if (aObject is IObjectWithText objectWithText)
			_dialogueText.text = objectWithText.Text;
		else if (aObject is IObjectWithLocalizableText objectWithLocalizableText)
			_dialogueText.text = objectWithLocalizableText.Text;
		else
			_dialogueText.text = string.Empty;



	}

	public void OnBranchesUpdated(IList<Branch> aBranches)
	{
		ClearAllBranches();

		foreach (Branch branch in aBranches)
		{
			if (!branch.IsValid) continue;

			var button = Instantiate(_brunchTemplate);
			RectTransform rectTransform = button.GetComponent<RectTransform>();
			rectTransform.SetParent(_branchesLayout, false);

			var branchButton = button.GetComponent<ArticyDebugBranch>();
			if (branchButton == null)
				branchButton = button.gameObject.AddComponent<ArticyDebugBranch>();
			branchButton.SetCurrentSpeaker(_currentSpeaker);
			branchButton.AssignBranch(_flowPlayer, branch);
		}
	}

	private void ClearAllBranches()
	{
		foreach (Transform child in _branchesLayout)
		{
			Destroy(child.gameObject);
		}
	}
}
