using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    [HideInInspector]public bool loading;

    private GameControl gameControl;

    private void Awake()
    {
        if (gameControl == null)
        {
            DontDestroyOnLoad(gameObject);
            gameControl = this;
        }
        else if (gameControl != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save(NodeGrid grid)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.resourceValues = grid.globalValues.defaultValues;

        for (int i = 0; i < grid.nodes.Count; i++)
        {
            if (grid.nodes[i] == grid.currentNode)
            {
                data.currentNodeValue = i;
            }
        }

        data.visitedNodes = new bool[grid.nodes.Count];
        for (int i = 0; i < data.visitedNodes.Length; i++)
        {
            data.visitedNodes[i] = grid.nodes[i].animator.GetBool(DialogParameters.visitedString);
        }


        data.inventoryItemsNames = new string[grid.inventory.items.Length];
        for (int i = 0; i < grid.inventory.items.Length; i++)
        {
            if (grid.inventory.items[i] != null)
            {
                data.inventoryItemsNames[i] = grid.inventory.items[i].name;
            }
        }

        bf.Serialize(file, data);
        file.Close();

    }

    public void Load(NodeGrid grid)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat") && loading)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            grid.globalValues.defaultValues = data.resourceValues;
            grid.startingNodeValue = data.currentNodeValue;
            for (int i = 0; i < grid.nodes.Count; i++)
            {
                grid.nodes[i].animator.SetBool(DialogParameters.visitedString, data.visitedNodes[i]);
            }

            Item[] itemResources = Resources.LoadAll<Item>("") as Item[];
            for (int i = 0; i < data.inventoryItemsNames.Length; i++)
            {
                for (int j = 0; j < itemResources.Length; j++)
                {
                    if (itemResources[j].name == data.inventoryItemsNames[i])
                    {
                        grid.inventory.AddItem(itemResources[j]);
                    }
                }
            }
        }
    }

    public void LaunchGame(bool continueGame)
    {
        if (continueGame)
        {
            SceneManager.LoadScene(1);
            loading = true;
        } else
        {
            SceneManager.LoadScene(1);
            loading = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

[Serializable]
class PlayerData
{
    public int[] resourceValues;
    public int currentNodeValue;
    public bool[] visitedNodes;
    public string[] inventoryItemsNames;
}
