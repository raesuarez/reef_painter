using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerInput;
    private Rigidbody rb;
    public float speed = 5;
    public score scoreScript;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
       
    void Update(){
        GatherInputs();
        Look();
    }

    void FixedUpdate(){
        Move();
    }
    
    void Look(){

        if(playerInput != Vector3.zero){
            var relative = (transform.position + playerInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    void GatherInputs(){
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }

    void Move(){
        rb.MovePosition(transform.position + (transform.forward*playerInput.magnitude) * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "trash"){
        collision.gameObject.SetActive(false);
        scoreScript.subtractScore();
        }
    }
}
