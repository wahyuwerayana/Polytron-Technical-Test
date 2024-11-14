using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerationSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilesPrefabList;
    [SerializeField] private int gridSize;
    private TreePlanting treePlantingScript;


    private void Start(){
        treePlantingScript = GetComponent<TreePlanting>();
        GenerateRandomTile();
    }

    private void GenerateRandomTile(){
        GameObject[,] gridArray = new GameObject[gridSize, gridSize];
        
        InitiateEveryTypeOfTiles(gridArray);

        FillRemainingTiles(gridArray);

        treePlantingScript.StartPlantTree();
    }

    private void InitiateEveryTypeOfTiles(GameObject[,] gridArray){
        HashSet<Vector3Int> usedPositions = new HashSet<Vector3Int>();

        // Taruh satu tile di tempat random untuk setiap jenis tile
        foreach(GameObject tile in tilesPrefabList){
            Vector3Int tilePosition;
            do{
                int xLocation = Random.Range(0, gridSize);
                int zLocation = Random.Range(0, gridSize);
                tilePosition = new Vector3Int(xLocation, 0, zLocation);
            } while(usedPositions.Contains(tilePosition));

            usedPositions.Add(tilePosition);
            gridArray[tilePosition.x, tilePosition.z] = tile;
            GameObject spawnedTile = Instantiate(tile, tilePosition, Quaternion.identity);
            CheckDirtTile(spawnedTile);
        }
    }

    private void FillRemainingTiles(GameObject[,] gridArray){
        // Isi sisa tile di tempat yang kosong
        for(int i = 0; i < gridSize; i++){
            for(int j = 0; j < gridSize; j++){
                if(gridArray[i, j] == null){
                    GameObject spawnedTile = Instantiate(GetRandomTile(tilesPrefabList), new Vector3(i, 0, j), Quaternion.identity);
                    CheckDirtTile(spawnedTile);
                }
            }
        }
    }

    private void CheckDirtTile(GameObject spawnedTile){
        if(spawnedTile.CompareTag("Dirt")){
            treePlantingScript.dirtTilesList.Add(spawnedTile);
        }
    }

    private GameObject GetRandomTile(List<GameObject> tilesList){
        return tilesList[Random.Range(0, tilesList.Count)];
    }
}
