using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{ 
    public static GridManager Instance; 
    [SerializeField] private int _width, _height; 
    [SerializeField] private Transform _cam; 
    [SerializeField] private Tile _grassTile, _groundTile;
    
    private Dictionary<Vector2, Tile> _tiles;
    
    private GameObject terrainGrid;

    protected Graph graph;
    protected Dictionary<Team, int> startPositionPerTeam;
    
    List<Tile> allTiles = new List<Tile>();

   protected void Awake()
   {
       Instance = this;
       GenerateGrid();
       allTiles = terrainGrid.GetComponentsInChildren<Tile>().ToList();
       InitializeGraph(); 
       startPositionPerTeam = new Dictionary<Team, int>(); 
       startPositionPerTeam.Add(Team.Heroes, 0); 
       startPositionPerTeam.Add(Team.Enemies, graph.Nodes.Count -1);
    }

   /*public Node GetFreeNode(Team forTeam)
    {
        int startIndex = startPositionPerTeam[forTeam];
        int currentIndex = startIndex;

        while(graph.Nodes[currentIndex].IsOccupied)
        {
            if(startIndex == 0)
            {
                currentIndex++;
                if (currentIndex == graph.Nodes.Count)
                    return null;
            }
            else
            {
                currentIndex--;
                if (currentIndex == -1)
                    return null;
            }
            
        }
        return graph.Nodes[currentIndex];
    }

   public List<Node> GetPath(Node from, Node to)
   { 
       return graph.GetShortestPath(from, to);
   }
   
   public List<Node> GetNodesCloseTo(Node to) 
   { 
       return graph.Neighbors(to);
   }

    public Node GetNodeForTile(Tile t)
    {
        var allNodes = graph.Nodes;

        for (int i = 0; i < allNodes.Count; i++)
        {
            if (t.transform.GetSiblingIndex() == allNodes[i].index)
            {
                return allNodes[i];
            }
        }

        return null;
    }*/
    
    private void InitializeGraph()
    {
        graph = new Graph();
        for (int i = 0; i < allTiles.Count; i++)
        {
            Vector3 place = allTiles[i].transform.position;
            graph.AddNode(place);
        }

        var allNodes = graph.Nodes;
        foreach (Node from in allNodes)
        {
            foreach (Node to in allNodes)
            {
                if (Vector3.Distance(from.worldPosition, to.worldPosition) < 1f && from != to)
                {
                    graph.AddEdge(from, to);
                }
            }
        }
    }
    public void GenerateGrid() 
    { 
        _tiles = new Dictionary<Vector2, Tile>(); 
        for (int x = 0; x < _width; x++) 
        { 
            for (int y = 0; y < _height; y++) 
            { 
                var randomTile = Random.Range(0, 6) == 3 ? _groundTile : _grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x,y), Quaternion.identity); 
                spawnedTile.name = $"Tile {x} {y}"; 
                spawnedTile.Init(x,y); 
                _tiles[new Vector2(x, y)] = spawnedTile;
                terrainGrid = spawnedTile.gameObject;
            }
        }
        _cam.transform.position = new  Vector3((float)_width/2-0.5f,(float)_height/2-0.5f,-10); 
        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroSpawnTile() 
    { 
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    
    public Tile GetEnemySpawnTile() 
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
}
