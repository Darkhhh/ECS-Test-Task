using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class CreateTilesSystem : IInitializeSystem, ICleanupSystem
{
    private readonly Contexts _context;
    private readonly Sprite _sprite;
    private readonly Transform _parent;
    private readonly int _tilesInRow;
    private readonly float _goldProbability;
    private readonly int _defaultDepth;

    public List<Vector3> GoldPositions { get; private set; }
    public CreateTilesSystem(Contexts contexts, Sprite sprite, Transform parent, int tilesInRow, float goldProbability, int defaultDepth)
    {
        _context = contexts;
        _sprite = sprite;
        _parent = parent;
        _tilesInRow = tilesInRow;
        _goldProbability = goldProbability;
        _defaultDepth = defaultDepth;
    }

    public void Initialize()
    {
        GoldPositions = new List<Vector3>();
        var startValue = (float)_tilesInRow / 2 - 0.5f * ((_tilesInRow + 1) % 2);
        float currentX = -startValue, currentY = startValue;
        for(var i = 0; i < _tilesInRow; i++){
            for(var j = 0; j < _tilesInRow; j++)
            {
                var position = new Vector3(currentX, currentY, 0);
                var goldDepth = -1;
                if (Random.Range(0f, 1f) < _goldProbability)
                {
                    goldDepth = Random.Range(0, _defaultDepth);
                    GoldPositions.Add(position);
                }
                CreateTileEntity(position, goldDepth, _defaultDepth);
                currentX++;
            }
            currentX = -startValue;
            currentY--;
        }
    }

    private void CreateTileEntity(Vector3 position, int goldDepth, int depth)
    {
        var entity = _context.game.CreateEntity();
        entity.isTile = true;
        entity.AddPosition(position);
        entity.AddDepth(depth);
        entity.AddIngotDepth(goldDepth);
        entity.AddOpacity(1f);
        
        var gameObject = new GameObject("TileClone");
        gameObject.transform.SetParent(_parent,false);
        var renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = _sprite;
        gameObject.transform.position = entity.position.value;
        
        entity.AddView(gameObject);
        gameObject.Link(entity);
    }

    public void Cleanup()
    {
        var entities = _context.game.GetGroup(GameMatcher.Tile).GetEntities();
        foreach (var entity in entities)
        {
            entity.view.gameObject.Unlink();
            entity.RemoveView();
        }
    }
}