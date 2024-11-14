using TMPro;
using UnityEngine;

public class HouseSpawning : MonoBehaviour
{
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private TMP_Text scoreText;
    private int score = 0;
    private TreePlanting treePlantingScript;
    

    private void Start() {
        treePlantingScript = GetComponent<TreePlanting>();
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            SpawnHouse();
        }
    }

    private void SpawnHouse(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // Raycast dari posisi mouse diklik
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer)){
            if(hit.transform.CompareTag("Dirt") || hit.transform.CompareTag("Desert")){
                TileProperties tileProperties = hit.transform.GetComponent<TileProperties>();
                bool hasObjectSpawned = tileProperties.hasObjectSpawned;

                if(!hasObjectSpawned){
                    Instantiate(housePrefab, hit.transform.position, Quaternion.identity);
                
                    if(hit.transform.CompareTag("Dirt")){
                        treePlantingScript.dirtTilesList.Remove(hit.transform.gameObject);
                        score += 10;
                    } else if(hit.transform.CompareTag("Desert")){
                        score += 2;
                    }

                    scoreText.text = "Score: " + score.ToString();
                    tileProperties.hasObjectSpawned = true;
                }
            }
        }
    }
}
