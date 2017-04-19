using UnityEngine;
using System.Collections;

/* The player can move in 4 directions: up, down, left, and right. The player has a set speed. The player uses WASD to move the player and
* space to dig at the spot the player is on.
*/

public class PlayerMovement : MonoBehaviour {
	
	public Sprite defaultLook;								//The default sprite look for the player
	public Sprite digging;									//The sprite where the player is digging
	public AudioClip foundNothingSound = new AudioClip();	//The sound that plays when the player finds nothing.
	public AudioClip getGem = new AudioClip();				//The sound that plays when the player found a gem.

	public float moveSpeed = 0.08f;							//How fast the player can move
	public string moveDirec = "down";						//The current direction the player is moving in
	public float xWarp = 13.5f;								//The x disitance that the player warps when screen wrapping
	public float yWarp = 10f;								//The y disitance that the player warps when screen wrapping
	public bool isDigging;									//Is the player currently digging?

	// Controls the player movement
	void Update () 
	{
		if(isDigging == false)
		{
			Vector3 newPos;
			if(Input.GetKey(KeyCode.W))
			{
				newPos = new Vector2(0, moveSpeed);
				moveDirec = "up";
			}
			else if(Input.GetKey(KeyCode.A))
			{
				newPos = new Vector2(moveSpeed * -1, 0);
				moveDirec = "left";
			}
			else if(Input.GetKey(KeyCode.S))
			{
				newPos = new Vector2(0, moveSpeed * -1);
				moveDirec = "down";
			}
			else if(Input.GetKey(KeyCode.D))
			{
				newPos = new Vector2(moveSpeed, 0);
				moveDirec = "right";
			}
			else
			{
				newPos = new Vector2(0,0);
			}
			this.gameObject.transform.position += newPos;

			//This allows the player to dig at the spot they are currently on.
			if(Input.GetKeyUp(KeyCode.Space))
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = digging;
				newPos = new Vector2(0, 0);
				isDigging = true;
				StartCoroutine(DoneDigging());
			}
		}
	}

	//This is for when the player is on an active gem spot or when the player hits an enemy
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Gem")
		{
			if(other.gameObject.GetComponent<GemLocation>().hasGem == true)
			{
				GameObject.Find("Radar").GetComponent<Radar>().onGem = true;
				if(isDigging == true)
				{
					other.gameObject.GetComponent<GemLocation>().FoundGem(this.gameObject);
					GameObject.Find("Radar").GetComponent<Radar>().onGem = false;
					GameObject.Find("SFX").GetComponent<AudioSource>().PlayOneShot(getGem,0.3f);
				}
			}
			else
			{
				if(isDigging == true && GameObject.Find("SFX").GetComponent<AudioSource>().isPlaying == false)
					GameObject.Find("SFX").GetComponent<AudioSource>().PlayOneShot(foundNothingSound, 1f);
				
				GameObject.Find("Radar").GetComponent<Radar>().onGem = false;
			}
		}
		else if(other.gameObject.tag == "Enemy")
		{
			GameObject.Find("HealthGUI").GetComponent<HealthCounter>().TakeDamage();
		}
	}

	//This is called when the player reaches the endge of the screen.
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Wall")	//If the player hits the edge of the screen, they screen wrap.
		{
			Vector3 adjust;
			switch(moveDirec)
			{
			case "up":
				adjust = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - yWarp, this.gameObject.transform.position.z);
				break;
			case "down":
				adjust = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + yWarp, this.gameObject.transform.position.z);
				break;
			case "left":
				adjust = new Vector3(this.gameObject.transform.position.x + xWarp, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
				break;
			case "right":
				adjust = new Vector3(this.gameObject.transform.position.x - xWarp, this.gameObject.transform.position.y,this.gameObject.transform.position.z);
				break;
			default:
				adjust = new Vector3(0,0, this.gameObject.transform.position.z);
				break;
			}
			this.gameObject.transform.position = adjust;
		}
	}

	// Changes the sprite back to the moving sprite as well as allowing the player to move again.
	IEnumerator DoneDigging()
	{
		yield return new WaitForSeconds(0.3f);
		isDigging = false;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = defaultLook;
	}
}
