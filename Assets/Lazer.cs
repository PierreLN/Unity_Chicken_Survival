using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private VolumetricLines.VolumetricLineBehavior lazer;
    private Vector3 mouvementEnd;
    public float vitesseLazer_x = 0.30f;
    public float vitesseLazer_y = 0.30f;
    public float tailleLazer = 2.0f;
    private float a, b;
    public GameObject cible;
    GameObject[] lasers;

    void Start()
    {
        lazer = GetComponent<VolumetricLines.VolumetricLineBehavior>();
        lazer.StartPos = new Vector3(0.4f, 0.0f, 0.0f);

        mouvementEnd.x = 0.0f;
        mouvementEnd.y = 0.0f;
        mouvementEnd.z = 0.0f;

        a = Random.Range(-0.03f, 0.03f);
        b = Random.Range(-0.03f, 0.03f);

        vitesseLazer_x += a;
        vitesseLazer_y += b;
    }

    void Update()
    {
        mouvementEnd.x += vitesseLazer_x;
        mouvementEnd.y += vitesseLazer_y;
        lazer.EndPos = new Vector3(mouvementEnd.x, mouvementEnd.y, 0.0f);
        GetComponent<BoxCollider2D>().offset = new Vector2(mouvementEnd.x, mouvementEnd.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Joueur") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Mur"))
        {
            Destroy(this.gameObject);

        }
    }
}

