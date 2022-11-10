using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Poulet : MonoBehaviour
{
    public float vitesse = 5.0f;
    private Rigidbody2D rig;
    private Animator anim;
    public GameObject animationMort;
    private float distance = 0.5f;

    private Vector2 direction = new Vector2();
    private Vector2 derniereDirection = new Vector2();

    public enum TPoulet
    {
        eVivant = 1,
        eMourrant = 2,
        eMort = 3,
        eDance = 4
    }

    public TPoulet etat;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
        anim.speed = 2.0f;
        etat = TPoulet.eVivant;
    }

    // Update is called once per frame
    void Update()
    {
        if (etat == TPoulet.eMourrant)
        {
            anim.SetBool("vivant", false);
            etat = TPoulet.eMort;
            direction = new Vector2(0.0f, 0.0f);
        }

        else if (etat == TPoulet.eMort)
        {
            Invoke("RestartScene", 5f);
        }
        else
        {
            if (etat == TPoulet.eVivant)
            {
                direction.x = Input.GetAxisRaw("Horizontal");
                direction.y = Input.GetAxisRaw("Vertical");
            }

            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
            anim.SetFloat("Speed", direction.sqrMagnitude);

            if (direction.sqrMagnitude > 0.001f)
            {
                derniereDirection = direction;
            }
            else
            {
                anim.SetFloat("Horizontal", derniereDirection.x);
                anim.SetFloat("Vertical", derniereDirection.y);
            }
            direction.Normalize();
        }
    }

    private void FixedUpdate()
    {
        rig.velocity = direction * vitesse;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemi")
            || collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            etat = TPoulet.eMourrant;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("OeufsVictoire"))
        {
            StartCoroutine(CDanceDeVictoire());
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Oeuf"))
        {
            Debug.Log("fraaaa");
            if (SceneManager.GetActiveScene().name == "Cours")
            {
                SceneManager.LoadScene("Grange", LoadSceneMode.Single);
            }
            else if (SceneManager.GetActiveScene().name == "Grange")
            {
                SceneManager.LoadScene("Route", LoadSceneMode.Single);
            }
            
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CDanceDeVictoire()
    {
        while (true)
        {
            etat = TPoulet.eDance;
            direction = new Vector2(0.0f, 0.0f);
            yield return new WaitForSeconds(3.0f); 
            direction = new Vector2(-distance, 0.0f);
            yield return new WaitForSeconds(1.0f);
            direction = new Vector2(0.0f, 0.0f);
            yield return new WaitForSeconds(3.0f);
            direction = new Vector2(distance, 0.0f);
            yield return new WaitForSeconds(1.0f);

        }
    }

}


