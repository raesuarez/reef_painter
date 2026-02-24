using Unity.VisualScripting;
using UnityEngine;

public class setCoralColor : MonoBehaviour
{
    public GameManager GM;
    public Material coralShader;
    public float coralSat;
    void Start()
    {
        GM = GameManager.Instance;
    }
    void Update()
    {
        coralSat = GM.coralSaturation*5;

        coralShader.SetFloat("_Spread", coralSat);
    }


}
