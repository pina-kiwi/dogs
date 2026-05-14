using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;
using Game339.Tests.Infrastructure;

namespace Game339.Tests.Services;

public class BoneServiceTests
{
    private IGameLog _log;
    private I_BoneService _boneService;
    
    private PlayerDog NewPlayer(int maxBones, int startingBones) => new(maxBones, startingBones);
    private void ResetService(int maxBones = 10, int startingBones = 3) => _boneService = new BoneService(NewPlayer(maxBones, startingBones), _log);

    [SetUp]
    public void SetUp()
    {
        _log = EmptyGameLog.Instance;
        ResetService();
    }

    [Test]
    public void BoneCount_Gets_GameState_PlayerBones_Value()
    {
        PlayerDog player = NewPlayer(10, 5);
        I_BoneService temp_boneService = new BoneService(player, _log);
        
        player.NumBones.Value += 4;
        player.NumBones.Value -= 7;

        Assert.That(temp_boneService.BoneCount, Is.EqualTo(player.NumBones.Value));
    }
    
    [TestCase(10)]
    [TestCase(-8)]
    public void CalculateBones_Stays_Within_Range(int changeAmount)
    {
        ResetService(5, 3);

        int remainingBones = _boneService.CalculateBones(changeAmount);

        Assert.That(remainingBones, Is.LessThanOrEqualTo(5).And.GreaterThanOrEqualTo(0));
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
        ResetService(initialBones, initialBones);

        MediumDog attacker = new(0, 0);
        _boneService.StealBones(attacker);

        Assert.That(_boneService.BoneCount, Is.EqualTo(initialBones - attacker.BonesToSteal));
    }
    
    [Test]
    public void StealBones_Reduces_BoneCount_By_Attacker_Damage()
    {
        ResetService(10, 8);

        _boneService.StealBones(new LargeDog(0, 0));

        Assert.That(_boneService.BoneCount, Is.EqualTo(3));
    }
}
