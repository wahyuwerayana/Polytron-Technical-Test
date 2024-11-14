using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TileGenerationSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilesList;
    [SerializeField] private int gridSize;
    private TreePlanting treePlantingScript;


    private void Start()
    {
        treePlantingScript = GetComponent<TreePlanting>();
        GenerateRandomTile();
    }

    private GameObject GetRandomTile(List<GameObject> tiles){
        return tiles[Random.Range(0, tiles.Count)];
    }

    private void GenerateRandomTile(){
        GameObject[,] gridArray = new GameObject[gridSize, gridSize];
        HashSet<Vector3Int> usedPositions = new HashSet<Vector3Int>();

        // Taruh tile di tempat random untuk setiap jenis tile
        foreach(GameObject tile in tilesList){
            Vector3Int tilePosition;
            do{
                int xLocation = Random.Range(0, gridSize);
                int zLocation = Random.Range(0, gridSize);
                tilePosition = new Vector3Int(xLocation, 0, zLocation);
            } while(usedPositions.Contains(tilePosition));

            usedPositions.Add(tilePosition);
            gridArray[tilePosition.x, tilePosition.z] = tile;
            GameObject spawnedTile = Instantiate(tile, tilePosition, Quaternion.identity);
            if(spawnedTile.CompareTag("Dirt")){
                treePlantingScript.dirtTiles.Add(spawnedTile);
            }
        }

        // Isi sisa tile di tempat yang kosong
        for(int i = 0; i < gridSize; i++){
            for(int j = 0; j < gridSize; j++){
                if(gridArray[i, j] == null){
                    GameObject spawnedTile = Instantiate(GetRandomTile(tilesList), new Vector3(i, 0, j), Quaternion.identity);
                    if(spawnedTile.CompareTag("Dirt")){
                        treePlantingScript.dirtTiles.Add(spawnedTile);
                    }
                }
            }
        }

        treePlantingScript.StartPlantTree();
    }
}
