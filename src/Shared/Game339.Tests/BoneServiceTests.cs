using Game339.Shared.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

namespace Game339.Tests;

public class BoneServiceTests
{
    private IGameLog _log;
    private I_BoneService _boneService;

    private static GameState NewGame(int startingBones)
    {
        PlayerDog player = new(0, 0);
        
        List<EnemyDog> enemyDogs = [
            new SmallDog(0, 0),
            new MediumDog(0, 0),
            new LargeDog(0, 0)
        ];

        return new GameState(player, enemyDogs, startingBones);
    }

    private void ResetService(int startingBones = 3, int maxBones = 10) => _boneService = new BoneService(maxBones, NewGame(startingBones), _log);

    [SetUp]
    public void SetUp()
    {
        _log = EmptyGameLog.Instance;
        ResetService();
    }

    [Test]
    public void BoneCount_Gets_GameState_PlayerBones_Value()
    {
        GameState game = NewGame(5);
        I_BoneService temp_boneService = new BoneService(10, game, _log);
        
        game.PlayerBones.Value += 4;
        game.PlayerBones.Value -= 7;

        Assert.That(temp_boneService.BoneCount, Is.EqualTo(game.PlayerBones.Value));
    }
    
    [TestCase(10)]
    [TestCase(-8)]
    public void CalculateBones_Stays_Within_Range(int changeAmount)
    {
        ResetService(5);

        int remainingBones = _boneService.CalculateBones(changeAmount);

        Assert.That(remainingBones, Is.LessThanOrEqualTo(10).And.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void AddBone_Adds_One_To_BoneCount()
    {
        ResetService();

        int initialBones = _boneService.BoneCount;
        _boneService.AddBone();

        Assert.That(_boneService.BoneCount, Is.EqualTo(initialBones+1));
    }

    [Test]
    public void StealBones_Reduces_BoneCount_By_Specified_Amount()
    {
        int initialBones = 8;
        ResetService(initialBones);

        int bonesToLose = 4;
        _boneService.StealBones("Nobody", bonesToLose);

        Assert.That(_boneService.BoneCount, Is.EqualTo(initialBones - bonesToLose));
    }
    
    [Test]
    public void StealBones_Reduces_BoneCount_By_Attacker_Damage()
    {
        ResetService(8);

        _boneService.StealBones(new LargeDog(0, 0));

        Assert.That(_boneService.BoneCount, Is.EqualTo(3));
    }
}
