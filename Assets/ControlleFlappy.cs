using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleFlappy : MonoBehaviour
{

    public float vitesseY;
    public float vitesseX;
    public Sprite flappyBlesse;
    public Sprite flappyGuerit;
    public GameObject PieceOr;
    public GameObject PackVie;
    public GameObject Champignon;
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

    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        //si l'objet flappy touche une colonne le sprite change
        if (infoCollision.gameObject.name == "Colonne")
        {

            GetComponent<SpriteRenderer>().sprite = flappyBlesse;

        }

        else if (infoCollision.gameObject.name == "PieceOr")
        {
            infoCollision.gameObject.SetActive(false);
            Invoke("ReactiverPiece", 7f);
            float valeurAleatoireY = Random.Range(-1, 1);
            infoCollision.gameObject.transform.position = new Vector2(infoCollision.gameObject.transform.position.x, valeurAleatoireY);
        }

        else if (infoCollision.gameObject.name == "PackVie")
        {
            infoCollision.gameObject.SetActive(false);
            Invoke("ReactiverPackVie", 7f);
            GetComponent<SpriteRenderer>().sprite = flappyGuerit;
        }

        else if (infoCollision.gameObject.name == "Champignon")
        {
            infoCollision.gameObject.SetActive(false);
            Invoke("ReactiverChampignon", 7f);
            transform.localScale *= 2f;
        }
    }

    void ReactiverPiece()
    {
        PieceOr.SetActive(true);
    }

    void ReactiverPackVie()
    {
        PackVie.SetActive(true);
    }

    void ReactiverChampignon()
    {
        Champignon.SetActive(true);
        transform.localScale /= 2f;
    }



}
