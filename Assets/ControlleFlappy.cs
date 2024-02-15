using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleFlappy : MonoBehaviour
{

    public float vitesseY;
    public float vitesseX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vitesseX = -3;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vitesseX = 3;
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            vitesseY = 8;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }



        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
    }
    }
