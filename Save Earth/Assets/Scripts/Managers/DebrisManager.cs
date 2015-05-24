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

	private List<GameObject> debrisList;
	private Dictionary<float, GameObject> activeDebris;
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
		debrisList = new List<GameObject>();
		activeDebris = new Dictionary<float, GameObject>();
	
		InitializeDebris();
	}
	
	private void InitializeDebris()
	{
		for (int i = 0; i < spawnCount; i++)
		{
			GameObject go = GameObject.Instantiate<GameObject>(debris) as GameObject;
			debrisList.Add(go);
			go.transform.SetParent(transform);
			go.SetActive(false);
		}	
	}
	
	private GameObject GetInactiveDebris()
	{
		foreach (GameObject go in debrisList)
		{
			if (!go.activeSelf)
				return go;
		}
		
		return GameObject.Instantiate<GameObject>(debris) as GameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (active)
		{
			if ((Time.time - lastTime) > timeDelay)
			{
				lastTime = Time.time;
				
				Vector3 loc = new Vector3(-50, Random.Range(lowRange, hiRange), 0);
				GameObject sendDebris = GetInactiveDebris();
				
				Destroy(sendDebris.GetComponent<PolygonCollider2D>());
				
				sendDebris.transform.position = loc;
				sendDebris.GetComponent<SpriteRenderer>().sprite = debrisSprites[Random.Range(0, debrisSprites.Length)];
				
				PolygonCollider2D pC = sendDebris.AddComponent<PolygonCollider2D>();
				pC.isTrigger = true;
				
				float scale = Random.Range(0.25f, 2.0f);
				int rot = Random.Range (0, 360);
				
				sendDebris.transform.localScale = new Vector3(scale, scale, scale);
				sendDebris.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
				
				sendDebris.SetActive(true);
				
				sendDebris.GetComponent<Rigidbody2D>().AddForce(new Vector3(debrisSpeed, 0, 0), ForceMode2D.Impulse);
				activeDebris.Add(Time.time, sendDebris);
			}
			
			foreach (KeyValuePair<float, GameObject> keyValue in activeDebris)
			{
				if ((Time.time - keyValue.Key) > lifetime)
				{
					keyValue.Value.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
					keyValue.Value.SetActive(false);
					activeDebris.Remove(keyValue.Key);
				}
			}
		}
	}
}
