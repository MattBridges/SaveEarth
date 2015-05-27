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
        string[] str = this.name.Split('_');
        if (str[0] == "PlayerShip")
        {
            GameObject[] psNodes = GameObject.FindGameObjectsWithTag("SpawnNode");
            foreach(GameObject node in psNodes)
            {
                string[] nd = node.name.Split('_');
                if(nd[0] == "PlayerShip"&& node != this.gameObject)
                {
                    node.SetActive(false);
                    Debug.LogError("There was more than one player spawn node");
                }
            }
        }
    }
}
