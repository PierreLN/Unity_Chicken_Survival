using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cochon;
    public Vector3 tetePortail;
    public Vector3 delta;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CCochon()); 
    }

    // Update is called once per frame
    void Update()
    {
        delta.y = Random.Range(-1.0f, 1.0f);
    }

    IEnumerator CCochon()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.5f));
            Instantiate(cochon, transform.position+ delta, Quaternion.identity);

        }
    }
}
