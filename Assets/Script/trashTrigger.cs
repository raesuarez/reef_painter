using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class trashTrigger : MonoBehaviour
{   public GameManager GM;
    public collectibleSpawner cs;
    private bool insideTrash;
    void Start()
    {
        GM = GameManager.Instance;
        cs = FindAnyObjectByType<collectibleSpawner>();
    }
    void Update()
    {
        if (insideTrash && Input.GetKeyDown(KeyCode.Q)&& GM.energyScore > 5)
        {
            StartCoroutine(destroyTrash());
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //activate Q  image in the UI
            insideTrash = true;
        }
    }
    void OnTriggerExit(Collider other)
        {
            insideTrash = false;
            //deactivate Q  image in the UI
        }
    IEnumerator destroyTrash()
    {
        //run crab animation
        yield return new WaitForSeconds(3f); // do this however long the crab animation runs
        GM.coralSap(5);
        this.gameObject.SetActive(false);
        cs.currentTrashAmount -= 1;
    }
}
