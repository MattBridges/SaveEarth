using UnityEngine;
using System.Collections;

public class RegisterLevel : MonoBehaviour {
    public enum Level{ Earth, Mars};
    private LevelCreator lc;
    public Level curType = Level.Earth;
    public int mission;
    public int variant;

	// Use this for initialization
    public void Start()
    {
        lc = FindObjectOfType<LevelCreator>();
        AddLevel();
        this.gameObject.SetActive(false);
    }

    public void AddLevel()
    {        
        lc.AddLevel(curType.ToString(), mission, variant, this.gameObject);
    }

}
