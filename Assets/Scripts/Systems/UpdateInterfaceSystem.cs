using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine.UI;

public sealed class UpdateInterfaceSystem : ReactiveSystem<GameEntity> {
    private readonly GameContext _context;
    private readonly Text _shovelsNumber;
    private readonly Text _goldNumber;

    public UpdateInterfaceSystem(Contexts contexts, Text shovelsNumber, Text goldNumber) : base(contexts.game) {
        _context = contexts.game;
        _shovelsNumber = shovelsNumber;
        _goldNumber = goldNumber;
        InitializeInterface();
    }

    private void InitializeInterface()
    {
        var modelEntity = _context.GetGroup(GameMatcher.Model).GetEntities().FirstOrDefault();
        if (modelEntity is null) throw new Exception("Can not access ModelEntity");
        _shovelsNumber.text = $"Shovels: {modelEntity.shovels.value}/{modelEntity.defaultShovelsNumber.value}";
        _goldNumber.text = $"Gold: {modelEntity.collectedIngots.value}/{modelEntity.winGoldNumber.value}";
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Shovels, GameMatcher.CollectedIngots));
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isModel;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var modelEntity = entities.FirstOrDefault();
        if (modelEntity is null) throw new Exception("Can not access ModelEntity");
        _shovelsNumber.text = $"Shovels: {modelEntity.shovels.value}/{modelEntity.defaultShovelsNumber.value}";
        _goldNumber.text = $"Gold: {modelEntity.collectedIngots.value}/{modelEntity.winGoldNumber.value}";
    }
}