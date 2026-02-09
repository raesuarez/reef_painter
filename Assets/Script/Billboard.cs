using UnityEngine;

public class Billboard : MonoBehaviour
{
    private BillboardType billboardType;
    public enum BillboardType {LookAtCamera, CameraForward}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate(){
         switch (billboardType){
            case BillboardType.LookAtCamera:
            transform.LookAt(Camera.main.transform.position, Vector3.up);
            break;
            case BillboardType.CameraForward:
            transform.forward = Camera.main.transform.forward;
            break;
            default:
            break;
         }
    }
}
