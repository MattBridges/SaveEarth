using UnityEngine;
using System.Collections;

public class EnemySpawn_Rabid : MonoBehaviour {

	//public GameObject enemyShip;
	public GameObject dragonShip;
	public GameObject raptorShip;
	public GameObject motherShip;
	public GameObject spawnPortal;
	public int missionDifficulty;
	public Vector3 spawnValues;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public bool runTestWaves; // Toggle to activate testing.
	public bool testDragonfly; // Use only one ship type for testing.
	public bool testWingedRaptor; // USE ONLY ONE SHIP TYPE FOR TESTING.
	public bool testMothership; // Use only one ship type for testing.
	public int numberToTest; // Select number of selected ship type.
	Vector3 portalPosition;

	string enemy_Type;
	int enemy_Difficulty;
	int enemy_Health;
	int enemy_Damage;
	int enemy_Speed;
	string w_Type;
	string w_Range;
	int w_RateOfFire;
	int w_Cooldown;
	int projectile_Speed;

	int enemyOnes;
	int enemyTwos;
	int enemyThrees;
	int dragonDiff = 1;
	int raptorDiff = 5;
	int motherDiff = 10;
	int waveCount;
	int totalEnemies;
	int[] waveOne = new int[1000];
	int[] waveTwo = new int[1000];
	int[] waveThree = new int[1000];
	int[] waveFour = new int[1000];
	//bool waveKilled = false;
	//bool waveOneSpawned = false;
	//bool waveTwoSpawned = false;
	//bool waveThreeSpawned = false;
	//bool waveFourSpawned = false;

	// Use this for initialization
	void Start ()
	{
        
        if (!runTestWaves)
		{
			SelectEnemies();
			SelectWaves();
			StartCoroutine(SpawnEnemies ());
		}
		else
		{
			if (testDragonfly)
			{
				enemyOnes = numberToTest; enemyTwos = 0; enemyThrees = 0;
			}
			else if (testWingedRaptor)
			{
				enemyTwos = numberToTest; enemyOnes = 0; enemyThrees = 0;
			}
			else if (testMothership)
			{
				enemyThrees = numberToTest; enemyOnes = 0; enemyTwos = 0;
			}

			waveCount = 1;
			for (int e = 0; e < numberToTest; e++)
			{
				if (waveOne[e] == 0)
				{
					if (testDragonfly)
					{
						waveOne[e] = 1;
					}
					else if (testWingedRaptor)
					{
						waveOne[e] = 2;
					}
					else if (testMothership)
					{
						waveOne[e] = 3;
					}
				}
			}
			// Set default enemy values according to test variables
		
			StartCoroutine(SpawnEnemies ());
		}

	}
	
	void SelectEnemies()
	{
		// Initialize values
		int currentDifficulty = 0;
		enemyOnes = 0;
		enemyTwos = 0;
		enemyThrees = 0;

		// Use mission difficulty
		// Randomly select an enemy difficulty
		// Count up this difficulty until it reaches mission difficulty
		for (int e = 0; e < missionDifficulty; e++)
		{
			if (currentDifficulty < missionDifficulty)
			{
				int tempEnemy = Random.Range(0, 3);

				if (tempEnemy == 0)
				{
					if ((currentDifficulty + dragonDiff) <= missionDifficulty)
					{
						enemyOnes++;
						currentDifficulty = currentDifficulty + dragonDiff;
					}
				}
				else if (tempEnemy == 1)
				{
					if ((currentDifficulty + raptorDiff) <= missionDifficulty)
					{
						enemyTwos++;
						currentDifficulty = currentDifficulty + raptorDiff;
					}
					else
					{
						enemyOnes++;
						currentDifficulty = currentDifficulty + dragonDiff;
					}
				}
				else if (tempEnemy == 2)
				{
					if ((currentDifficulty + motherDiff) <= missionDifficulty)
					{
						enemyThrees++;
						currentDifficulty = currentDifficulty + motherDiff;
					}
					else if ((currentDifficulty + raptorDiff) <= missionDifficulty)
					{
						enemyTwos++;
						currentDifficulty = currentDifficulty + raptorDiff;
					}
					else
					{
						enemyOnes++;
						currentDifficulty = currentDifficulty + dragonDiff;
					}
				}
			}
		}
	}
	
	void SelectWaves()
	{
		// Find total number of enemies
		totalEnemies = enemyOnes + enemyTwos + enemyThrees;
		Debug.Log("Total Enemies " + totalEnemies);
		Debug.Log ("Dragonflies: " + enemyOnes);
		Debug.Log ("Winged Raptors: " + enemyTwos);
		Debug.Log ("Motherships: " + enemyThrees);

		// Setup number of waves to be created
		if (totalEnemies >= 50)
		{ waveCount = 4; }
		else if (totalEnemies >= 25)
		{ waveCount = 3; }
		else if (totalEnemies >= 10)
		{ waveCount = 2; }
		else if (totalEnemies < 10)
		{ waveCount = 1; }
		Debug.Log ("WaveCount: " + waveCount);

		// Use temporary values to manipulate for wave selection
		int tempOne = enemyOnes;
		int tempTwo = enemyTwos;
		int tempThree = enemyThrees;

		// Select enemies to put into each wave
		for (int e = 0; e < totalEnemies; e++)
		{
			// Place all Dragonflies into waves
			if (tempOne > 0)
			{
				// Pick a wave based on current wave count
				// Place a dragonfly into the wave
				// Decrement current tempOne count
				if (waveCount == 4)
				{
					int RandWave = Random.Range(0,4);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 1; tempOne--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 1; tempOne--; break;}
						}
					}
					else if (RandWave == 2)
					{
						for (int c = 0; c < waveThree.Length; c++)
						{
							if (waveThree[c] == 0)
							{ waveThree[c] = 1; tempOne--; break;}
						}
					}
					else if (RandWave == 3)
					{
						for (int d = 0; d < waveFour.Length; d++)
						{
							if (waveFour[d] == 0)
							{ waveFour[d] = 1; tempOne--; break;}
						}
					}
				}
				else if (waveCount == 3)
				{
					int RandWave = Random.Range(0,3);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 1; tempOne--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 1; tempOne--; break;}
						}
					}
					else if (RandWave == 2)
					{
						for (int c = 0; c < waveThree.Length; c++)
						{
							if (waveThree[c] == 0)
							{ waveThree[c] = 1; tempOne--; break;}
						}
					}
				}
				else if (waveCount == 2)
				{
					int RandWave = Random.Range(0,2);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 1; tempOne--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 1; tempOne--; break;}
						}
					}
				}
				else if (waveCount == 1)
				{
					for (int a = 0; a < waveOne.Length; a++)
					{
						if (waveOne[a] == 0)
						{ waveOne[a] = 1; tempOne--; break; }
					}
				}
			}

			// Place all Winged Raptors into waves
			if (tempTwo > 0)
			{
				// Pick a wave based on current wave count
				// Place a Winged Raptor into the wave
				// Decrement current tempTwo count
				if (waveCount == 4)
				{
					int RandWave = Random.Range(0,4);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 2; tempTwo--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 2; tempTwo--; break;}
						}
					}
					else if (RandWave == 2)
					{
						for (int c = 0; c < waveThree.Length; c++)
						{
							if (waveThree[c] == 0)
							{ waveThree[c] = 2; tempTwo--; break;}
						}
					}
					else if (RandWave == 3)
					{
						for (int d = 0; d < waveFour.Length; d++)
						{
							if (waveFour[d] == 0)
							{ waveFour[d] = 2; tempTwo--; break;}
						}
					}
				}
				else if (waveCount == 3)
				{
					int RandWave = Random.Range(0,3);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 2; tempTwo--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 2; tempTwo--; break;}
						}
					}
					else if (RandWave == 2)
					{
						for (int c = 0; c < waveThree.Length; c++)
						{
							if (waveThree[c] == 0)
							{ waveThree[c] = 2; tempTwo--; break;}
						}
					}
				}
				else if (waveCount == 2)
				{
					int RandWave = Random.Range(0,2);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 2; tempTwo--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 2; tempTwo--; break;}
						}
					}
				}
				else if (waveCount == 1)
				{
					for (int a = 0; a < waveOne.Length; a++)
					{
						if (waveOne[a] == 0)
						{ waveOne[a] = 2; tempTwo--; break; }
					}
				}
			}

			// Place all Motherships into waves
			if (tempThree > 0)
			{
				// Pick a wave based on current wave count
				// Place a Mothership into the wave
				// Decrement current tempThree count
				if (waveCount == 4)
				{
					int RandWave = Random.Range(0,4);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 3; tempThree--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 3; tempThree--; break;}
						}
					}
					else if (RandWave == 2)
					{
						for (int c = 0; c < waveThree.Length; c++)
						{
							if (waveThree[c] == 0)
							{ waveThree[c] = 3; tempThree--; break;}
						}
					}
					else if (RandWave == 3)
					{
						for (int d = 0; d < waveFour.Length; d++)
						{
							if (waveFour[d] == 0)
							{ waveFour[d] = 3; tempThree--; break;}
						}
					}
				}
				else if (waveCount == 3)
				{
					int RandWave = Random.Range(0,3);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 3; tempThree--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 3; tempThree--; break;}
						}
					}
					else if (RandWave == 2)
					{
						for (int c = 0; c < waveThree.Length; c++)
						{
							if (waveThree[c] == 0)
							{ waveThree[c] = 3; tempThree--; break;}
						}
					}
				}
				else if (waveCount == 2)
				{
					int RandWave = Random.Range(0,2);
					if (RandWave == 0)
					{
						for (int a = 0; a < waveOne.Length; a++)
						{
							if (waveOne[a] == 0)
							{ waveOne[a] = 3; tempThree--; break;}
						}
					}
					else if (RandWave == 1)
					{
						for (int b = 0; b < waveTwo.Length; b++)
						{
							if (waveTwo[b] == 0)
							{ waveTwo[b] = 3; tempThree--; break;}
						}
					}
				}
				else if (waveCount == 1)
				{
					for (int a = 0; a < waveOne.Length; a++)
					{
						if (waveOne[a] == 0)
						{ waveOne[a] = 3; tempThree--; break; }
					}
				}
			}
		}
	}
	
	IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			for (int w = 1; w <= waveCount; w++)
			{
				//Debug.Log ("W = " + w);
				if (w == 1)
				{
					// Add in portal spawning for wave1 here
					portalPosition = new Vector3(Random.Range (-10.0f, 10.0f), Random.Range (-10.0f, 10.0f), -8);
					Instantiate (spawnPortal, portalPosition, spawnPortal.transform.rotation);
					Debug.Log("Spawned Portal for Wave1");

					// Find usable enemies to spawn
					int tempWaveOneLength = 0;
					for (int a = 0; a < waveOne.Length; a++)
					{
						if (waveOne[a] == 0)
						{ tempWaveOneLength = a; Debug.Log("Length of Wave 1: " + tempWaveOneLength); break;}
					}

					for (int i = 0; i <= tempWaveOneLength; i++)
					{
						if (waveOne[i] == 0)
						{ break; }
						else if (waveOne[i] == 1)
						{ enemy_Type = "Dragonfly"; }
						else if (waveOne[i] == 2)
						{ enemy_Type = "Winged Raptor"; }
						else if (waveOne[i] == 3)
						{ enemy_Type = "Mothership"; }

						Vector3 spawnPosition = new Vector3 (portalPosition.x, portalPosition.y, spawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;
						
						// Customize the spawned enemy by the type desired
						switch (enemy_Type)
						{
						case "Dragonfly":
							enemy_Health = 1;
							enemy_Difficulty = dragonDiff;
							enemy_Damage = 1;
							enemy_Speed = 1;
							w_Type = "small weak projectile";
							w_Range = "short";
							w_RateOfFire = 5;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (dragonShip, spawnPosition, dragonShip.transform.rotation);
							Debug.Log("Wave1 Spawned: " + enemy_Type);
							break;
							
						case "Winged Raptor":
							enemy_Health = 3;
							enemy_Difficulty = raptorDiff;
							enemy_Damage = 5;
							enemy_Speed = 2;
							w_Type = "beam weapon";
							w_Range = "medium";
							w_RateOfFire = 3;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (raptorShip, spawnPosition, raptorShip.transform.rotation);
							Debug.Log("Wave1 Spawned: " + enemy_Type);
							break;
							
						case "Mothership":
							enemy_Health = 9;
							enemy_Difficulty = motherDiff;
							enemy_Damage = 10;
							enemy_Speed = 2;
							w_Type = "large powerful projectile";
							w_Range = "far";
							w_RateOfFire = 1;
							w_Cooldown = 4;
							projectile_Speed = 1;
							Instantiate (motherShip, spawnPosition, motherShip.transform.rotation);
							Debug.Log("Wave1 Spawned: " + enemy_Type);
							break;
							
						default:
							Debug.Log ("An unsupported enemy type was selected.");
							break;
						}
						yield return new WaitForSeconds(spawnWait);
					}
					Destroy(spawnPortal);
					yield return new WaitForSeconds(waveWait);
					if (waveCount == 1)
					{ Debug.Log("Stopping Spawn after Wave1."); break; }
				}
				else if (w == 2)
				{
					// Add in portal spawning for wave2 here
					portalPosition = new Vector3(Random.Range (-10.0f, 10.0f), Random.Range (-10.0f, 10.0f), -8);
					Instantiate (spawnPortal, portalPosition, spawnPortal.transform.rotation);
					Debug.Log("Spawned Portal for Wave2");

					// Find usable enemies to spawn
					int tempWaveTwoLength = 0;
					for (int b = 0; b < waveTwo.Length; b++)
					{
						if (waveTwo[b] == 0)
						{ tempWaveTwoLength = b; break; }
					}

					for (int i = 0; i <= tempWaveTwoLength; i++)
					{
						if (waveTwo[i] == 0)
						{ break; }
						else if (waveTwo[i] == 1)
						{ enemy_Type = "Dragonfly"; }
						else if (waveTwo[i] == 2)
						{ enemy_Type = "Winged Raptor"; }
						else if (waveTwo[i] == 3)
						{ enemy_Type = "Mothership"; }
						
						Vector3 spawnPosition = new Vector3 (portalPosition.x, portalPosition.y, spawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;
						
						// Customize the spawned enemy by the type desired
						switch (enemy_Type)
						{
						case "Dragonfly":
							enemy_Health = 1;
							enemy_Difficulty = dragonDiff;
							enemy_Damage = 1;
							enemy_Speed = 1;
							w_Type = "small weak projectile";
							w_Range = "short";
							w_RateOfFire = 5;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (dragonShip, spawnPosition, dragonShip.transform.rotation);
							Debug.Log("Wave2 Spawned: " + enemy_Type);
							break;
							
						case "Winged Raptor":
							enemy_Health = 3;
							enemy_Difficulty = raptorDiff;
							enemy_Damage = 5;
							enemy_Speed = 2;
							w_Type = "beam weapon";
							w_Range = "medium";
							w_RateOfFire = 3;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (raptorShip, spawnPosition, raptorShip.transform.rotation);
							Debug.Log("Wave2 Spawned: " + enemy_Type);
							break;
							
						case "Mothership":
							enemy_Health = 9;
							enemy_Difficulty = motherDiff;
							enemy_Damage = 10;
							enemy_Speed = 2;
							w_Type = "large powerful projectile";
							w_Range = "far";
							w_RateOfFire = 1;
							w_Cooldown = 4;
							projectile_Speed = 1;
							Instantiate (motherShip, spawnPosition, motherShip.transform.rotation);
							Debug.Log("Wave2 Spawned: " + enemy_Type);
							break;
							
						default:
							Debug.Log ("An unsupported enemy type was selected.");
							break;
						}
						yield return new WaitForSeconds(spawnWait);
					}
					Destroy(spawnPortal);
					Debug.Log ("Waiting after wave2 spawn.");
					yield return new WaitForSeconds(waveWait);
				}
				else if (w == 3)
				{
					// Add in portal spawning for wave3 here
					portalPosition = new Vector3(Random.Range (-10.0f, 10.0f), Random.Range (-10.0f, 10.0f), -8);
					Instantiate (spawnPortal, portalPosition, spawnPortal.transform.rotation);
					Debug.Log("Spawned Portal for Wave3");

					// Find usable enemies to spawn
					int tempWaveThreeLength = 0;
					for (int c = 0; c < waveThree.Length; c++)
					{
						if (waveThree[c] == 0)
						{ tempWaveThreeLength = c; break;}
					}

					for (int i = 0; i <= tempWaveThreeLength; i++)
					{
						if (waveThree[i] == 0)
						{ break; }
						else if (waveThree[i] == 1)
						{ enemy_Type = "Dragonfly"; }
						else if (waveThree[i] == 2)
						{ enemy_Type = "Winged Raptor"; }
						else if (waveThree[i] == 3)
						{ enemy_Type = "Mothership"; }
						
						Vector3 spawnPosition = new Vector3 (portalPosition.x, portalPosition.y, spawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;
						
						// Customize the spawned enemy by the type desired
						switch (enemy_Type)
						{
						case "Dragonfly":
							enemy_Health = 1;
							enemy_Difficulty = dragonDiff;
							enemy_Damage = 1;
							enemy_Speed = 1;
							w_Type = "small weak projectile";
							w_Range = "short";
							w_RateOfFire = 5;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (dragonShip, spawnPosition, dragonShip.transform.rotation);
							Debug.Log("Wave3 Spawned: " + enemy_Type);
							break;
							
						case "Winged Raptor":
							enemy_Health = 3;
							enemy_Difficulty = raptorDiff;
							enemy_Damage = 5;
							enemy_Speed = 2;
							w_Type = "beam weapon";
							w_Range = "medium";
							w_RateOfFire = 3;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (raptorShip, spawnPosition, raptorShip.transform.rotation);
							Debug.Log("Wave3 Spawned: " + enemy_Type);
							break;
							
						case "Mothership":
							enemy_Health = 9;
							enemy_Difficulty = motherDiff;
							enemy_Damage = 10;
							enemy_Speed = 2;
							w_Type = "large powerful projectile";
							w_Range = "far";
							w_RateOfFire = 1;
							w_Cooldown = 4;
							projectile_Speed = 1;
							Instantiate (motherShip, spawnPosition, motherShip.transform.rotation);
							Debug.Log("Wave3 Spawned: " + enemy_Type);
							break;
							
						default:
							Debug.Log ("An unsupported enemy type was selected.");
							break;
						}
						yield return new WaitForSeconds(spawnWait);
					}
					Destroy(spawnPortal);
					Debug.Log ("Waiting after wave3 spawn.");
					yield return new WaitForSeconds(waveWait);
				}
				else if (w == 4)
				{
					// Add in portal spawning for wave4 here
					portalPosition = new Vector3(Random.Range (-10.0f, 10.0f), Random.Range (-10.0f, 10.0f), -8);
					Instantiate (spawnPortal, portalPosition, spawnPortal.transform.rotation);
					Debug.Log("Spawned Portal for Wave4");

					// Find usable enemies to spawn
					int tempWaveFourLength = 0;
					for (int d = 0; d < waveFour.Length; d++)
					{
						if (waveFour[d] == 0)
						{ tempWaveFourLength = d; break;}
					}

					for (int i = 0; i <= tempWaveFourLength; i++)
					{
						if (waveFour[i] == 0)
						{ break; }
						else if (waveFour[i] == 1)
						{ enemy_Type = "Dragonfly"; }
						else if (waveFour[i] == 2)
						{ enemy_Type = "Winged Raptor"; }
						else if (waveFour[i] == 3)
						{ enemy_Type = "Mothership"; }
						
						Vector3 spawnPosition = new Vector3 (portalPosition.x, portalPosition.y, spawnValues.z);
						Quaternion spawnRotation = Quaternion.identity;
						
						// Customize the spawned enemy by the type desired
						switch (enemy_Type)
						{
						case "Dragonfly":
							enemy_Health = 1;
							enemy_Difficulty = dragonDiff;
							enemy_Damage = 1;
							enemy_Speed = 1;
							w_Type = "small weak projectile";
							w_Range = "short";
							w_RateOfFire = 5;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (dragonShip, spawnPosition, dragonShip.transform.rotation);
							Debug.Log("Wave4 Spawned: " + enemy_Type);
							break;
							
						case "Winged Raptor":
							enemy_Health = 3;
							enemy_Difficulty = raptorDiff;
							enemy_Damage = 5;
							enemy_Speed = 2;
							w_Type = "beam weapon";
							w_Range = "medium";
							w_RateOfFire = 3;
							w_Cooldown = 3;
							projectile_Speed = 2;
							Instantiate (raptorShip, spawnPosition, raptorShip.transform.rotation);
							Debug.Log("Wave4 Spawned: " + enemy_Type);
							break;
							
						case "Mothership":
							enemy_Health = 9;
							enemy_Difficulty = motherDiff;
							enemy_Damage = 10;
							enemy_Speed = 2;
							w_Type = "large powerful projectile";
							w_Range = "far";
							w_RateOfFire = 1;
							w_Cooldown = 4;
							projectile_Speed = 1;
							Instantiate (motherShip, spawnPosition, motherShip.transform.rotation);
							Debug.Log("Wave4 Spawned: " + enemy_Type);
							break;
							
						default:
							Debug.Log ("An unsupported enemy type was selected.");
							break;
						}
						yield return new WaitForSeconds(spawnWait);
					}
					Destroy(spawnPortal);
					Debug.Log ("Waiting after wave4 spawn.");
					yield return new WaitForSeconds(waveWait);
				}
			}
			Debug.Log("Completed spawning all waves.");
			break;
		}
	}

}
