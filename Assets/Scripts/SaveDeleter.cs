using System;
using UnityEditor;
using UnityEngine;

public class SaveDeleter : MonoBehaviour
{

	[MenuItem("Tools/Delete")]
	static public void DeleteSaves()
	{
		PlayerPrefs.DeleteAll();
	}
}
