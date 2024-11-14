using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlanting : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    [HideInInspector] public List<GameObject> dirtTiles;


    public void StartPlantTree(){
        StartCoroutine(PlantTree());
    }

    private IEnumerator PlantTree(){
        foreach(GameObject dirtTiles in dirtTiles){
            yield return new WaitForSeconds(1f);
            Instantiate(treePrefab, dirtTiles.transform.position, Quaternion.identity);
        }

        Debug.Log("Tree is finished planting");
    }
}
