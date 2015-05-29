using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	private static EventManager _instance;
	
	public delegate void ResetTarget();
	public static event ResetTarget rT;

    public delegate void LoadLevel();
    public static event LoadLevel loadLvl;
    
    // For towing collectibles
    public delegate void towObject(PlayerShip pShip, HerculesPart part, bool towDrop);
	public static event towObject doTow;    

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
    	doTow(PlayerShip.Instance, part, towDrop);
    }
}
