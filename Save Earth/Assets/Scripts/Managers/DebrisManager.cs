using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisManager : MonoBehaviour {

	public float lowRange;
	public float hiRange;
	public float timeDelay;
	public GameObject debris;
	public int spawnCount;
	public int debrisSpeed;
	public bool active;
	public float lifetime;
	
	[HideInInspector]
	public Sprite[] debrisSprites;

	private List<Debris> debrisList;
	private static DebrisManager _instance;
	private float lastTime;
	
	public static DebrisManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<DebrisManager>();
			}
			
			return _instance;
		}
	}

	// Use this for initialization
	void Start () 
	{
		debrisSprites = Resources.LoadAll<Sprite>(debris.name);		
		debrisList = new List<Debris>();
	
		InitializeDebris();
	}
	
	private void InitializeDebris()
	{
		for (int i = 0; i < spawnCount; i++)
		{
			GameObject go = GameObject.Instantiate<GameObject>(debris) as GameObject;
			
			debrisList.Add(go.GetComponent<Debris>());
			go.transform.SetParent(transform);
			go.SetActive(false);
		}	
	}
	
	private Debris GetInactiveDebris()
	{
		foreach (Debris go in debrisList)
		{
			if (!go.gameObject.activeSelf)
				return go;
		}
		
		GameObject deb = GameObject.Instantiate<GameObject>(debris) as GameObject;
		
		return deb.GetComponent<Debris>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (active)
		{
			if ((Time.time - lastTime) > timeDelay)
			{
				lastTime = Time.time;
				
				Debris sendDebris = GetInactiveDebris();
				sendDebris.RandomizeDebris(lowRange, hiRange, lifetime, debrisSpeed);
			}
		}
	}
}
