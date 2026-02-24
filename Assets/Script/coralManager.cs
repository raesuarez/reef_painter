using UnityEngine;

public class coralManager : MonoBehaviour
{
    public GameObject coralUI;
    public GameManager GM;
    private bool insideCoralArea;
    void Start()
    {
        GM = GameManager.Instance;
    }
    void Update()
    {
        changeCoral();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            coralUI.gameObject.SetActive(true);
            insideCoralArea = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            coralUI.gameObject.SetActive(false);
            insideCoralArea = false;
        }
    }

    void changeCoral()
    {
        if (insideCoralArea && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("q pressed");
            GM.coralSap();
        }
    }
}
