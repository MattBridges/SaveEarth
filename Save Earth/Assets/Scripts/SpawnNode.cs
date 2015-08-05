using UnityEngine;
using UnityEditor;
using System.Collections;
[ExecuteInEditMode]
public class SpawnNode : MonoBehaviour {
    public enum ShipType { 
        
        PlayerShip, AllyRaptor, Aphrodite, Battering, Bombardier, Bumble, Defender, Dreadnaught, 
        EnemyBaseShip, Gatherer, Hercules, Hive, Hunter, LightCarrier, Protector, Rammer, Raptor, 
        Replenisher, Trapper, Wasp, Wolverine};
    public enum SpawnType { Normal, Delayed };
    
    public ShipType shipType;
    public SpawnType spawnType;
    public float delayTime=0;
    public int PathNodeGroup = 0;
    public bool DestroyEndCond;

    
    public void OnEnable()
    {
       // UpName();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(!Application.isPlaying)
            UpName();
	}
    
    public void UpName()
    {
        if(spawnType == SpawnType.Delayed)
            this.name = "Delayed_" + shipType.ToString() + "_Spawn ";
        else
            this.name = shipType.ToString() + "_Spawn ";
    }
}
