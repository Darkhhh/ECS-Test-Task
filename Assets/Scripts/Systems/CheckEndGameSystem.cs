using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public sealed class CheckEndGameSystem : ReactiveSystem<GameEntity> {
    private readonly GameContext _context;
    private readonly GameObject _endGamePanel;
    private readonly Text _endGameText;

    public CheckEndGameSystem(Contexts contexts, GameObject endGamePanel, Text endGameText) : base(contexts.game) {
        _context = contexts.game;
        _endGamePanel = endGamePanel;
        _endGameText = endGameText;
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
        if (modelEntity.collectedIngots.value == modelEntity.winGoldNumber.value)
        {
            _endGamePanel.SetActive(true);
            _endGameText.text = "Nice, you've found all gold!";
        }

        if (modelEntity.shovels.value < 1)
        {
            _endGamePanel.SetActive(true);
            _endGameText.text = "Sorry, you're out of shovels!";
        }
    }
}