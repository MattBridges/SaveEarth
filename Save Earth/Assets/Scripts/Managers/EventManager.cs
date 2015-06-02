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
	public List<GameObject> theStations;
	public List<OrbitalBase> theOrbitals;
	
	public delegate void collectObject(GameObject collector, GameObject item);
	public static event collectObject collectIt;
	
	public delegate void dropObject(GameObject collector);
	public static event dropObject dropIt;

	public delegate void stations(string type);
	public static event stations searchStations;

	public delegate void orbitals(string type);
	public static event orbitals searchOrbitals;
				
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
	
	public void takeObject(GameObject collector, GameObject item)
	{
		if (collectIt != null)
			collectIt(collector, item);
	}
	
	public void leaveObject(GameObject collector)
	{
		if (dropIt != null)
			dropIt(collector);
	}
	
	public List<GameObject> findStation(string type)
	{
		if (searchStations != null)
			searchStations(type);
		
		return theStations;
	}
	
	public List<OrbitalBase> findOrbitals(string type)
	{
		if (searchOrbitals != null)
			searchOrbitals(type);
		
		return theOrbitals;
	}
}
