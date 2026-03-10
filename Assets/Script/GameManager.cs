using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    [Header("game vars")]
    public float temperature = 0.5f;
    public float coralSaturation;
    public float foodSpawnRate;
    public UnityEvent highTemp;
    [Header("crab")]
    public GameObject crabMagic;
    public ParticleSystem crabMagicParticles;
    public PlayerController controller;
    public Animator crabMagicAnimator;
    [Header("Reef Life")]
    public GameObject lowTempLife;
    public GameObject coolTempLife;
    public GameObject warmTempLife;
    public GameObject warmerTempLife;
    [Header("energy")]
    public Slider energySlider;
    public int energyMultiplier;
    public TMP_Text multiplierText;
    public int energyScore;
    public GameObject lowEnergy;
    public UnityEngine.UI.Image energyFill;
    public UnityEngine.UI.Image energyHandle;
    public GameObject q;
    [Header("temp slider")]
    public Slider tempSlider;
    [Header("Audio")]
    public AudioSource magicSound;
    public AudioSource algaePickup;
    
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
        crabMagicAnimator = controller.GetComponentInChildren<Animator>();
    }
    void Update()
    {
        tempSlider.value = temperature;
        energySlider.value = ((energyScore-(energyMultiplier*10) )/ 10.0f);
        energyMultiplier = energyScore / 10;
        multiplierText.text = energyMultiplier + "x";
        if(temperature < 1.0f && temperature > 0.00f)
        {
            temperature += 0.00333f * Time.deltaTime; // in 5 minutes 0 to 1
            coralSaturation = 1f - temperature; // coral saturation is inverse to temp
            foodSpawnRate = coralSaturation * 5f; // the less saturated the corals are, the more food spawns
            // if the coral saturation is 0.1, food spawns every 0.5 seconds
            // if coral saturation is 1, food spawns every 5 seconds
        }
        else if(temperature < 0.2f && temperature >= 0.0f)
        {
            lowTempLife.SetActive(true);
            coolTempLife.SetActive(true);
            warmTempLife.SetActive(true);
            warmerTempLife.SetActive(true);
        }
        if(temperature < 0.4f && temperature >= 0.2f)
        {
            lowTempLife.SetActive(false);
            coolTempLife.SetActive(true);
            warmTempLife.SetActive(true);
            warmerTempLife.SetActive(true);
        }
        if(temperature < 0.6f && temperature >= 0.4f)
        {
            lowTempLife.SetActive(false);
            coolTempLife.SetActive(false);
            warmTempLife.SetActive(true);
            warmerTempLife.SetActive(true);
        }
        if(temperature < 0.8f && temperature >= 0.6f)
        {
            lowTempLife.SetActive(false);
            coolTempLife.SetActive(false);
            warmTempLife.SetActive(false);
            warmerTempLife.SetActive(true);
        }
        if (temperature > 0.8f)
        {
            lowTempLife.SetActive(false);
            coolTempLife.SetActive(false);
            warmTempLife.SetActive(false);
            warmerTempLife.SetActive(false);
        }
        else if(temperature == 0.00f)
        {
            temperature = 0.01f;
        }
    }
    public void addScore(){
        energyScore+=1;
        algaePickup.Play();
    }
    public void coralSap(int amountSapped){
        //Debug.Log("coral sapped!");
        if(energyScore >= amountSapped)
        {
            energyScore -= amountSapped;
        }
        //Debug.Log("coral score sapped!");
        if(amountSapped == 10 && energyScore >=10)
        {
            magicSound.Play();
            temperature -= 0.1f;
        }
    }
    
    public IEnumerator useMagic(int amountSapped, float seconds)
    {
        //run animation for crab
        if(temperature >= 0.1f && energyScore >= amountSapped)
        {
            controller.enabled = false;
            crabMagicParticles.Play();
            crabMagicAnimator.SetBool("usingMagic", true);
            yield return new WaitForSeconds(seconds);
            crabMagicParticles.Stop();
            crabMagicAnimator.SetBool("usingMagic", false);
            coralSap(amountSapped);
            controller.enabled = true;
        }
        else if(energyScore < amountSapped)
        {
            StartCoroutine(lowEnergyWarning());
        }
    }
    public IEnumerator lowEnergyWarning()
    {
        lowEnergy.SetActive(true);
        energyFill.color = Color.red;
        energyHandle.color = Color.red;
        yield return new WaitForSeconds(1f);
        lowEnergy.SetActive(false);
        energyFill.color = Color.white;
        energyHandle.color = Color.white;
    }
    
}
