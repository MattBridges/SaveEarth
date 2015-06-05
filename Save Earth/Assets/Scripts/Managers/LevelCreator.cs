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
        SpawnShips();
        
        GameManager.Instance.currentMission = Mission;
        EventManager.ResetTargets();
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
            LoadNextZone();
            nextMission =1;

            Debug.Log(GameManager.Instance.currentZone +" "+ nextMission);
        }
        GameManager.Instance.currentMission.SetActive(false);
        LoadLevel(GetRandomMission(GameManager.Instance.currentZone, nextMission));
        GameManager.Instance.currentMissionNum = nextMission;
    }
    public void LoadNextZone()
    {
        
        if(GameManager.Instance.currentZone == "Earth")
        {
            GameManager.Instance.currentZone = "Mars";
            return;
        }

        if (GameManager.Instance.currentZone == "Mars")
        {
            GameManager.Instance.currentZone = "AsteroidBelt";
            return;
        }

        if (GameManager.Instance.currentZone == "AsteroidBelt")
        {
            GameManager.Instance.currentZone = "Jupiter";
            return;
        }

        if (GameManager.Instance.currentZone == "Jupiter")
        {
            GameManager.Instance.currentZone = "Saturn";
            return;
        }

        if (GameManager.Instance.currentZone == "Saturn")
        {
            GameManager.Instance.currentZone = "Uranus";
            return;
        }

        if (GameManager.Instance.currentZone == "Uranus")
        {
            GameManager.Instance.currentZone = "Pluto";
            return;
        }

        if (GameManager.Instance.currentZone == "Pluto")
        {
            GameManager.Instance.currentZone = "KuiperBelt";
            return;
        }

        if (GameManager.Instance.currentZone == "KuiperBelt")
        {
           //Put Game Complete Script here!

        }

    }

    public void ClearAllMissionNodes(GameObject mis)
    {
        
        if(GameManager.Instance.currentMission != null)
            GameManager.Instance.currentMission.SetActive(false);

        GameObject[] nodes = GetActiveNodes();
        foreach(GameObject node in nodes)
        {
            node.SetActive(false);
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
    public void SpawnShips()
    {
        GameObject[] an = GetActiveNodes();
        foreach (GameObject node in an)
        {
            SpawnNode nod = node.GetComponent<SpawnNode>();
            if(nod.spawnType == SpawnNode.SpawnType.Delayed)
            {
                StartCoroutine(DelaySpawn(nod.delayTime,node));
            }
            else
                SpawnShip(node);
        }
    }
    public void SpawnShip( GameObject node)
    {
        
        string[] ns = node.name.Split('_');
        string nodeName = null;
        if (node.GetComponent<SpawnNode>().spawnType == SpawnNode.SpawnType.Delayed)
            nodeName = ns[1];
        else
            nodeName = ns[0];
    
        if(nodeName == "PlayerShip")
        {
           
            PlayerShip player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
            player.RespawnPlayer();
            
        }

        else
        {
            GameObject shp = ObjectPooler.Instance.ReturnObject(nodeName);
            shp.GetComponent<AIController>().pathNodeGroup = node.GetComponent<SpawnNode>().PathNodeGroup;
            shp.transform.position = node.transform.position;
            shp.SetActive(true);
            
            if (node.GetComponent<SpawnNode>().DestroyEndCond)
            {
                EndObject eo = shp.GetComponent<EndObject>();
                eo.isEndObject = true;
                eo.UpdateEndObject();
            }
        }
    }
    IEnumerator DelaySpawn(float delay, GameObject node)
    {
        yield return new WaitForSeconds(delay);
        
        SpawnShip(node);
    }

 
    #endregion

}
