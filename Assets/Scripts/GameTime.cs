using UnityEngine;
using System.Collections;

/*	This script is to keep track of the amount of time that passes in the game. Whenever activateEvent is called, this script calls on the GemSpawner and the
 * four EnemySpawner objects to increase their difficulty.
 */

public class GameTime : MonoBehaviour {

	public float eventTime;	//This is the time interval where activateEvent is called on.

	//Once the game starts, ActivateEvent will run every X seconds.
	void Start()
	{
		InvokeRepeating("ActivateEvent",eventTime,eventTime);
	}
		
	//This increases the difficulty of the game by adding in more enemies, increasing their speed, but also increasing the # of gems that spawn
	void ActivateEvent()
	{
		if(GameObject.Find("GemSpawner") == true)
		{
			GameObject.Find("GemSpawner").GetComponent<GemSpawner>().IncreaseSpawnRate();
			GameObject.Find("GemSpawner").GetComponent<GemSpawner>().ChangeGemLocationSprite();
		}
		EnemySpawner.IncreaseDifficulty();
	}

}
