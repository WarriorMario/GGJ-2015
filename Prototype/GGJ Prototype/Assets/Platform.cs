using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Platform : MonoBehaviour
{
    // Store all blocks in a grid
    List<List<GameObject>> m_Grid;
    // We need a function to generate it in the editor as starting
    public int m_Width;
    public int m_Height;
    [HideInInspector]
    public List<Player> m_Players = new List<Player>();
    // Flood fill
    // Check on what group the player is -> brute force it

    // Use this for initialization
    public void Init()
    {
        // Only initialize once
        if (m_Grid != null)
        {
            return;
        }
        m_Grid = new List<List<GameObject>>();
        for (int row = 0; row < m_Height; ++row)
        {
            m_Grid.Add(new List<GameObject>());
            for (int col = 0; col < m_Width; ++col)
            {
                m_Grid[row].Add(null);
            }
        }
        // Loop through our children and get the blocks -> retard proof for designers
        List<Transform> cubes = GetAllBlocks(transform);
        int num = cubes.Count;

        for (int i = 0; i < num; ++i)
        {
            GameObject obj = cubes[i].gameObject;
            // Get the z (row) and x (col)
            m_Grid[(int)(obj.transform.position.z - transform.position.z)][(int)(obj.transform.position.x - transform.position.x)] = obj;
        }
    }

    void OnBlockDestroy()
    {
        bool[,] visited = new bool[m_Height, m_Width];
        foreach (Player obj in m_Players)
        {
            GameObject block = obj.GetBlock();
            int row = (int)(block.transform.position.z - transform.position.z);
            int col = (int)(block.transform.position.x - transform.position.x);
            if (visited[row, col] == false)
            {
                List<Vector2> region = new List<Vector2>();
                // Flood fill
                //I will use a queue to keep record of the positions we are gonna traverse.
                //Each element in the queue is a coordinate position (row,column) of an element
                //of the matrix.
                //Q = Queue()
                Queue<Vector2> q = new Queue<Vector2>();

                // A container for the up, down, left and right directions.
                Vector2[] dirs = new Vector2[4];
                dirs[0] = new Vector2(-1, 0);
                dirs[1] = new Vector2(1, 0);
                dirs[2] = new Vector2(0, -1);
                dirs[3] = new Vector2(0, 1);

                // Now we will add our initial position to the queue.
                q.Enqueue(new Vector2(row, col));

                // And we will mark the element as null. You will definitely need to
                // use a boolean matrix to mark visited elements. In this case I will simply
                // mark them as null.
                visited[row, col] = true;

                //Go through each element in the queue, while there are still elements to visit.
                while (q.Count != 0)
                {
                    //Pop the next element to visit from the queue.
                    //Remember this is a (row, column) position.
                    Vector2 pos = q.Dequeue();

                    //Add the element to the output region.
                    region.Add(pos);

                    //Check for non-visited position adjacent to this (r,c) position.
                    //These are:
                    //(r + 1, c): down
                    //(r - 1, c): up
                    //(r, c - 1): left
                    //(r, c + 1): right
                    foreach (Vector2 dir in dirs)
                    {
                        //Check if this adjacent position is not null and keep it between
                        //the matrix size.
                        if (pos.x + dir.x < m_Height && pos.y + dir.y < m_Width && pos.x + dir.x >= 0 && pos.y + dir.y >= 0)
                        {
                            if (m_Grid[(int)(pos.x + dir.x)][(int)(pos.y + dir.y)] != null)
                            {
                                //Then add the position to the queue to be visited later if we haven't visited it already
                                if (visited[(int)(pos.x + dir.x), (int)(pos.y + dir.y)])
                                {
                                    continue;
                                }
                                q.Enqueue(new Vector2(pos.x + dir.x, pos.y + dir.y));

                                //And mark this position as visited.
                                visited[(int)(pos.x + dir.x), (int)(pos.y + dir.y)] = true;
                            }
                        }
                    }
                }
            }
        }
        for (int y = 0; y < m_Height; ++y)
        {
            for (int x = 0; x < m_Width; ++x)
            {
                if (visited[y, x] == false)
                {
                    Destroy(m_Grid[y][x]);
                }
            }
        }
    }

    public void DestroyBlock(GameObject obj)
    {
        int row = (int)(obj.transform.position.z - transform.position.z);
        int col = (int)(obj.transform.position.x - transform.position.x);
        // Destroy the block
        obj.GetComponent<BlockHover>().enabled = false;
        obj.GetComponent<Rigidbody>().useGravity = true;
        Destroy(m_Grid[row][col], 5f);
        m_Grid[row][col] = null;
        // Call destroy event
        OnBlockDestroy();
    }

    List<Transform> GetAllBlocks(Transform transformForSearch)
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform trans in transformForSearch.transform)
        {
            //Debug.Log (trans.name);
            if (trans.tag.ToString() == "Cube")
            {
                childs.Add(trans);
            }
            else
            {
                childs.AddRange(GetAllBlocks(trans));
            }
        }
        return childs;
    }
}
