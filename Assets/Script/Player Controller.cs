using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Vector3 playerInput;
    private Rigidbody rb;
    public float speed = 5;
    public GameManager GM;
    public bool isFacingRight = true;
    public bool isFacingUp = true;
    public SpriteRenderer crabbyImage;
    public Sprite upCrabby;
    public Sprite downCrabby;
    //public ParticleSystem crabMagic;
    void Awake()
    {
        GM = GameManager.Instance;
    }
    void Start(){
        GM = GameManager.Instance;
        rb = this.GetComponent<Rigidbody>();
        crabbyImage = GetComponentInChildren<SpriteRenderer>();
        Cursor.visible = false;
        //crabMagic = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }
       
    void Update()
    {
        GatherInputs();
        RotateImage();
        Move();
    }
    
    void RotateImage(){

        //if(playerInput != Vector3.zero){
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
            Debug.Log("changed sprite");
            }
        if (!isFacingUp && playerInput.z > 0){
            isFacingUp = true;
            crabbyImage.sprite = upCrabby;
            }
       // }
    }

    void GatherInputs(){
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
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
        if (collision.gameObject.tag == "food"){
        collision.gameObject.SetActive(false);
        GM.addScore();
        }
    }
}
