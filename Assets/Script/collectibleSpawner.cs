using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleSpawner : MonoBehaviour
{
    public static collectibleSpawner SharedInstance;
    public GameManager GM;
    public float foodSpawnRate;
    public List<GameObject> foodPool;
    public GameObject food;
    public int foodPoolAmount;
    public List<GameObject> trashPool;
    public GameObject trash;
    public int trashPoolAmount;
    public int totalFood;
    public int totalTrash;
    public float trashDelayRate;
    public int currentTrashAmount;
    public trashTrigger tt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        GM = GameManager.Instance;
        foodSpawnRate = GM.foodSpawnRate;
        // the less saturated the corals are, the more food spawns
        // if the coral saturation is 0.1, food spawns every 0.5 seconds
        // if coral saturation is 1, food spawns every 5 seconds
        trashDelayRate = 0f;
        
        totalFood = foodPoolAmount;
        totalTrash = trashPoolAmount;

        //food pool
        foodPool = new List<GameObject>();
        for (int i = 0; i < foodPoolAmount; i++) {    
            GameObject obj = (GameObject)Instantiate(food);
            obj.SetActive(false); 
            foodPool.Add(obj);
            }
        StartCoroutine(SpawnFood());

        //trash pool
        trashPool = new List<GameObject>();
        for (int i = 0; i < trashPoolAmount; i++) {    
            GameObject obj = (GameObject)Instantiate(trash);
            trash.GetComponentInChildren<SpriteRenderer>().sprite = tt.trashes[(Random.Range(0, tt.trashes.Count))];
            obj.SetActive(false); 
            trashPool.Add(obj);
            }
        StartCoroutine(SpawnTrash());
    }
    
    void Update()
    {
        foodSpawnRate = GM.foodSpawnRate;
        //Debug.Log(foodSpawnRate);
        trashDelayRate = currentTrashAmount * 0.2f; //5 pieces of trash adds 1 second delay to spawn rate
    }

    IEnumerator SpawnFood()
    {
        for (int i = 0; i < foodPoolAmount; i++)
        {
            Debug.Log("spawning food");
            GameObject f = GetPooledFood();
            if(f != null)
            {   

                f.transform.position = GetRandomSpawnPosition();
                f.transform.rotation = Quaternion.Euler(new Vector3(0,45,0));
                f.SetActive(true);
                yield return new WaitForSeconds(foodSpawnRate + trashDelayRate);
            }
        }
    }
    IEnumerator SpawnTrash()
    {
        for (int i = 0; i < trashPoolAmount; i++)
        {
            GameObject t = GetPooledTrash();
            if(t != null)
            {
                t.transform.position = GetRandomSpawnPosition();
                t.transform.rotation = Quaternion.Euler(new Vector3(0,45,0));
                t.SetActive(true);
                yield return new WaitForSeconds(10);
            }  
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-45, 45);
        float randomZ = Random.Range(-45, 45);
        return new Vector3 (randomX, 0.4f, randomZ);
    }


    public GameObject GetPooledFood() 
    {
    //1
    
    Debug.Log("getting pooled food");
    for (int i = 0; i < foodPool.Count; i++) {
    //2
        if (!foodPool[i].activeInHierarchy) {
        return foodPool[i];
        }
    }
    //3   
    return null;
    }
    public GameObject GetPooledTrash() 
    {
    //1
    
    for (int i = 0; i < trashPool.Count; i++) {
    //2
        if (!trashPool[i].activeInHierarchy) {
            currentTrashAmount += 1;
        return trashPool[i];
        }
    }
    //3   
    return null;
    }
}
