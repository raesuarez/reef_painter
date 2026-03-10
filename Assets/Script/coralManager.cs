using UnityEngine;

public class coralManager : MonoBehaviour
{
    public GameManager GM;
    private bool insideCoralArea;
    public GameObject q;
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
            Debug.Log("entered coral area");
            insideCoralArea = true;
            if(q != null)
            {
                q.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            insideCoralArea = false;
            if(q != null)
            {
                q.SetActive(false);
            }
        }
    }

    void changeCoral()
    {
        if (insideCoralArea && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("q pressed");
            StartCoroutine(GM.useMagic(10, 3f));
        }
    }
}
