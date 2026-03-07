using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class trashTrigger : MonoBehaviour
{   public GameManager GM;
    public collectibleSpawner cs;
    private bool insideTrash;
    public List<Sprite> trashes;
    void Start()
    {
        GM = GameManager.Instance;
        cs = FindAnyObjectByType<collectibleSpawner>();
        trashes = new List<Sprite>();
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
            Debug.Log("trigger exited");
            //deactivate Q  image in the UI
        }
    IEnumerator destroyTrash()
    {
        
        StartCoroutine(GM.useMagic(5));
        yield return new WaitForSeconds(4f);
        this.gameObject.SetActive(false);
        cs.currentTrashAmount -= 1;
        insideTrash = false;
    }
}
