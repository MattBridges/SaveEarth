using UnityEngine;
using UnityEditor;
using System.Collections;

public class SpawnNode : MonoBehaviour {
    public enum ShipType { PlayerShip, Dragonfly, Raptor, MotherShip, Satellite, AllyRaptor, OrbitalRefinery, Gatherer, AllyOrbitalBase, OrbitalBase, AllyCarrier, Carrier, EnemyBaseShip, EnemyMine};
    public ShipType SpawnType;
    public int PathNodeGroup = 0;
    public bool DestroyEndCond;

    
    public void OnEnable()
    {
        UpName();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void UpName()
    {
        this.name = SpawnType.ToString() + "_Spawn ";
    }
}
