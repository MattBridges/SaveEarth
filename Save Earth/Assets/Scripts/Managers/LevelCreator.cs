using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelCreator : MonoBehaviour {
    private Dictionary<string, Dictionary<int, Dictionary<int, GameObject>>> levelData;
    private ObjectPooler op;
    private PoolingManager pm;
    private GameObject lastMission;
    private GameManager gameManager;

	// Use this for initialization
    public void Start()
    {
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        pm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }

    #region Level Spawn Methods
    public void AddLevel(string level, int mission, int variant, GameObject LevelObject)
    {
        InitLevelDictionary();
        if (levelData.ContainsKey(level) == false)
            levelData[level] = new Dictionary<int, Dictionary<int, GameObject>>();
        if (levelData[level].ContainsKey(mission) == false)
            levelData[level][mission] = new Dictionary<int, GameObject>();

        levelData[level][mission][variant] = LevelObject;
    }
    public int GetMissionCount(string level, int mission)
    {
        InitLevelDictionary();
        if (levelData.ContainsKey(level) == false)
            return 0;
        if (levelData[level].ContainsKey(mission) == false)
            return 0;

        return levelData[level][mission].Count;
    }
    public GameObject GetRandomMission(string level, int mission)
    {
        InitLevelDictionary();
        if (levelData.ContainsKey(level) == false)
            return null;
        if (levelData[level].ContainsKey(mission) == false)
            return null;

        int varient = Random.Range(1, GetMissionCount(level, mission)+1);
        return levelData[level][mission][varient];
    }
    void InitLevelDictionary()
    {
        if (levelData != null)
            return;

        levelData = new Dictionary<string, Dictionary<int, Dictionary<int, GameObject>>>();
    }
    public void LoadLevel(GameObject Mission)
    {
        ClearAllMissionNodes(lastMission);
        Mission.SetActive(true);
        SpawnEnemies();
        lastMission = Mission;
        gameManager.currentMission = Mission;
        Debug.Log("Loaded Level: " + Mission.name);
    }
    public void LoadRandomMission(string curLevel, int mission)
    {
        LoadLevel(GetRandomMission(curLevel, mission));
    }

    public void ClearAllMissionNodes(GameObject mis)
    {
        if(mis!=null)
        {
            mis.SetActive(false);
        }        
    }
    public GameObject[] GetActiveNodes()
    {
        GameObject[] activeNodes = GameObject.FindGameObjectsWithTag("SpawnNode");
        return activeNodes;
    }
    #endregion
    #region Object Spawning Methods
    public void SpawnEnemies()
    {
        GameObject[] an = GetActiveNodes();
        foreach (GameObject node in an)
        {
            string[] ns = node.name.Split(' ');
            string nodeName = ns[0];

            if (nodeName == "DragonFlySpawn")
                SpawnDragonfly(node);
            if (nodeName == "RaptorSpawn")
                SpawnRaptor(node);
            if (nodeName == "MotherShipSpawn")
                SpawnMotherShip(node);
            if (nodeName == "MineSpawn")
                SpawnMines(node);
            if (nodeName == "SatelliteSpawn")
                SpawnSatellite(node);
            if (nodeName == "AllyRaptorSpawn")
                SpawnAllyRaptor(node);
            if (nodeName == "OrbitalRefinerySpawn")
                SpawnOrbitalRefinery(node);
            if (nodeName == "GathererSpawn")
                SpawnGatherer(node);
            if (nodeName == "AllyOrbitalSpawn")
                SpawnAllyOrbital(node);
            if (nodeName == "OrbitalBaseSpawn")
                SpawnOrbitalBase(node);
            if (nodeName == "AllyCarrierSpawn")
                SpawnAllyCarrier(node);
            if (nodeName == "CarrierSpawn")
                SpawnCarrier(node);
            if (nodeName == "BaseShipSpawn")
                SpawnBaseShip(node);
        }
    }
    public void SpawnDragonfly(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.dragonFlies, pm.dfShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnRaptor(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.raptors, pm.rpShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnMotherShip(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.motherShips, pm.mShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnMines(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.mines , pm.mine, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnSatellite(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.satellites, pm.satellite, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnAllyRaptor(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.allyCarriers, pm.allyCarrier, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnOrbitalRefinery(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.orbitalRefinerys, pm.orbitalRefinery, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnGatherer(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.gatherers, pm.gatherer, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnAllyOrbital(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.allyOrbitalBases, pm.allyOrbitalBase, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnOrbitalBase(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.orbitalBases, pm.orbitalBase, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnAllyCarrier(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.allyCarriers, pm.allyCarrier, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnCarrier(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.carriers, pm.carrier, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnBaseShip(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.baseShips, pm.baseShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    #endregion
}
