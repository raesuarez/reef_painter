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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        GM = GameManager.Instance;
        foodSpawnRate = GM.foodSpawnRate;
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
            obj.SetActive(false); 
            trashPool.Add(obj);
            }

        StartCoroutine(SpawnTrash());
    }
    
    void Update()
    {
        foodSpawnRate = GM.foodSpawnRate;
    }

    IEnumerator SpawnFood()
    {
        for (int i = 0; i < foodPoolAmount; i++)
        {
            if (i == totalFood-1)
            {
                i=0;
            }
            GameObject f = GetPooledFood();
            f.transform.position = GetRandomSpawnPosition();
            f.transform.rotation = Quaternion.identity;
            f.SetActive(true);
            yield return new WaitForSeconds(foodSpawnRate);
        }
        
    }
    IEnumerator SpawnTrash()
    {
        for (int i = 0; i < trashPoolAmount; i++)
        {
            if (i == totalTrash-1)
            {
                i=0;
            }
            GameObject t = GetPooledTrash();
            t.transform.position = GetRandomSpawnPosition();
            t.transform.rotation = Quaternion.identity;
            t.SetActive(true);
            yield return new WaitForSeconds(5);
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
        return trashPool[i];
        }
    }
    //3   
    return null;
    }
}
