using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cochon : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private float animSpeed;

    private Vector2 direction = new Vector2();

    public float speed = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();

        direction.y = 0.0f;
        direction.x = 1.0f;
        animSpeed = speed;

        Vector3 pos = transform.position;
        if (pos.x >= 0.0f)
            speed = -speed;
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetFloat("horizontal", rig.velocity.x);
        anim.SetFloat("vertical", rig.velocity.y);
        anim.SetFloat("speed", animSpeed);
    }

    private void FixedUpdate()
    {
        rig.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Joueur"))
        {
            speed = 0f;
            rig.velocity = new Vector2(0f, 0f);
            Destroy(this.gameObject);

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spawner"))
        {

            Destroy(this.gameObject);
        }
    }
}
