using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	private static EventManager _instance;
	
	public delegate void ResetTarget();
	public static event ResetTarget rT;

    public delegate void LoadLevel();
    public static event LoadLevel loadLvl;
    
    // For towing collectibles
    public delegate void towObject(PlayerShip pShip, HerculesPart part, bool towDrop);
	public static event towObject doTow;   
	
	public delegate void Cannons(GameObject orbBase);
	public static event Cannons findCannons;
	
	public delegate void sendCannon(Carrier carrier, List<OrbitalBaseCannon> attachment);
	public static event sendCannon sendCannonReference;

	public List<OrbitalBaseCannon> cannons;
	public List<OrbitalBaseCannon> theCannons;
	
	public delegate void collectObject(GameObject collector);
	public static event collectObject collectIt;
	
	public static EventManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<EventManager>();
			}
			return _instance;
		}
	}
	
	public static void ResetTargets()
	{
		if (rT != null)
			rT();
	}
	
    public static void LoadLvl()
    {
        if (loadLvl != null)
            loadLvl();
    }
    
    public static void checkTow(HerculesPart part, bool towDrop)
    {
    	if (doTow != null)
	    	doTow(PlayerShip.Instance, part, towDrop);
    }
    
	public void GetCannons(Carrier carrier, GameObject orbBase)
	{
		if (findCannons != null)
			findCannons(orbBase);
		
		if (sendCannonReference != null)
			sendCannonReference(carrier, cannons);			
	}
	
	public void takeObject(GameObject collector)
	{
		collectIt(collector);
	}
}
