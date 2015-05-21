using UnityEngine;
using System.Collections;

public class SpawnNode : MonoBehaviour {
    public enum ShipType { Player, DragonFly, Raptor, MotherShip, Satellite, AllyRaptor, OrbitalRefinery, Gatherer, AllyOrbital, OrbitalBase, AllyCarrier, Carrier, BaseShip};
    public ShipType SpawnType;
    public bool DestroyEndCond;
    
    public void OnEnable()
    {
        this.name = SpawnType.ToString() + "Spawn";
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
