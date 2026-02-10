using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    
    public int energyScore;
    public TMP_Text text;

    // Update is called once per frame
    void Start()
    {
        energyScore = 0;
    }

    public void setScore(){
        text.text = "Energy: " + energyScore;
    }

    public void addScore(){
        energyScore++;
        setScore();
    }

    public void subtractScore(){
        energyScore--;
        setScore();
    }

    public void coralSap(){
        energyScore -= 5;
        setScore();
    }
}
