using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DayNightCycle : MonoBehaviour
{
    public SpriteRenderer rendu;
    public GameObject poulet;
    public Rigidbody2D rig;

    private Color morningColor = new Color32(255, 0, 0, 100);
    private Color noonColor = new Color32(0, 255, 0, 100);
    private Color eveningColor = new Color32(0, 0, 255, 100);
    private Color nightColor = new Color32(255, 255, 255, 100);

    private Color current_color;
    private Color target_color;


    private enum TCycle
    {
        morning,
        noon,
        evening,
        night
    }

    private TCycle state;
    private TCycle current_state;

    private bool Morning()
    {
        if (GetTime() >= 15 & GetTime() < 30)
        { return true; }
        else
        {
            return false;
        }
    }
    private bool Noon()
    {
        if (GetTime() >= 30 & GetTime() < 45)
        { return true; }
        else
        {
            return false;
        }
    }
    private bool Evenning()
    {
        if (GetTime() >= 45 & GetTime() <= 60)
        { return true; }
        else
        {
            return false;
        }
    }
    private bool Night()
    {
        if (GetTime() >= 0 & GetTime() < 15)
        { return true; }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame updat
    void Start()
    {
        rendu = GetComponent<SpriteRenderer>();
        poulet = GetComponent<GameObject>();
        rig = GetComponent<Rigidbody2D>();


        SetState();
        rendu.color = current_color;
        current_state = state;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        SetState();
        if (current_state != state)
        {
            //Debug.Log("Current state: " + current_state + " State: " + state);
            StartCoroutine(CTransitTime());
            current_state = state;
        }
    }

    IEnumerator CTransitTime()
    {
        float tick = 0f;
        while (rendu.color != target_color)
        {
            tick += Time.deltaTime * 1f;
            rendu.color = Color.Lerp(current_color, target_color, tick);
            yield return null;
        }
    }

    private void SetState()
    {
        //state =
        //         (Morning() ? TCycle.morning : state) |
        //       (Noon() ? TCycle.noon : state) |
        //     (Evenning() ? TCycle.evening : state) |
        //   (Night() ? TCycle.night : state);

        if (Morning()) { state = TCycle.morning; }
        else if (Noon()) { state = TCycle.noon; }
        else if (Evenning()) { state = TCycle.evening; }
        else if (Night()) { state = TCycle.night; }

        switch (state)
        {
            case TCycle.morning:
                current_color = morningColor;
                target_color = noonColor;
                break;
            case TCycle.noon:
                current_color = noonColor;
                target_color = eveningColor;
                break;
            case TCycle.evening:
                current_color = eveningColor;
                target_color = nightColor;
                break;
            case TCycle.night:
                current_color = nightColor;
                target_color = morningColor;
                break;
            default:
                break;
        }
    }

    public int GetTime()
    {
        int currentHour = System.DateTime.Now.Second;
        return currentHour;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Joueur"))
        {
            Debug.Log("Frappe");
        }
    }

}
