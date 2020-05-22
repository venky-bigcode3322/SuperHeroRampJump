using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerPrefsWindow : EditorWindow
{
	
	//	public variables
	public string Key;
	public string Value;

	public DataType TypeDef = DataType.INTTEGER;
	
	private static PlayerPrefsWindow MyWindow;
	[MenuItem("BigCode/PlayerPrefs/SetKey %w")]
	public static void InitPlayerPrefsWindow ()
	{
		MyWindow = EditorWindow.GetWindow (typeof(PlayerPrefsWindow)) as PlayerPrefsWindow;
	}
	
	void OnGUI ()
	{
		GUILayout.Label ("Enter key here", EditorStyles.boldLabel);
		Key = EditorGUILayout.TextField ("Key : ", Key);
		GUILayout.Label ("Enter value here", EditorStyles.boldLabel);
		Value = EditorGUILayout.TextField ("Value : ", Value);
		GUILayout.Label ("Select data type here", EditorStyles.boldLabel);
		TypeDef = (DataType)EditorGUILayout.EnumPopup ("Type : ", TypeDef);
        if(GUILayout.Button("Save"))
        {
            SetPlayerPref();
        }
	}
	
	void SetPlayerPref ()
	{
		if (string.IsNullOrEmpty (Key)) {
			EditorUtility.DisplayDialog ("Warning!", "Key should not be empty", "Ok");
			return;
		}
		if (TypeDef != DataType.STRING) {
			if (string.IsNullOrEmpty (Value)) {
				EditorUtility.DisplayDialog ("Warning!", "Value should not be empty", "Ok");
			}
		}

		switch (TypeDef) {
		case DataType.INTTEGER:
			PlayerPrefs.SetInt (Key, int.Parse (Value));
			break;
		case DataType.FLOAT:
			PlayerPrefs.SetFloat (Key, float.Parse (Value));
			break;
		case DataType.STRING:
			PlayerPrefs.SetString (Key, Value);
			break;
		}
		Debug.Log ("Key : " + Key + " Value : " + Value);
		MyWindow.Close ();
	}

}

public enum DataType
{
	INTTEGER,
	FLOAT,
	STRING
}

