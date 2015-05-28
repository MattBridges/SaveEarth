using UnityEngine;
using System.Collections;

public class HerculesController : AIController {

	public bool canWake;
	public int crewMembers;
	
	private int[] repairs;
	
	private Sprite[] sprites;
	private SpriteRenderer theSprite;
		
	// Use this for initialization
	void Awake () 
	{
		base.Start ();
		attack = false;		
		
		theSprite = this.gameObject.GetComponent<SpriteRenderer>();
		sprites = Resources.LoadAll<Sprite>("Ships/Hercules");
		repairs = new int[4];
	}
	
	private void checkParts(GameObject obj)
	{
		HerculesPart part = obj.GetComponent<HerculesPart>();
		
		if (part != null)
		{
			switch (part.partType)
			{
				case HerculesPart.PartTypes.Part_Top:
					repairs[0] = 1;
					break;
				case HerculesPart.PartTypes.Part_Bottom:
					repairs[1] = 1;
					break;
				case HerculesPart.PartTypes.Part_Front:
					repairs[2] = 1;
					break;
				case HerculesPart.PartTypes.Part_Back:
					repairs[3] = 1;
					break;
			}
			
			if (repairs[0] != 1 && repairs[1] == 1 && repairs[2] == 1 && repairs[3] == 1)
				theSprite.sprite = sprites[0];
			else if (repairs[0] == 1 && repairs[1] != 1 && repairs[2] == 1 && repairs[3] == 1)
				theSprite.sprite = sprites[1];
			else if (repairs[0] == 1 && repairs[1] == 1 && repairs[2] != 1 && repairs[3] == 1)
				theSprite.sprite = sprites[2];
			else if (repairs[0] == 1 && repairs[1] == 1 && repairs[2] == 1 && repairs[3] != 1)
				theSprite.sprite = sprites[3];
			else if (repairs[0] != 1 && repairs[1] == 1 && repairs[2] != 1 && repairs[3] == 1)
				theSprite.sprite = sprites[4];
			else if (repairs[0] != 1 && repairs[1] != 1 && repairs[2] != 1 && repairs[3] == 1)
				theSprite.sprite = sprites[5];
			else if (repairs[0] != 1 && repairs[1] == 1 && repairs[2] != 1 && repairs[3] != 1)
				theSprite.sprite = sprites[6];
			else if (repairs[0] == 1 && repairs[1] != 1 && repairs[2] != 1 && repairs[3] == 1)
				theSprite.sprite = sprites[7];
			else if (repairs[0] == 1 && repairs[1] != 1 && repairs[2] == 1 && repairs[3] != 1)
				theSprite.sprite = sprites[8];
			else if (repairs[0] != 1 && repairs[1] != 1 && repairs[2] == 1 && repairs[3] != 1)
				theSprite.sprite = sprites[9];
			else if (repairs[0] == 1 && repairs[1] == 1 && repairs[2] != 1 && repairs[3] != 1)
				theSprite.sprite = sprites[10];
			else if (repairs[0] != 1 && repairs[1] == 1 && repairs[2] == 1 && repairs[3] != 1)
				theSprite.sprite = sprites[11];
			else if (repairs[0] == 1 && repairs[1] != 1 && repairs[2] != 1 && repairs[3] != 1)
				theSprite.sprite = sprites[12];
			else if (repairs[0] != 1 && repairs[1] != 1 && repairs[2] == 1 && repairs[3] == 1)
				theSprite.sprite = sprites[13];
			else if (repairs[0] != 1 && repairs[1] != 1 && repairs[2] != 1 && repairs[3] != 1)
				theSprite.sprite = sprites[14];
			else
				theSprite.sprite = sprites[15];	
		}
	}
	
	public override void AIFollow()
	{
		base.AIFollow();
		
	}
	
	public override void AIRetreat()
	{
		base.AIRetreat();
	}
	
	public override void AIAttack()
	{
		
	}
	
	public override void AIStationary()
	{
		
	}
	
	public override void AIPatrol()
	{
		base.AIPatrol();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "EnemyBullet") {
			TakeDamage(5);
		}		
		
		if (other.gameObject.tag == "ShipPart")
		{
			checkParts(other.gameObject);
		}
	}
		
	public override void FixedUpdate ()
	{
		base.FixedUpdate();
	}
	
	void OnDisable()
	{
		health = maxHealth;
	}
}
