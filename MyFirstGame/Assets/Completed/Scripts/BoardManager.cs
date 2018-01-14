using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class BordManager : MonoBehaviour {


    [Serializable]
    public class Count
    {
        public int max, min;
        
        public Count(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

    }


    public int columns = 8; // taille colonne
    public int rows = 8;  //taille ligne
    public Count wallCount = new Count(5,7); // definit le minimun et la maximum de mur dans le niveaux
    public Count foodCount = new Count(1,5); // definit le minimun et la maximum de nouriture dans le niveaux
    public GameObject exit; // cree variable sorite sol mur ect ...
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] foodTiles;

    private Transform boardHolder;
    private List<Vector3> gridPosition = new List<Vector3>(); // tableau du niveauxytu(



    void InitialiseList()
    {
        gridPosition.Clear();

        for (int x = 1; x < columns -1; x++)
        {
            for (int y = 1; y < rows -1 ; y++)
            {
                gridPosition.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for(int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }

    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPosition.Count);
        Vector3 randomPositon = gridPosition[randomIndex];
        gridPosition.RemoveAt(randomIndex);
        return randomPositon;
    }

    void LayoutObjectAtRandom(GameObject[] titleArray, int minimum, int maximun)
    {
        int objectCount = Random.Range(minimum, maximun + 1 );
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject titleChoice = titleArray[Random.Range(0, titleArray.Length)];
            Instantiate(titleChoice, randomPosition, Quaternion.identity);

        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.min, wallCount.max);
        LayoutObjectAtRandom(foodTiles, foodCount.min, foodCount.max);
        int enemyCount = (int)Math.Log(level,2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);


    }
}
