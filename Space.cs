using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour {

    public Map map;
    public Vector2 pos;

    //approachable from different directions
	bool approachNorth;
    bool approachEast;
    bool approachSouth;
    bool approachWest;

    //can be moved to
    bool walkable;
    public bool inRange;

    //adjacent spaces
    public List<Space> adjacent;

    //bfs
    public bool visited;


	void Start () {
        approachNorth = true;
        approachEast = true;
        approachSouth = true;
        approachWest = true;
        walkable = true;
        inRange = false;

        pos = transform.position;
        map = FindObjectOfType<Map>();
        adjacent = new List<Space>();
        visited = false;
    }

    public Space(Vector2 place)
    {
        approachNorth = true;
        approachEast = true;
        approachSouth = true;
        approachWest = true;
        walkable = true;
        inRange = false;

        pos = place;
        map = FindObjectOfType<Map>();
        adjacent = new List<Space>();
        visited = false;
    }

    public void setRange()
    {
        inRange = true;
    }

    public void reset()
    {
        GetComponent<Renderer>().material.color = Color.white;
        adjacent.Clear();
        inRange = false;
        visited = false;
    }
	
	public void findNeighbors()
    {
        reset();

        checkSpace(Vector2.up);
        checkSpace(Vector2.down);
        checkSpace(Vector2.right);
        checkSpace(Vector2.left);
    }

    void checkSpace(Vector2 direction)
    {
        Vector2 next = pos + direction;
        if ((next.x <= map.maxX && next.x >= map.minX) && (next.y <= map.maxY && next.y >= map.minY))
        {
            Space sp = map.findSpace(pos + direction);
            if (sp != null)
            {
                if (direction.Equals(Vector2.up) && sp.approachSouth)
                {
                    adjacent.Add(sp);
                }
                else if (direction.Equals(Vector2.down) && sp.approachNorth)
                {
                    adjacent.Add(sp);
                }
                else if (direction.Equals(Vector2.right) && sp.approachWest)
                {
                    adjacent.Add(sp);
                }
                else if (direction.Equals(Vector2.left) && sp.approachEast)
                {
                    adjacent.Add(sp);
                }
            }
        }
       
    }


    string toString()
    {
        return "" + pos;
    }
}
