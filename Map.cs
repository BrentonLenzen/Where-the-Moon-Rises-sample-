using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    //public List<Space> map;
    public Space[] map;
    public GameObject[] list;
    public List<Unit> enemies;
    public int items;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

	// Use this for initialization
	void Start () {
        //map = new List<Space>();
        
        items = 0;
        list = GameObject.FindGameObjectsWithTag("Tile");
        map = new Space[list.Length];
        init();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject i in temp)
        {
            enemies.Add(i.GetComponent<Unit>());
        }
    }
	
    public void init()
    {
        int i = 0;
        foreach(GameObject sp in list)
        {
            map[i] = sp.GetComponent<Space>();
            i++;
        }
    }

    public void reset()
    {
        foreach(Space sp in map)
        {
            sp.reset();
        }
    }


    public Space findSpace(Vector2 pos)
    {
        foreach(Space sp in map)
        {
            if (sp != null && (sp.pos.Equals(pos) || Vector2.Distance(pos,sp.pos)<.5))
                return sp;
        }
        return null;
    }

    public bool containsSpace(Vector2 pos)
    {
        if (findSpace(pos) == null)
            return false;
        return true;
    }

    public bool enemyAtSpace(Vector2 sp)
    {
        foreach(Unit en in enemies)
        {
            if (sp.Equals(en.transform.position))
                return true;
        }
        return false;
    }
}
