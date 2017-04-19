using UnityEngine;
using System.Collections;

/*	This controls how gems are spawned in the game. Every 30 seconds, another gem is allowed to spawn. When the number hits 9, no more extra gems will
 * 	spawn. This also controls what the gem spots look like.
 */
using System.Collections.Generic;

public class GemSpawner : MonoBehaviour {

	public int numbOfGemsToSpawn = 3;			//How many gems that can be spawned
	public int numbOfGemsOnField = 0;			//How many gems that are currently on the field.
	public int maxGemsToSpawn = 10;				//The maximum number of gems that can be spawned at once
	public  GameObject[] gemLocations;			//Contains all of the possible locations for the gems.
	public Sprite[] groundSprites;				//Contains all of the looks for the gem spots.

	//Immediatly spawns out gems once the game starts
	void Start () 
	{
		Random.seed = System.Environment.TickCount;
		SpawnMoreGems();
	}
	
	// This allows for another gem to be spawned once 30 seconds pass.
	void Update () 
	{
		if(numbOfGemsOnField == 0)
			SpawnMoreGems();
	}

	// This function is called when there's no gems on the field. Depending on how many gems can be spawned, this function will randomly choose a location in its list
	// and allow one to spawn there.
	void SpawnMoreGems()
	{
		while(numbOfGemsOnField < numbOfGemsToSpawn)
		{
			int rand = Random.Range(0,gemLocations.Length);
			if(gemLocations[rand].GetComponent<GemLocation>().hasGem == false)
			{
				gemLocations[rand].GetComponent<GemLocation>().ActivateGemSpot();
				numbOfGemsOnField++;
			}
		}
	}

	//This simply changes what the ground looks like.
	public void ChangeGemLocationSprite()
	{
		int rand = Random.Range(0,groundSprites.Length);
		for(int i = 0; i < gemLocations.Length; i++)
			gemLocations[i].GetComponent<SpriteRenderer>().sprite = groundSprites[rand];
	}

	//This function increases the number of gems that can be spawned
	public void IncreaseSpawnRate()
	{
		if(numbOfGemsToSpawn < maxGemsToSpawn)
			numbOfGemsToSpawn++;
	}
}
