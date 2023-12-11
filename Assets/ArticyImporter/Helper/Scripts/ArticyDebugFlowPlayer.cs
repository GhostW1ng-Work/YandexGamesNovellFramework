using System.Collections.Generic;
using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Unity.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ArticyFlowPlayer))]
public class ArticyDebugFlowPlayer : MonoBehaviour, IArticyFlowPlayerCallbacks
{
	[Header("UI")]

	public GameObject branchPrefab;

	public Text displayNameLabel;

	public Text textLabel;

	public Text typeLabel;
	public Text technicalNameLabel;
	public Text idLabel;

	public RectTransform branchLayoutPanel;

	public Image previewImagePanel;

	[Header("Options")]
	public bool showFalseBranches = false;
	
	private ArticyFlowPlayer flowPlayer;

	void Start()
	{
		flowPlayer = GetComponent<ArticyFlowPlayer>();
		Debug.Assert(flowPlayer != null, "ArticyDebugFlowPlayer needs the ArticyFlowPlayer component!.");

		
		ClearAllBranches();


		if (flowPlayer != null && flowPlayer.StartOn == null)
			textLabel.text = "<color=green>No object selected in the flow player. Navigate to the ArticyflowPlayer and choose a StartOn node.</color>";
	}


	public void OnFlowPlayerPaused(IFlowObject aObject)
	{
		if(aObject != null)
		{
			typeLabel.text = aObject.GetType().Name;


			idLabel.text = string.Empty;
			technicalNameLabel.text = string.Empty;

			var articyObj = aObject as IArticyObject;
			if(articyObj != null)
			{
				idLabel.text = articyObj.Id.ToHex();
				technicalNameLabel.text = articyObj.TechnicalName;
			}
		}

		if (aObject is IObjectWithDisplayName objlWithDisplayName)
			displayNameLabel.text = objlWithDisplayName.DisplayName;
		else if (aObject is IObjectWithLocalizableDisplayName objWithLocalizableDisplayName)
			displayNameLabel.text = objWithLocalizableDisplayName.DisplayName;
		else
			displayNameLabel.text = string.Empty;

		if (aObject is IObjectWithText objWithText)
			textLabel.text = objWithText.Text;
		else if (aObject is IObjectWithLocalizableText objWithLocalizableText)
			textLabel.text = objWithLocalizableText.Text;
		else
			textLabel.text = string.Empty;

		ExtractCurrentPausePreviewImage(aObject);
	}

	public void OnBranchesUpdated(IList<Branch> aBranches)
	{
		ClearAllBranches();

		foreach (var branch in aBranches)
		{
			if (!branch.IsValid && !showFalseBranches) continue;

			var btn = Instantiate(branchPrefab);
			var rect = btn.GetComponent<RectTransform>();
			rect.SetParent(branchLayoutPanel, false);

			var branchBtn = btn.GetComponent<ArticyDebugBranch>();
			if(branchBtn == null)
				branchBtn = btn.AddComponent<ArticyDebugBranch>();

			branchBtn.AssignBranch(flowPlayer, branch);
		}
	}

	private void ClearAllBranches()
	{
		foreach (Transform child in branchLayoutPanel)
			Destroy(child.gameObject);
	}

	private void ExtractCurrentPausePreviewImage(IFlowObject aObject)
	{
		IAsset articyAsset = null;

		previewImagePanel.sprite = null;

		var dlgSpeaker = aObject as IObjectWithSpeaker;
		if (dlgSpeaker != null)
		{
			ArticyObject speaker = dlgSpeaker.Speaker;
			if (speaker != null)
			{	
				var speakerWithPreviewImage = speaker as IObjectWithPreviewImage;
				if (speakerWithPreviewImage != null)
				{
					articyAsset = speakerWithPreviewImage.PreviewImage.Asset;
				}
			}
		}

		if (articyAsset == null)
		{
			var objectWithPreviewImage = aObject as IObjectWithPreviewImage;
			if (objectWithPreviewImage != null)
			{
				articyAsset = objectWithPreviewImage.PreviewImage.Asset;
			}
		}

		if (articyAsset != null)
		{
			previewImagePanel.sprite = articyAsset.LoadAssetAsSprite();
		}
	}

	public void CopyTargetLabel(BaseEventData aData)
	{
		var pointerData = aData as PointerEventData;
		if (pointerData != null)
			GUIUtility.systemCopyBuffer = pointerData.pointerPress.GetComponent<Text>().text;
		Debug.LogFormat("Copied text \"{0}\" into clipboard!", GUIUtility.systemCopyBuffer);
	}
}
