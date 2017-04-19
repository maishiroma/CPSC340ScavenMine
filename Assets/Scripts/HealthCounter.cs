using UnityEngine;
using System.Collections;

/*	This keeps track of the player's health as well as displaying it to the screen. 
 */
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HealthCounter : MonoBehaviour {

	public int currHealthIndex = 3;
	public bool inviniFrames = false;
	public int inviniFramesLength = 2;
	public Sprite[] healthGUI;
	public AudioClip hitSound = new AudioClip();

	//Constantly checks if the player has taken damage. If so, it updates the GUI for it. If it falls below 0, it calls the GameOver function.
	void Update () 
	{
		this.gameObject.GetComponent<SpriteRenderer>().sprite = healthGUI[currHealthIndex];
		if(currHealthIndex == 0)
			SceneManager.LoadScene("Game_Over");
	}

	//This is called in PlayerMovement when the player hits an enemy. They also get X numb of invinicible frames.
	public void TakeDamage()
	{
		if(inviniFrames == false)
		{
			currHealthIndex--;
			inviniFrames = true;
			GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
			GameObject.Find("SFX").GetComponent<AudioSource>().PlayOneShot(hitSound,0.3f);
			StartCoroutine(ResetInviniFrames());
		}
	}

	//Resets the inviniFrames back to normal.
	IEnumerator ResetInviniFrames()
	{
		yield return new WaitForSeconds(inviniFramesLength);
		inviniFrames = false;
		GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
	}
}
