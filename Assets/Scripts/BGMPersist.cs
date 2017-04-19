using UnityEngine;
using System.Collections;

/* This script allows the game to hold different BGM music. Depending on the level, it also changes what music is currently playing.
 */
using UnityEngine.SceneManagement;

public class BGMPersist : MonoBehaviour {

	public AudioClip titleMusic = new AudioClip();
	public AudioClip mainMusic = new AudioClip();
	public AudioClip gameOverSound = new AudioClip();
	public float titleVolume = 0.21f;
	public float mainVolume = 1f;
	public float gameOverVolume = 0.8f;

	//Allows the BGM to persist, even when changing scenes.
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	//Whenever the level changes, so does the music.
	void OnLevelWasLoaded()
	{
		if(SceneManager.GetActiveScene().name == "Main_Game")
		{
			gameObject.GetComponent<AudioSource>().Stop();
			gameObject.GetComponent<AudioSource>().clip = mainMusic;
			gameObject.GetComponent<AudioSource>().volume = mainVolume;
			gameObject.GetComponent<AudioSource>().Play();
		}
		else if(SceneManager.GetActiveScene().name == "Game_Over")
		{
			gameObject.GetComponent<AudioSource>().Stop();
			gameObject.GetComponent<AudioSource>().PlayOneShot(gameOverSound, gameOverVolume);
		}
	}


}
