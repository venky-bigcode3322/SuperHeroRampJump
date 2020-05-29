using UnityEngine;
using UnityEditor;
using System.IO;

public class PlayerPrefsDelete : EditorWindow
{

	[MenuItem ("BigCode/PlayerPrefs/Delete %q")]
	public static void DeletePrefs ()
	{
        if (EditorUtility.DisplayDialog("BigCode", "Are you sure? Do you wanna delete playerprefs", "Yes","No"))
        {
            File.Delete(GameConstants_BigCode.LocalGameDataPath);

            PlayerPrefs.DeleteAll();

            EditorUtility.DisplayDialog("BigCode", "PlayerPrefs deleted successfully", "Ok");
        }
	}

}
