using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour {

    public Map map;
    public Space space;
    public int moves;
    public bool hasMoved;

	// Use this for initialization
	protected void Start () {
        Vector2 pos = transform.position;
        map = FindObjectOfType<Map>();
        space = map.findSpace(pos);
        hasMoved = false;
        //range = new List<Space>();
    }
	public bool moveUnit(Space t)
    {
        this.transform.position = new Vector3(t.pos.x,t.pos.y,500);  // = t.transform.position.x;
        return true;
    }
    protected void getMoveSet()
    {
        foreach (Space sp in map.map)
        {
            sp.reset();
        }
        bfs(space, moves);
    }

    //bfs
    void bfs(Space current, int move)
    {
        if (move > 0)
        {
            current.findNeighbors();
            current.setRange();
            current.visited = true;
            current.GetComponent<Renderer>().material.color = Color.magenta;
            foreach (Space sp in current.adjacent)
            {
                if (!sp.visited)
                {
                    sp.setRange();
                    sp.visited = true;
                    sp.GetComponent<Renderer>().material.color = Color.magenta;
                    bfs(sp, move - 1);
                }
            }
        }
    }

    void Update()
    {
        Vector2 pos = transform.position;
        space = map.findSpace(pos);
        getMoveSet();
    }
    
    
    //enemy AI
    public Space calcMove()
    {
        Vector2 playerPos = FindObjectOfType<PlayerMove>().transform.position;
        float horizontal = playerPos.x - transform.position.x;
        float vertical = playerPos.y - transform.position.y;
        float x = transform.position.x;
        float y = transform.position.y;
        float rand = Random.Range(0, 10);
        if(rand > 5)
        {
            if (horizontal > 0 && x < map.maxX)
            {
                x += 1;
            }
            else if (horizontal < 0 && x > map.minX)
            {
                x -= 1;
            }
        }
        else
        {
            if (vertical > 0 && y < map.maxY)
            {
                y += 1;
            }
            else if (vertical < 0 && y > map.minY)
            {
                y -= 1;
            }
        }
        Vector2 next = new Vector2(x, y);
        if ((map.containsSpace(next)) && Vector2.Distance(playerPos,next) > 1)
        {
            return map.findSpace(new Vector2(x, y));
        }
        else
        {
            return map.findSpace(new Vector2(transform.position.x, transform.position.y));
        }
    }


}
