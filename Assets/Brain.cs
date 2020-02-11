using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int DNALength = 2; //What to do when seeing platform / What to do when not seeing platform
    public float timeAlive;
    public float timeWalking;

    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeGround = true;
    public LayerMask mask;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            alive = false; 
        }
    }

    public void Init()
    {
        //0 forward
        //1 left
        //2 right
        dna = new DNA(DNALength, 3);
        timeAlive = 0.0f;
        alive = true;
    }

    private void Update()
    {
        if (!alive) return;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 0.1f);
        seeGround = false;
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit, 10, mask))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.tag == "platform")
            {
                seeGround = true;
            }
        }
        timeAlive = PopulationManager.elapsed;

        //Read DNA
        float turn = 0;
        float move = 0;
        if (seeGround)
        {
            if (dna.GetGene(0) == 0)
            {
                move = 1; timeWalking += 1;
            }
            else if (dna.GetGene(0) == 1) turn = -90;
            else if (dna.GetGene(0) == 2) turn = 90;
        }
        else
        {
            if (dna.GetGene(1) == 0)
            {
                move = 1; ; timeWalking += 1;
            }
            else if (dna.GetGene(1) == 1) turn = -90;
            else if (dna.GetGene(1) == 2) turn = 90;
        }

        this.transform.Translate(0, 0, move * 0.1f);
        this.transform.Rotate(0, turn, 0);
    }
}