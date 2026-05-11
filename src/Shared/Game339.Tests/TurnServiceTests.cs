using Game339.Shared.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

namespace Game339.Tests;

[TestFixture]
public class TurnServiceTests
{
    private IGameLog _log;
    private I_TurnService _turnService;

    private static GameState NewGame()
    {
        PlayerDog player = new(0, 0);
        
        List<EnemyDog> enemyDogs = [
            new SmallDog(0, 0),
            new MediumDog(0, 0),
            new LargeDog(0, 0)
        ];

        return new GameState(player, enemyDogs, 3);
    }

    private void ResetService() => _turnService = new TurnService(NewGame(), _log);

    [SetUp]
    public void SetUp()
    {
        _log = EmptyGameLog.Instance;
        ResetService();
        _turnService.GetCharacterOnMove();
    }
    
    [Test, Order(1)]
    public void ARE_YOU_THERE()
    {
        ResetService();
        Assert.That(true);
    }
    
    [Test, Order(2)]
    public void ARE_WE_CONNECTED()
    {
        ResetService();
        Assert.That(true);
    }
    
    [Test, Order(3)]
    public void ___()
    {
        ResetService();
        Assert.That(true);
    }
    
    [Test, Order(4)]
    public void EXCELLENT()
    {
        ResetService();
        Assert.That(true);
    }
    
    [Test, Order(5)]
    public void TRULY_EXCELLENT()
    {
        ResetService();
        Assert.That(true);
    }
}