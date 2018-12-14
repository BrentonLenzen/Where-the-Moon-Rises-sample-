using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Movment
{
    public bool inMenu = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<TurnManager>().GetComponent<TurnManager>().playerTurn)
        {
            // do nothing;
        }
        else
        {
            if(!inMenu)
            {
            getSelectedTile();
            }
        }

    }
    public void getSelectedTile()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.tag == "Tile")
                {

                    Space t = hit.collider.GetComponent<Space>();
                    
                    //moveUnit(t);
                    if (t.inRange)
                    {
                        // Debug.Log("Tile hit = " + t.name);
                        moveUnit(t);
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnManager>().turnEnd();
                    }
                }
            }
        }


    }
    public bool Move(bool isHuman)
    {
        playerTurn player = this.GetComponentInParent<playerTurn>();
      //  StartCoroutine(waitForInput());
        //Debug.Log("moving");
        player.playerPhase(isHuman);
        return true;

    }
    IEnumerator waitForInput()
    {
        bool temp = true;
        while(temp)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.tag == "Tile")
                    {

                        Space t = hit.collider.GetComponent<Space>();
                        //moveUnit(t);
                        if (t.inRange)
                        {
                            // Debug.Log("Tile hit = " + t.name);
                            moveUnit(t);
                            temp = false;
                        }
                    }
                }
            }
        }
        return null;
    }
}
