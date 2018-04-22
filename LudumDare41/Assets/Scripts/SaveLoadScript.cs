using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadScript {

	public static GameManagerScript saveGame = new GameManagerScript();

	public static void Save()
	{
		saveGame = GameManagerScript.current;
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/saveGame.gd");
		bf.Serialize (file, SaveLoadScript.saveGame);
		file.Close ();
	}

	public static void Load() 
	{
		if(File.Exists(Application.persistentDataPath + "/saveGame.gd"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/saveGame.gd", FileMode.Open);
			SaveLoadScript.saveGame = (GameManagerScript)bf.Deserialize (file);
			file.Close ();
		}
	}

}
