using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float temperature = 0.5f;
    public float coralSaturation;
    public float foodSpawnRate;
    public UnityEvent highTemp;
    public int energyScore;
    public TMP_Text energyText;
    public TMP_Text tempText;
    public string tempLabel;
    public GameObject lowEnergy;
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }
    void Start()
    {
        energyScore = 0;
        temperature = 0.5f;
        coralSaturation = 0.5f;
    }
    void Update()
    {
        if(temperature < 1.0 && temperature > 0.0f)
        {
            temperature += 0.00167f * Time.deltaTime;
            coralSaturation = 1f - temperature;
            foodSpawnRate = coralSaturation * 5f;
        }
        else
        {
            highTemp.Invoke();
        }
        setTempLabel();
        tempText.text = "Temperature: " + tempLabel;

    }

    void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void setScore(){
        energyText.text = "Energy: " + energyScore;
    }

    public void addScore(){
        energyScore+=1;
        setScore();
    }

    public void coralSap(){

        Debug.Log("coral sapped!");

        if(temperature >= 0.1f && energyScore >= 10)
        {
            energyScore -= 10;
            temperature -= 0.1f;
            setScore();
            Debug.Log("coral score sapped!");

        }
        else if(energyScore < 10)
        {
            StartCoroutine(lowEnergyWarning());
        }
    }
    IEnumerator lowEnergyWarning()
    {
        lowEnergy.SetActive(true);
        yield return new WaitForSeconds(3);
        lowEnergy.SetActive(false);
    }
    public void setTempLabel()
    {
        if(temperature < 0.2f)
        {
            tempLabel = "Cold";
        }
        if(temperature > 0.2f && temperature < 0.4f)
        {
            tempLabel = "Cool";
        }
        if(temperature > 0.4f && temperature < 0.6f)
        {
            tempLabel = "Warm";
        }
        if(temperature > 0.6f && temperature < 0.8f)
        {
            tempLabel = "Hot";
        }
        if(temperature > 0.8f && temperature < 1.0f)
        {
            tempLabel = "Too Hot!";
        }
    }
}
