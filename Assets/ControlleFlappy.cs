using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlleFlappy : MonoBehaviour
{

    public float vitesseY;
    public float vitesseX;
    public Sprite flappyBlesse;
    public Sprite flappyBlesseHaut;
    public Sprite flappyGuerit;
    public Sprite flappyGueritHaut;
    public GameObject PieceOr;
    public GameObject PackVie;
    public GameObject Champignon;
    public AudioSource musique;
    public AudioClip SonCol;
    public AudioClip SonOr;
    public AudioClip SonPack;
    public AudioClip SonChamp;
    public AudioClip SonFinPartie;
    bool blesse;
    bool partieTerminer;
    public TextMeshProUGUI score;
    public TextMeshProUGUI texteFin;
    int compteur = 0;

    

    // Start is called before the first frame update
    void Start()
    {
       // texteFin.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vitesseX = -2;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vitesseX = 2;
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            vitesseY = 7;

            if (blesse == false)
            {
                GetComponent<SpriteRenderer>().sprite = flappyGuerit;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = flappyBlesse;
            }


            
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }


        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (blesse == false)
            {
                GetComponent<SpriteRenderer>().sprite = flappyGueritHaut;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = flappyBlesseHaut;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        //si l'objet flappy touche une colonne le sprite change
        if (infoCollision.gameObject.name == "Colonne")
        {
            compteur -= 5; //enleve 5 points chaque fois que flappy touche une colonne
            score.text = compteur.ToString();

            {
                if (blesse == false)
                {
                    GetComponent<AudioSource>().PlayOneShot(SonCol);
                    GetComponent<SpriteRenderer>().sprite = flappyBlesse;
                }

                else
                {
                    partieTerminer = true;
                    GetComponent<Rigidbody2D>().freezeRotation = false;
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<AudioSource>().PlayOneShot(SonFinPartie);
                    Invoke("relancerScene", 5f);
                }

                blesse = true;


            }
        }

        else if (infoCollision.gameObject.name == "PieceOr")
        {
            infoCollision.gameObject.SetActive(false);
            Invoke("ReactiverPiece", 7f);
            compteur += 5; //ajoute 5 points
            score.text = compteur.ToString();
            float valeurAleatoireY = Random.Range(-1, 1);
            infoCollision.gameObject.transform.position = new Vector2(infoCollision.gameObject.transform.position.x, valeurAleatoireY);
            GetComponent<AudioSource>().PlayOneShot(SonOr, 1f);
        }

        else if (infoCollision.gameObject.name == "PackVie")
        {
            infoCollision.gameObject.SetActive(false);
            Invoke("ReactiverPackVie", 7f);
            compteur += 5; //ajoute 5 points
            score.text = compteur.ToString();
            GetComponent<SpriteRenderer>().sprite = flappyGuerit;
            GetComponent<AudioSource>().PlayOneShot(SonPack, 1f);
        }

        else if (infoCollision.gameObject.name == "Champignon")
        {
            infoCollision.gameObject.SetActive(false);
            Invoke("ReactiverChampignon", 7f);
            compteur += 10; //ajoute 5 points
            score.text = compteur.ToString();
            transform.localScale *= 2f;
            GetComponent<AudioSource>().PlayOneShot(SonChamp, 1f);
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

    void relancerScene()
    {
        SceneManager.LoadScene("exerciseFlappy");
    }

}
