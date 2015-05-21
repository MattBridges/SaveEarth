using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelCreator : MonoBehaviour {
    #region Singlton Block
    private static LevelCreator _instance;
    public static LevelCreator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelCreator>();
            }
            return _instance;
        }
    }
    #endregion 

    #region Variables
    private Dictionary<string, Dictionary<int, Dictionary<int, GameObject>>> levelData;
    #endregion

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
        ClearAllMissionNodes(GameManager.Instance.currentMission);
        ClearAllEnemyShips();
        UIManager.Instance.UpdatePlayerHealthText();
        UIManager.Instance.UpdatePlayerLivesText();
        Mission.SetActive(true);
        SpawnEnemies();
        EventManager.LoadLevel();
        GameManager.Instance.currentMission = Mission;        
        Debug.Log("Loaded Level: " + Mission.name);
    }
    public void LoadRandomMission(string curLevel, int mission)
    {
        LoadLevel(GetRandomMission(curLevel, mission));
    }
    public void LoadNextMission()
    {
        int nextMission = GameManager.Instance.currentMissionNum + 1;
        if(nextMission>5)
        {
            nextMission =5;
        }
        LoadLevel(GetRandomMission(GameManager.Instance.currentZone, nextMission));
        GameManager.Instance.currentMissionNum = nextMission;
    }
    public void ClearAllMissionNodes(GameObject mis)
    {
        if(mis!=null)
        {
            mis.SetActive(false);
        }        
    }
    public void ClearAllEnemyShips()
    {
      
        GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
        foreach(GameObject obj in objs)
        {
            if (obj.tag == "Enemy" || obj.tag == "Mine" || obj.tag == "Satellite" || obj.tag == "Station" || obj.tag == "Collectable")
            {
                obj.SetActive(false);
            }
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
            if (nodeName == "PlayerShipSpawn" || nodeName == "PlayerSpawn")
                SpawnPlayerShip();
        }
    }
    public void SpawnDragonfly(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.dragonFlies, PoolingManager.Instance.dfShip, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnRaptor(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.raptors, PoolingManager.Instance.rpShip, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnMotherShip(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.motherShips, PoolingManager.Instance.mShip, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnMines(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.mines, PoolingManager.Instance.mine, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnSatellite(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.satellites, PoolingManager.Instance.satellite, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnAllyRaptor(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.allyCarriers, PoolingManager.Instance.allyCarrier, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnOrbitalRefinery(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.orbitalRefinerys, PoolingManager.Instance.orbitalRefinery, PoolingManager.Instance.shipCollector);
        if (Position.GetComponent<SpawnNode>().DestroyEndCond)
            ship.GetComponent<OrbitalRefinery>().endCondidtionObject = true;
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnGatherer(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.gatherers, PoolingManager.Instance.gatherer, PoolingManager.Instance.shipCollector);
        if (Position.GetComponent<SpawnNode>().DestroyEndCond)
            ship.GetComponent<Gatherer>().endCondidtionObject = true;
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnAllyOrbital(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.allyOrbitalBases, PoolingManager.Instance.allyOrbitalBase, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnOrbitalBase(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.orbitalBases, PoolingManager.Instance.orbitalBase, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnAllyCarrier(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.allyCarriers, PoolingManager.Instance.allyCarrier, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnCarrier(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.carriers, PoolingManager.Instance.carrier, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnBaseShip(GameObject Position)
    {
        GameObject ship = ObjectPooler.Instance.ReturnObject(PoolingManager.Instance.baseShips, PoolingManager.Instance.baseShip, PoolingManager.Instance.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnPlayerShip()
    {
        PlayerShip ship = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        ship.SpawnPlayer();
        
    }
    #endregion

}
