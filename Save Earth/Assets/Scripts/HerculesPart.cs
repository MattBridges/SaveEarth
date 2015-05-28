﻿using UnityEngine;
using System.Collections;

public class HerculesPart : Collectible {

	public SpriteRenderer theSprite;
	public Sprite[] sprites;
	
	public enum PartTypes { Part_Top, Part_Bottom, Part_Front, Part_Back };
	public PartTypes partType;

	// Use this for initialization
	void Awake () 
	{
		theSprite = this.gameObject.GetComponent<SpriteRenderer>();
		sprites = Resources.LoadAll<Sprite>("Ships/HerculesParts");
		
		switch (partType)
		{
			case PartTypes.Part_Front:
				theSprite.sprite = sprites[0];
				break;
			case PartTypes.Part_Top:
				theSprite.sprite = sprites[1];
				break;
			case PartTypes.Part_Bottom:
				theSprite.sprite = sprites[2];
				break;
			case PartTypes.Part_Back:
				theSprite.sprite = sprites[3];
				break;
			default:
				break;
		}	
		
		Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
		this.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
	}
	
	public override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")		
		{
			if (type == CollectTypes.Towable)
			{
				PlayerShip plr = other.gameObject.GetComponent<PlayerShip>();
				
				if (plr.towingObject == null)
				{
					beingTowed = true;
					towedBy = other.gameObject;
					plr.towingObject = this;
				}
			}
		}
		
		if (other.tag == "Hercules")
		{
			if (towedBy)
			{
				PlayerShip plr = towedBy.GetComponent<PlayerShip>();
				
				plr.towingObject = null;
				towedBy = null;
				beingTowed = false;
				this.gameObject.SetActive(false);
			}
		}
	}
}
