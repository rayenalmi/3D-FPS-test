using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGanaration : MonoBehaviour
{
    public Transform NavMeshFloor;
    public Transform tilePrefab;
    public Transform ObsticolPrefab;
    public Transform navmeshmaskprefab;

    public Vector2 mapSize;
    public Vector2 MaxmapSize;

    public float tilesize;

    public int seed = 10;

    List<Coord> AlllistCoord;
    Queue<Coord> ShuflidTileCoords;
    Coord MapCenter;

    [Range(0,1)]
    public float outlinePercent;
    [Range(0, 1)]
    public float OpstaclePercent;
    public void GanarationMap()
    {
        AlllistCoord = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                AlllistCoord.Add(new Coord(x, y));
            }
        }

        ShuflidTileCoords = new Queue<Coord>(Utility.ShufulleArray(AlllistCoord.ToArray(),seed));
        MapCenter = new Coord(((int)(mapSize.x)) / 2, (int)(mapSize.y / 2));
        string holdername = "Generation Map";

        if(transform.Find (holdername))
        {
            DestroyImmediate(transform.Find(holdername).gameObject);
        }

        Transform mapHolder = new GameObject(holdername).transform;
        mapHolder.parent = transform;
        for (int x=0; x<mapSize.x; x++)
        {
            for(int y=0;y<mapSize.y; y++)
            {
                Vector3 tileposition = CoordToPosition(x, y);
                Transform newttile = Instantiate(tilePrefab, tileposition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newttile.localScale = Vector3.one * (1 - outlinePercent)* tilesize;
                newttile.parent = mapHolder;
            }
           
        }

        bool[,] obstacelMap = new bool[(int)mapSize.x, (int) mapSize.y];
        int currentOpstcalMap = 0;
        int NbObstacle =(int)( mapSize.x * mapSize.y * OpstaclePercent );
        for (int i = 0; i < NbObstacle; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacelMap[randomCoord.x, randomCoord.y] = true;
            currentOpstcalMap++;
            if (randomCoord != MapCenter && MapIsAccesele(obstacelMap, currentOpstcalMap))
            {
                Vector3 ObsatclePostion = CoordToPosition(randomCoord.x, randomCoord.y);
                Transform newObstacle = Instantiate(ObsticolPrefab, ObsatclePostion + Vector3.up * 0.5f, Quaternion.identity) as Transform;
                newObstacle.parent = mapHolder;
                newObstacle.localScale = Vector3.one * (1 - outlinePercent) * tilesize;
            }
            else
            {
                obstacelMap[randomCoord.x, randomCoord.y] = false;
                currentOpstcalMap --;
            }
        }


        Transform maskleft = Instantiate(navmeshmaskprefab, Vector3.left * (MaxmapSize.x + mapSize.x) / 4 * tilesize, Quaternion.identity) as Transform;
        maskleft.parent = mapHolder;
        maskleft.localPosition = new Vector3((MaxmapSize.x - mapSize.x) / 2, 1, mapSize.y) * tilesize ;
      


        NavMeshFloor.localScale = new Vector3(MaxmapSize.x, MaxmapSize.y) * tilesize;
    }

    bool MapIsAccesele(bool[,] obstacelMap , int currentOpstcalMap)
    {
        bool[,] mapFlafs = new bool[obstacelMap.GetLength(0),obstacelMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(MapCenter);
        mapFlafs[MapCenter.x, MapCenter.y] = true;

        int accessibleTileCount = 1;

        while(queue.Count >0 )
        {
            Coord tile = queue.Dequeue();

            for(int x=-1; x<=1; x++)
            {
                for(int y=-1; y<=1; y++)
                {
                    int NeighbourX = tile.x + x;
                    int NeighbourY = tile.y + y; 
                    if(x==0 || y==0)
                    {
                        if(NeighbourX >=0 && NeighbourX < obstacelMap.GetLength(0) && NeighbourY >=0 && NeighbourY < obstacelMap.GetLength(1))
                        {
                            if(! mapFlafs[NeighbourX,NeighbourY] && ! obstacelMap[NeighbourX, NeighbourY])
                            {
                                mapFlafs[NeighbourX, NeighbourY] = true;
                                queue.Enqueue(new Coord(NeighbourX, NeighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }

            }
        }

        int targetaccessibleTileCount = (int)(mapSize.x * mapSize.y - currentOpstcalMap);
        return targetaccessibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y) * tilesize;
    }

    public Coord GetRandomCoord ()
    {
        
        Coord randomcoord = ShuflidTileCoords.Dequeue ();
        ShuflidTileCoords.Enqueue(randomcoord);
        return randomcoord;

    }




    public struct Coord 
        {
        public int x;
        public int y;
        public Coord (int _x , int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        GanarationMap(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
