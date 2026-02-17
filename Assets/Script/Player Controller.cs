using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Vector3 playerInput;
    private Rigidbody rb;
    public float speed = 5;
    public score scoreScript;
    bool isFacingRight = true;
    bool isFacingUp = true;
    public SpriteRenderer crabbyImage;
    public Sprite upCrabby;
    public Sprite downCrabby;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
       
    void Update()
    {
        GatherInputs();
        RotateImage();
        Move();
    }
    
    void RotateImage(){

        if(playerInput != Vector3.zero){
            if (isFacingRight && playerInput.x < 0){
            isFacingRight = false;
            crabbyImage.flipX = true;
            }
        if (!isFacingRight && playerInput.x > 0){
            isFacingRight = true;
            crabbyImage.flipX = false;
            }
        if (isFacingUp && playerInput.z < 0){
            isFacingUp = false;
            crabbyImage.sprite = downCrabby;
            }
        if (!isFacingUp && playerInput.z > 0){
            isFacingUp = true;
            crabbyImage.sprite = upCrabby;
            }
        }
    }

    void GatherInputs(){
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }

    void Move(){
        Vector3 direction = IsoVectorConvert(playerInput);
        rb.MovePosition(transform.position+direction * speed * Time.deltaTime);
    }

    private Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0,45f,0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "trash"){
        collision.gameObject.SetActive(false);
        //scoreScript.subtractScore();
        }
        if (collision.gameObject.tag == "food"){
        collision.gameObject.SetActive(false);
        scoreScript.addScore();
        }

    }
}
