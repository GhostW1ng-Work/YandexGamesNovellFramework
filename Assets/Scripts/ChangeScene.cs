using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(SetScene);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(SetScene);
	}

	private void SetScene()
	{
		SceneManager.LoadScene(1);
	}
}
