using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    #region Serialized Fields

    [Header("Setup")]
    public Camera gameCamera;

    [SerializeField] private Text shovelsNumberText;
    [SerializeField] private Text goldNumberText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Text endGameText;
    
    [Header("Tile Parameters")]
    [SerializeField][Range(1, 10)] private int tilesInRow;
    [SerializeField][Range(0f, 1f)] private float goldProbability;
    [SerializeField] private int defaultDepth;

    [Header("Game Parameters")] 
    [SerializeField] private int shovelsNumber;
    [SerializeField] private int winGoldNumber;

    #endregion


    #region Private Values

    private CreateTilesSystem _createTilesSystem;
    private CreateIngotsSystem _createIngotsSystem;
    private Systems _executeSystems;

    #endregion
    
    
    private void Start()
    {
        var tilesParent = new GameObject("Tiles");
        var ingotsParent = new GameObject("Ingots");
        var contexts = Contexts.sharedInstance;
        var squareSprite = Resources.Load<Sprite>("Square");

        var modelEntity = contexts.game.CreateEntity();
        modelEntity.isModel = true;
        modelEntity.AddShovels(shovelsNumber);
        modelEntity.AddDefaultShovelsNumber(shovelsNumber);
        modelEntity.AddCollectedIngots(0);
        modelEntity.AddWinGoldNumber(winGoldNumber);
        modelEntity.AddDefaultDepth(defaultDepth);
        
        
        
        _createTilesSystem = new CreateTilesSystem(
                contexts, 
                squareSprite, 
                tilesParent.transform, 
                tilesInRow, 
                goldProbability, 
                defaultDepth);
        _createTilesSystem.Initialize();

        _createIngotsSystem = new CreateIngotsSystem(
            contexts, 
            squareSprite, 
            ingotsParent.transform,
            _createTilesSystem.GoldPositions);
        _createIngotsSystem.Initialize();
        
        _executeSystems = CreateExecuteSystems(contexts);
    }
    
    Systems CreateExecuteSystems(Contexts contexts) {
        return new Feature("Systems")
            .Add(new MouseInputSystem(contexts, gameCamera))
            .Add(new DigTileSystem(contexts))
            .Add(new ChangeOpacitySystem(contexts))
            .Add(new ActivateIngotSystem(contexts))
            .Add(new RemoveIngotSystem(contexts))
            .Add(new RemoveClickComponentSystem(contexts))
            .Add(new UpdateInterfaceSystem(contexts, shovelsNumberText, goldNumberText))
            .Add(new CheckEndGameSystem(contexts, endGamePanel, endGameText))
            .Add(new RestartGameSystem(contexts));
    }

    private void Update()
    {
        _executeSystems.Execute();
    }

    private void OnDestroy()
    {
        _createTilesSystem.Cleanup();
        _createIngotsSystem.Cleanup();
    }
}
