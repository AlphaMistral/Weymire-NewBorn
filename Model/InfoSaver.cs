using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

//New version of InfoSaver uses DialogueLua as its ONLY data manager. And thus please make sure to Save Lua Environment at all necessary moments!
public class InfoSaver : MonoBehaviour 
{
	private static InfoSaver instance;

	//Awake function checks whether the static singleton exists. If not, create one!
	void Awake ()
	{
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyImmediate(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    public static void SavePlayerPosition (Vector3 playerPos)
    {
        DialogueLua.SetVariable(Consts.VariableName.playerPosX, playerPos.x);
        DialogueLua.SetVariable(Consts.VariableName.playerPosY, playerPos.y);
        DialogueLua.SetVariable(Consts.VariableName.playerPosZ, playerPos.z);
    }

    public static void SaveLuaEnvironment ()
    {
        string luaEnvironment = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString(Consts.VariableName.luaEnvironmentData, luaEnvironment);
    }

    public static string GetLuaEnvironment ()
    {
        return PlayerPrefs.GetString(Consts.VariableName.luaEnvironmentData);
    }

    //!!!!!!NOTICE: PLEASE REMEMBER TO USE COROUTINE IN THE PRESENTOR SECTOR!!!!!!. 
    public static void TakeScreenShot ()
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //For some unknown reason, compression of images is not supported on iOS platform.
        if (Application.platform == RuntimePlatform.Android)
            screenShot.Compress(true);
        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();
        GallerySaveNewImage(bytes);
    }

    public static IEnumerator GetGalleryTextureByID (UITexture targetTexture, int index)
    {
        if (index == 0)
        {
            targetTexture.mainTexture = Resources.Load(Consts.FileName.galleryBlankImage, typeof(Texture2D)) as Texture2D;
        }
        else
        {
            string path = "file://" + Application.persistentDataPath + "/Screenshot" + PlayerPrefs.GetInt (Consts.VariableName.galleryImageIndex + index) + ".png";
            WWW www = new WWW(path);
            yield return www;
            targetTexture.mainTexture = www.texture;
        }
    }

    public static void GallerySaveNewImage (byte[] bytes)
    {
        int index = PlayerPrefs.GetInt(Consts.VariableName.galleryImageNum) + 1;
		DialogueLua.SetVariable(Consts.VariableName.galleryImageNum, index);
        int head = PlayerPrefs.GetInt(Consts.VariableName.galleryImageHead) + 1;
        DialogueLua.SetVariable(Consts.VariableName.galleryImageHead, head);
        string fileName = Application.persistentDataPath + "/Screenshot" + head + ".png";
        System.IO.File.WriteAllBytes(fileName, bytes);
    }

    //Return a new Texture to display after deleting the image by its index. When calling this function in PRESENTOR please alter index first.
    public static IEnumerator GalleryDeleteImageByID (UITexture targetTexture, int index)
    {
        int totalImageNum = PlayerPrefs.GetInt(Consts.VariableName.galleryImageNum);
        int oldIndex = index;
        for (int i = index; i < totalImageNum; i++)
            DialogueLua.SetVariable(Consts.VariableName.galleryImageIndex + i, DialogueLua.GetVariable(Consts.VariableName.galleryImageIndex + (i + 1)).AsInt);
        if (oldIndex == totalImageNum)
        {
            yield return instance.StartCoroutine(GetGalleryTextureByID (targetTexture, index - 1));
            index--;
        }
        DialogueLua.SetVariable(Consts.VariableName.galleryImageNum, totalImageNum - 1);
        string deletePath = Application.persistentDataPath + "/Screenshot" + oldIndex + ".png";
        System.IO.File.Delete(deletePath);
        yield return instance.StartCoroutine(GetGalleryTextureByID(targetTexture, index));
    }

	/// <summary>
	/// Gets the string from resource. Indicate a line number and it will return the corresponding line. Otherwise all the mats instead. 
	/// </summary>
	/// <returns>The string from resource.</returns>
	/// <param name="fileName">File name.</param>
	/// <param name="lineNum">Line number.</param>
    public static string GetStringFromResource (string fileName, int lineNum = -1)
    {
        string texts = Resources.Load(fileName).ToString();
        if (lineNum == -1)
        {
			return texts;
        }
        string[] lines = texts.Split('\n');
		if (lineNum != -1)
			return lines[lineNum];
		else
			return texts;
    }

	/// <summary>
	/// Get the splitted strings based on the splitChar. A line Number is required in this case. 
	/// </summary>
	/// <returns>The strings from resouce.</returns>
	/// <param name="fileName">File name.</param>
	/// <param name="splitChar">Split char.</param>
	/// <param name="lineNum">Line number.</param>
	public static string[] GetStringsFromResouce (string fileName, char splitChar, int lineNum)
	{
		string texts = Resources.Load(fileName).ToString();
		string[] lines = texts.Split(splitChar);
		return lines;
	}

	public static void CreateNewSaveData ()
	{
		PlayerPrefsX.SetVector3(SaveVarName.PlayerPosition, Constant.PlayerPosition[0]);
		PlayerPrefsX.SetIntArray(SaveVarName.BackPackArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.ReminderArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.ConclusionArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.TruthArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.ClueArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.PermanentItemArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.UsableItemArray, null);
		PlayerPrefsX.SetIntArray(SaveVarName.ReminderItemArray, null);
	}
}
