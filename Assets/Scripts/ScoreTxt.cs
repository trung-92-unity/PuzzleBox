using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTxt : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;

    private void Start()
    {
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = "" + scoreValue;
    }
    public string ScoreReset()
    {
        scoreValue = 0;
        score.text = "" + scoreValue;
        return score.text;
    }
}
   
