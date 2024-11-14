using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanting : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    [HideInInspector] public List<GameObject> dirtTilesList;


    public void StartPlantTree(){
        StartCoroutine(PlantTree());
    }

    private IEnumerator PlantTree(){
        yield return new WaitForSeconds(1f);

        // Break jika list sudah habis
        if(dirtTilesList.Count == 0){
            Debug.Log("Tree is finished planting");
            yield break;
        }
        
        // Tanam pohon yang ada di list dirtTiles
        GameObject currentDirtTile = dirtTilesList[0];
        TileProperties tileProperty = currentDirtTile.GetComponent<TileProperties>();
        bool hasObjectSpawned = tileProperty.hasObjectSpawned;
        
        if(!hasObjectSpawned){
            Instantiate(treePrefab, currentDirtTile.transform.position, Quaternion.identity);
            tileProperty.hasObjectSpawned = true;
        }

        dirtTilesList.Remove(currentDirtTile);
        StartCoroutine(PlantTree());
    }
}
