using UnityEngine;
using System.Collections;

/*	This controls the menu controls. Depending on what you put in the menuType, it does various things.
 */
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public string menuType;		//Where is this script located in?

	//Depending on what type the menu is, the menu either goes to the main game or the how to play
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Return))
		{
			if(menuType == "Title" || menuType == "GameOver" || menuType == "HowToPlay")
				SceneManager.LoadScene("Main_Game");
		}
		else if(Input.GetKeyUp(KeyCode.H))
		{
			if(menuType == "Title")
				SceneManager.LoadScene("How_To_Play");
		}
	}
}
