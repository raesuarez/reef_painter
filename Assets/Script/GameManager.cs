using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;

public class GameManager : MonoBehaviour
{
    public float temperature = 0.5f;
    public float coralSaturation;
    public float foodSpawnRate;
    public UnityEvent highTemp;
    public int energyScore;
    public GameObject lowEnergy;
    public static GameManager Instance;
    public GameObject crabMagic;
    public ParticleSystem crabMagicParticles;
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

        crabMagic = GameObject.Find("crabMagic");
        crabMagicParticles = crabMagic.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(temperature < 1.0f && temperature > 0.0f)
        {
            temperature += 0.00333f * Time.deltaTime; // in 5 minutes 0 to 1
            coralSaturation = 1f - temperature; // coral saturation is inverse to temp
            foodSpawnRate = coralSaturation * 5f; // the less saturated the corals are, the more food spawns
            // if the coral saturation is 0.1, food spawns every 0.5 seconds
            // if coral saturation is 1, food spawns every 5 seconds
        }
        else
        {
            highTemp.Invoke();
        }
    }
    void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void addScore(){
        energyScore+=1;
    }
    public void coralSap(int amountSapped){
        Debug.Log("coral sapped!");
        energyScore -= amountSapped;
        temperature -= 0.1f;
        Debug.Log("coral score sapped!");
    }
    IEnumerator lowEnergyWarning()
    {
        lowEnergy.SetActive(true);
        yield return new WaitForSeconds(3);
        lowEnergy.SetActive(false);
    }
    public IEnumerator useMagic(int amountSapped)
    {
        //run animation for crab
        if(temperature >= 0.1f && energyScore >= 10)
        {
        crabMagicParticles.Play();
        yield return new WaitForSeconds(3f); // change length to length of animation
        coralSap(amountSapped);
        crabMagicParticles.Stop();
        }
        else if(energyScore < 10)
        {
            StartCoroutine(lowEnergyWarning());
        }
    }
    /* void GetCurrentFill(Image uiImage, float fill)
    {
        float fillAmount = fill / 100;
        uiImage.fillAmount = fillAmount;
    } */
}
