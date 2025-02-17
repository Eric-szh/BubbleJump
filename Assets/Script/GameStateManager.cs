using System;
using System.Collections.Generic;
using UnityEngine;
// all because you fogot to save the state
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public InvCtrl invCtrl;
    public List<GameObject> pickups = new List<GameObject>();
    public List<bool> pickupGained = new List<bool>();
    public List<bool> pickupGainedStored = new List<bool>();

    public List<GameObject> buttons = new List<GameObject>();
    public List<bool> buttonPressed = new List<bool>();
    public List<bool> buttonPressedStored = new List<bool>();

    public List<GameObject> doors = new List<GameObject>();
    public List<bool> doorLocked = new List<bool>();
    public List<bool> doorLockedStored = new List<bool>();

    public List<bool> powerUpUnlocked = new List<bool>();
    public List<bool> powerUpUnlockedStored = new List<bool>();

    public List<GameObject> paperWall;
    public List<bool> paperWallDestroyed;
    public List<bool> paperWallDestroyedStored;

    public int inventory = -1;
    public Sprite inventorySprite;
    private int inventoryIndex = -1;
    public int inventoryStored = -1;
    public Sprite inventoryStoredSprite;
    public CharacterSkillControler skillCtrl;

    public List<GameObject> enemies = new List<GameObject>();
    public List<Transform> enemySpawnPoints = new List<Transform>();
    private bool resetEnemy = false;

    public event Action DeathEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        Instance = this; // Assign the instance
        DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
    }

    public int RegisterPickup(GameObject pickup)
    {
        pickups.Add(pickup);
        pickupGained.Add(false);
        pickupGainedStored.Add(false);
        return pickups.Count - 1;
    }

    public void PickupGained(int index)
    {
        pickupGained[index] = true;
    }

    public int RegisterButton(GameObject button)
    {
        buttons.Add(button);
        buttonPressed.Add(false);
        buttonPressedStored.Add(false);
        return buttons.Count - 1;
    }

    public void ButtonPressed(int index)
    {
        buttonPressed[index] = true;
    }

    public void UnlockPowerUp(int index)
    {
        powerUpUnlocked[index] = true;
        skillCtrl.SetSkillState(index, true);
    }

    public int RegisterDoor(GameObject door)
    {
        doors.Add(door);
        doorLocked.Add(true);
        doorLockedStored.Add(true);
        return doors.Count - 1;
    }

    public void OpenDoor(int index)
    {
        doorLocked[index] = false;
    }

    public int RegisterInventory()
    {
        inventoryIndex++;
        return inventoryIndex;
    }

    public void UseInventory()
    {

        inventory = -1;
        invCtrl.SetSprite(null);
        inventorySprite = null;
       
    }

    public void GainInventory(int index, Sprite image)
    {
        inventory = index;
        invCtrl.SetSprite(image);
        inventorySprite = image;
    }

    public int RegisterPaperWall(GameObject paperWall)
    {
        this.paperWall.Add(paperWall);
        paperWallDestroyed.Add(false);
        paperWallDestroyedStored.Add(false);
        return this.paperWall.Count - 1;
    }

    public void DestoryPaperWall(int index)
    {
        paperWallDestroyed[index] = true;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
        enemySpawnPoints.Add(enemy.transform);
    }

    public void Save()
    {
        for (int i = 0; i < pickupGained.Count; i++)
        {
            pickupGainedStored[i] = pickupGained[i];
        }
        for (int i = 0; i < buttonPressed.Count; i++)
        {
            buttonPressedStored[i] = buttonPressed[i];
        }
        for (int i = 0; i < powerUpUnlocked.Count; i++)
        {
            powerUpUnlockedStored[i] = powerUpUnlocked[i];
        }
        // save door state
        for (int i = 0; i < doorLocked.Count; i++)
        {
            doorLockedStored[i] = doorLocked[i];
        }
        // save inventory state
        inventoryStored = inventory;
        inventoryStoredSprite = inventorySprite;
        // save paper wall state
        for (int i = 0; i < paperWall.Count; i++)
        {
            paperWallDestroyedStored[i] = paperWallDestroyed[i];
        }
    }

    public void Load()
    {
        for (int i = 0; i < pickupGained.Count; i++)
        {
            pickupGained[i] = pickupGainedStored[i];
            pickups[i].GetComponent<PickUpCtrl>().SetState(pickupGainedStored[i]);
        }
        for (int i = 0; i < buttonPressed.Count; i++)
        {
            buttonPressed[i] = buttonPressedStored[i];
            buttons[i].GetComponent<ButtonCtrl>().SetState(buttonPressedStored[i]);
        }
        for (int i = 0; i < powerUpUnlocked.Count; i++)
        {
            powerUpUnlocked[i] = powerUpUnlockedStored[i];
            skillCtrl.SetSkillState(i, powerUpUnlockedStored[i]);
        }
        for (int i = 0; i < doorLocked.Count; i++)
        {
            doorLocked[i] = doorLockedStored[i];
            doors[i].GetComponent<DoorCtrl>().SetState(doorLockedStored[i]);
        }

        inventory = inventoryStored;
        inventorySprite = inventoryStoredSprite;
        if (inventory != -1)
        {
            invCtrl.SetSprite(inventorySprite);
        } else
        {
            invCtrl.SetSprite(null);
        }

        // load paper wall state
        for (int i = 0; i < paperWall.Count; i++)
        {
            paperWallDestroyed[i] = paperWallDestroyedStored[i];
            paperWall[i].SetActive(!paperWallDestroyedStored[i]);
        }

        resetEnemy = true;
        DeathEvent.Invoke();
     }

    private void LateUpdate()
    {
        if (resetEnemy)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetActive(true);
                enemies[i].transform.position = enemySpawnPoints[i].position;
                enemies[i].GetComponent<MonsterUtil>().Reset();
            }
            resetEnemy = false;
        }
    }
}
