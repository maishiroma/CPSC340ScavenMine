using UnityEngine;
using System.Collections;

/*	This script handles keeping track of all of the gems that the player has gotten as well as displaying the GUI for it.
 */
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class GemCounter : MonoBehaviour {

	public static int gemCounter;						//How many gems did the player collect? This is made static in order to pass down info to the game over screen

	public GUIStyle fontForCounter = new GUIStyle();	//Used to give the font a better look
	public float xSize;									//The xPos of the font
	public float ySize;									//The yPos of the font

	private Vector3 posOfGUI;							//Used to dynamically shape the font's position on screens <= 1600x900 resolution
		
	//First, this function asjusts the GUI so it's centered around the camera
	void Start()
	{
		posOfGUI = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
	}

	//Updates the GUI as well as setting the dynamic behavior of the text.
	void OnGUI()
	{
		GUI.matrix = Matrix4x4.TRS( Vector3.zero, Quaternion.identity, new Vector3( Screen.width / 1600.0f, Screen.height / 900.0f, 1.0f ) );
		GUI.Label(new Rect(xSize,ySize,posOfGUI.x,posOfGUI.y), "= " + gemCounter,fontForCounter);
	}

	//This resets the score whenever the game is reloaded to the main game.
	void OnLevelWasLoaded()
	{
		if(SceneManager.GetActiveScene().name == "Main_Game")
			gemCounter = 0;
	}
}
