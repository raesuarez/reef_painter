using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using TMPro;

public class trashTrigger : MonoBehaviour
{   public GameManager GM;
    public collectibleSpawner cs;
    private bool insideTrash;
    public List<Sprite> trashes;
    private GameObject q;
    public LayerMask m_LayerMask;
    public AudioSource trashSound;
    public GameObject image;
    void Start()
    {
        GM = GameManager.Instance;
        cs = FindAnyObjectByType<collectibleSpawner>();
        q = GM.q;
        trashes = new List<Sprite>();
    }
    void Update()
    {
        if (insideTrash && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(destroyTrash());
        }
    }
    void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        // Use the OverlapBox to detect if there are any other colliders within this box area.
        // Use the GameObject's center, half the size (as a radius), and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        int i = 0;
        // Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            this.gameObject.SetActive(false);
            // Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            // Increase the number of Colliders in the array
            i++;
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            q.SetActive(true);
            insideTrash = true;
        }
    }
    void OnTriggerExit(Collider other)
        {
            insideTrash = false;
            Debug.Log("trigger exited");
            q.SetActive(false);
        }
    IEnumerator destroyTrash()
    {
        if(GM.energyScore >= 5)
        {
            trashSound.Play();
            StartCoroutine(GM.useMagic(5,1.5f));
            image.SetActive(false);
            yield return new WaitForSeconds(3.1f);
            
            cs.currentTrashAmount -= 1;
            insideTrash = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(GM.lowEnergyWarning());
        }
        
    }
}
