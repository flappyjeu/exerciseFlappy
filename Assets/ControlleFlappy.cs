using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlleFlappy : MonoBehaviour
{

    //les variables public
    public float vitesseY;
    public float vitesseX;
    //les sprites
    public Sprite flappyBlesse;
    public Sprite flappyBlesseHaut;
    public Sprite flappyGuerit;
    public Sprite flappyGueritHaut;
    //les GameObject
    public GameObject PieceOr;
    public GameObject PackVie;
    public GameObject Champignon;
    public GameObject grille;
    // les audios
    public AudioSource musique;
    public AudioClip SonCol;
    public AudioClip SonOr;
    public AudioClip SonPack;
    public AudioClip SonChamp;
    public AudioClip SonFinPartie;
    //les valeurs
    bool blesse;
    bool partieTerminer;
    //les textes
    public TextMeshProUGUI score;
    public TextMeshProUGUI texteFin;
    int compteur = 0;
    Color lacouleur;



    // Start is called before the first frame update
    void Start()
    {
        texteFin.gameObject.SetActive(false); //desactive le texte de fin lorsque le jeu commence
        lacouleur = texteFin.color; //lacouleur est égale à la couleur du texte de fin
        lacouleur.a = 0; //le alpha du texte de est égal à zéro
        texteFin.color = lacouleur; //le texte de fin est égale à lacouleur
        grille.GetComponent<Animator>().enabled = false; //desactive l'animation de la grille lorsque le jeu commence

    }

    // Update is called once per frame
    void Update()
    {
        //si le bouton A ou fleche de gauche...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vitesseX = -2; //la vitesse en X change

            //si la partieTerminer est true...
            if (partieTerminer == true)
            {
                vitesseX = 0; vitesseX = 0; //la vitesse en X change
            }
        }
        //sinon si le bouton D ou fleche de droite...
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vitesseX = 2;

            //si la partieTerminer est true...
            if (partieTerminer == true)
            {
                vitesseX = 0; //la vitesse en X change
            }
        }

        //si le bouton W ou fleche de Haut est enfoncé...
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            vitesseY = 7; //la vitesse en Y change

            //si flappy n'est pas blesser...
            if (blesse == false)
            {
                GetComponent<SpriteRenderer>().sprite = flappyGuerit; //le sprite change
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = flappyBlesse; //le sprite change
            }

            //si la partieTerminer est true...
            if (partieTerminer == true)
            {
                vitesseY = 0; //la vitesse en Y change
                GetComponent<SpriteRenderer>().sprite = flappyBlesseHaut; //le sprite change
            }




        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y; // il y a un effet de velocité avec le composant rigidbody2D lorsque flappy saute
        }


        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        //si le bouton W ou fleche de Haut est relaché...
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            //si flappy n'est pas blesser...
            if (blesse == false)
            {
                GetComponent<SpriteRenderer>().sprite = flappyGueritHaut; //le sprite change
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = flappyBlesseHaut; //le sprite change
            }
        }

        //si la partieTerminer est true...
        if (partieTerminer == true)
        {
            lacouleur.a += 0.001f; //le alpha augmente
            texteFin.color = lacouleur;

            if (texteFin.fontSize < 107) //si le font est plus petit que 107
            {
                texteFin.fontSize++; //le font size augmente
            }
        }
    }



    void OnCollisionEnter2D(Collision2D infoCollision) //fonction pour detecter les collision 2d
    {
        //si l'objet flappy touche une colonne
        if (infoCollision.gameObject.name == "Colonne")
        {
            compteur -= 5; //moins 5 points chaque fois que flappy touche une colonne
            score.text = compteur.ToString();

            {
                if (blesse == false) //si flappy n'est pas blesser...
                {
                    GetComponent<AudioSource>().PlayOneShot(SonCol); //joue le son une seule fois lorsque flappy touche une colonne
                    GetComponent<SpriteRenderer>().sprite = flappyBlesse; //le sprite change
                }

                else
                {
                    partieTerminer = true; //partieTerminer devient true 
                    GetComponent<Rigidbody2D>().freezeRotation = false; //empeche flappy de bouger
                    GetComponent<Collider2D>().enabled = false; //enleve le collider
                    GetComponent<Rigidbody2D>().angularVelocity = 45; //flappy tourne à 45 degrée 
                    texteFin.gameObject.SetActive(true); //le texte de fin est activer
                    GetComponent<AudioSource>().PlayOneShot(SonFinPartie);//joue le son une seule fois 
                    Invoke("relancerScene", 5f); //la scene relance après 5 secondes
                }

                blesse = true; //flappy blesse devient true


            }
        }

        //si l'objet flappy touche une Piece d'or
        else if (infoCollision.gameObject.name == "PieceOr")
        {
            infoCollision.gameObject.SetActive(false); //désactive la piece d'or
            Invoke("ReactiverPiece", 7f); //réactive la pièce après 7 secondes
            compteur += 5; //ajoute 5 points
            score.text = compteur.ToString();
            float valeurAleatoireY = Random.Range(-1, 1); // la position en Y change aléatoirement
            infoCollision.gameObject.transform.position = new Vector2(infoCollision.gameObject.transform.position.x, valeurAleatoireY);
            GetComponent<AudioSource>().PlayOneShot(SonOr, 1f); ; //joue le son une seule fois lorsque flappy touche une Pièce d'or
            grille.GetComponent<Animator>().enabled = true; //active l'animation de la grille
            Invoke("desactiverAnimation", 4f); //désactive l'animation apres 4 secondes
        }

        //si l'objet flappy touche un Pack de vie
        else if (infoCollision.gameObject.name == "PackVie")
        {
            infoCollision.gameObject.SetActive(false); //désactive le pack de vie
            Invoke("ReactiverPackVie", 7f); //réactive la pièce après 7 secondes
            compteur += 5; //ajoute 5 points
            score.text = compteur.ToString();
            blesse = false; // flappy n'est plus blesser
            GetComponent<SpriteRenderer>().sprite = flappyGuerit; //le sprite change
            GetComponent<AudioSource>().PlayOneShot(SonPack, 1f); //joue le son une seule fois
        }

        //si l'objet flappy touche un champignon
        else if (infoCollision.gameObject.name == "Champignon")
        {
            infoCollision.gameObject.SetActive(false); //désactive le pack de vie
            Invoke("ReactiverChampignon", 7f); //réactive la pièce après 7 secondes
            compteur += 10; //ajoute 5 points
            score.text = compteur.ToString();
            transform.localScale *= 2f; //flappy devient plus grod
            GetComponent<AudioSource>().PlayOneShot(SonChamp, 1f); //joue le son une seule fois
        }
    }

    void ReactiverPiece() //fonction pour réactiver la Pièce en or
    {
        PieceOr.SetActive(true);
    }

    void ReactiverPackVie()  //fonction pour réactiver le Pack de vie
    {
        PackVie.SetActive(true);
    }

    void ReactiverChampignon() //fonction pour réactiver le champignon
    {
        Champignon.SetActive(true);
        transform.localScale /= 2f;
    }

    void relancerScene() //fonction pour relancer la scene
    {
        SceneManager.LoadScene("exerciseFlappy");
    }

    void desactiverAnimation() //desactiver l'animation apres 4 secondes
    {
        grille.GetComponent<Animator>().enabled = false;
    }

}
