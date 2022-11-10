using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tirer_lazer : MonoBehaviour
{
    public GameObject lazer;
    public Vector3 lazer_head;

    public Transform cible;
    private SpriteRenderer rendu;
    public float maxDistance = 10.0f;
    public LayerMask masqueRayon; 


    // Start is called before the first frame update
    void Start()
    {

        rendu = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayon = cible.position - transform.position;
        rayon.Normalize();
        Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), rayon, maxDistance); // vecteur 2D
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayon, maxDistance, masqueRayon);

        if (hit.collider != null) 
        {
            Debug.DrawLine(transform.position, hit.point);
            rendu.color = Color.blue;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Joueur"))
            {
                rendu.color = Color.red;
                GameObject inst = Instantiate(lazer, transform.position, transform.localRotation * Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }
        }
        else
        {
            Debug.DrawRay(transform.position, rayon * maxDistance);
            rendu.color = Color.blue;
        }
    }
}
