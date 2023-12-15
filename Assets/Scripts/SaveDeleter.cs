using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SaveDeleter : MonoBehaviour
{
	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(DeleteSave);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(DeleteSave);
	}

	[MenuItem("Tools/Delete")]
	static public void DeleteSaves()
	{
		PlayerPrefs.DeleteAll();
	}

	public void DeleteSave()
	{
		PlayerPrefs.DeleteAll();
	}
}
