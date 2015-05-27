﻿using UnityEngine;
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
        EventManager.LoadLvl();
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
    public void SpawnShips()
    {
        GameObject[] an = GetActiveNodes();
        foreach (GameObject node in an)
        {
            string[] ns = node.name.Split('_');
            string nodeName = ns[0];
       
            SpawnShip(nodeName, node);
            
        }
    }
    public void SpawnShip( string nodeName, GameObject Position)
    {
        GameObject shp = ObjectPooler.Instance.ReturnObject(nodeName);
        shp.SetActive(true);
        shp.transform.position = Position.transform.position;
    }

 
    #endregion

}
