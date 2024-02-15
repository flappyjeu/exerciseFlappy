using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementObjets : MonoBehaviour
{

    public float vitesseX;
    public float positionFin;
    public float positionDebut;
    public float deplacementAleatoire;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (transform.position.x < positionFin)
        {
            deplacementAleatoire = Random.Range(-deplacementAleatoire, deplacementAleatoire);

            transform.position = new Vector3(positionDebut, deplacementAleatoire, 0);
        }

        transform.Translate(vitesseX, 0f, 0f);


        /*transform.position = new Vector2 (positionDebut, deplacementAleatoire);*/
    }
}
