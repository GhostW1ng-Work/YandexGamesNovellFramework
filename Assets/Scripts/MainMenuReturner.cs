using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuReturner : MonoBehaviour
{
	private CanvasGroup _canvasGroup;
	private void Start()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}
	public void ReturnToMainMenu()
	{
		EnablePanel();
		StartCoroutine(WaitBeforeReturn());
	}

	public void EnablePanel()
	{
		_canvasGroup.alpha = 1;
		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;
	}

	public void DisablePanel()
	{
		_canvasGroup.alpha = 0;
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
	}

	private IEnumerator WaitBeforeReturn()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(0);
	}
}
