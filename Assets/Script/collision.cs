using UnityEngine;
using UnityEngine.Events;

public class collision : MonoBehaviour
{
    //attached to a collider object not marked as trigger
    public UnityEvent onCollisionEnter;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "trash")
        {
            this.gameObject.SetActive(false);
        }
    }
}
