using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using System.Drawing;

public class Textes : MonoBehaviour
{

    public TextMeshProUGUI leScore;
    int score;
    public GameObject PieceOr;

    // Start is called before the first frame update
    void Start()
    {
        leScore.text = score.ToString(); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        if (infoCollision.gameObject.name == "PieceOr")
        {
            score++;
        }
    }
}
