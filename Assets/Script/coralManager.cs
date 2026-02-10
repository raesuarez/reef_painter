using UnityEngine;

public class coralManager : MonoBehaviour
{
    public GameObject coralUI;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            coralUI.gameObject.SetActive(true);
            Cursor.visible = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            coralUI.gameObject.SetActive(false);
            Cursor.visible = false;
        }
    }
}
