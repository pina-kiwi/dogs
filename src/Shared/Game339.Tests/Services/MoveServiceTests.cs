using System.Runtime.InteropServices;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;
using Game339.Tests.Infrastructure;

namespace Game339.Tests.Services;

public class MoveServiceTests
{
    private IGameLog _log;
    private I_MoveService _moveService;

    [SetUp]
    public void SetUp()
    {
        _log = EmptyGameLog.Instance;
        _moveService = new MoveService((10, 10), _log);
    }
    
    private static GridEntity CreateCharacter(Type characterType, [Optional] GridPosition position)
    {
        object[] parameters = characterType == typeof(PlayerDog) ? [ 10, 3, position ] : [ position.row, position.col ];
        return (Activator.CreateInstance(characterType, parameters) as GridEntity) ?? throw new InvalidOperationException();
    }

    [TestCase(0, 0)]
    [TestCase(9, 0)]
    [TestCase(0, 9)]
    [TestCase(9, 9)]
    public void IsValidPosition_Identifies_Valid_Positions(int row, int column)
    {
        Assert.That(_moveService.IsValidPosition(row, column), Is.True);
    }
    
    [TestCase(-1, -1)]
    [TestCase(-1, 9)]
    [TestCase(9, -1)]
    [TestCase(10, -1)]
    [TestCase(-1, 10)]
    [TestCase(10, 0)]
    [TestCase(0, 10)]
    [TestCase(10, 10)]
    public void IsValidPosition_Identifies_Invalid_Positions(int row, int column)
    {
        Assert.That(_moveService.IsValidPosition(row, column), Is.False);
    }
    
    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(10, -1)]
    [TestCase(-1, 9)]
    [TestCase(9, 9)]
    public void IsValidPosition_Is_Same_As_IsValidPosition(int row, int column)
    {
        bool signature1 = _moveService.IsValidPosition(row, column);
        bool signature2 = _moveService.IsValidPosition((row, column));
        
        Assert.That(signature1, Is.EqualTo(signature2));
    }
    
    [TestCase(typeof(SmallDog))]
    [TestCase(typeof(MediumDog))]
    [TestCase(typeof(LargeDog))]
    [TestCase(typeof(PlayerDog))]
    public void Move_Changes_Character_Location(Type characterType)
    {
        GridEntity gridEntity = CreateCharacter(characterType);

        GridPosition destination = (9, 9);
        _moveService.Move(gridEntity, destination);

        Assert.That(gridEntity.Position, Is.EqualTo(destination));
    }
    
    [Test]
    public void MoveRandom_Moves_Randomly()
    {
        GridPosition initialPosition = (4, 4);
        
        List<Type> characterTypes = [typeof(SmallDog), typeof(MediumDog), typeof(LargeDog), typeof(PlayerDog)];
        List<GridEntity> characters = characterTypes.Select(type => CreateCharacter(type, initialPosition)).ToList();

        for (int index = 0; index < characters.Count; index++)
            _moveService.MoveRandom(characters[index], (index+1)*3);

        int charactersNotAtOrigin = characters.Count(character => character.Position != initialPosition);
        int charactersInUniquePositions = characters.DistinctBy(character => character.Position).Count();
        
        Assert.Multiple(() =>
        {
            Assert.That(charactersNotAtOrigin, Is.GreaterThanOrEqualTo(characters.Count / 2));
            Assert.That(charactersInUniquePositions, Is.GreaterThanOrEqualTo(characters.Count / 2));
        });
    }

    [TestCase(0, 5)]
    [TestCase(5, 0)]
    [TestCase(5, 5)]
    public void Move_Takes_Correct_Num_Of_Steps(int row, int column)
    {
        GridEntity gridEntity = CreateCharacter(typeof(PlayerDog));
        
        int stepCount_expected = _CalculateStepCount(gridEntity.Position, (row, column));
        int stepCount_actual = 0;
        
        gridEntity.StepEvent += _IncrementStepCount;
        _moveService.Move(gridEntity, (row, column));
        gridEntity.StepEvent -= _IncrementStepCount;

        Assert.That(stepCount_actual, Is.EqualTo(stepCount_expected));

        int _CalculateStepCount(GridPosition startPosition, GridPosition endPosition)
        {
            int horizontal_stepsAway = Math.Abs(startPosition.col - endPosition.col);
            int vertical_stepsAway = Math.Abs(startPosition.row - endPosition.row);

            return horizontal_stepsAway + vertical_stepsAway;
        }
        
        void _IncrementStepCount(GridPosition newPosition) => stepCount_actual++;
    }
    
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    public void MoveRandom_Takes_Correct_Num_Of_Steps(int steps)
    {
        GridEntity gridEntity = CreateCharacter(typeof(MediumDog));
        
        int stepCount_expected = steps;
        int stepCount_actual = 0;
        
        gridEntity.StepEvent += _IncrementStepCount;
        _moveService.MoveRandom(gridEntity, steps);
        gridEntity.StepEvent -= _IncrementStepCount;

        Assert.That(stepCount_actual, Is.EqualTo(stepCount_expected));

        void _IncrementStepCount(GridPosition newPosition)
        { stepCount_actual++; }
    }

    [TestCase(0, -2)]
    [TestCase(10, 0)]
    [TestCase(-1, -1)]
    [TestCase(11, 11)]
    public void Move_Stays_In_Bounds(int row, int column)
    {
        GridEntity gridEntity = CreateCharacter(typeof(LargeDog));
        
        _moveService.Move(gridEntity, (row, column));
        GridPosition newPosition = gridEntity.Position;

        Assert.That(() => _moveService.IsValidPosition(newPosition), $"{newPosition} is not valid.");
    }

    [Test]
    public void MoveRandom_Stays_In_Bounds()
    {
        I_MoveService miniMoveService = new MoveService((1, 1), _log);
        GridEntity gridEntity = CreateCharacter(typeof(SmallDog));
        
        miniMoveService.MoveRandom(gridEntity, 10);
        GridPosition newPosition = gridEntity.Position;
        
        Assert.That(miniMoveService.IsValidPosition(newPosition), Is.True);
    }

    [Test]
    public void Move_To_Current_Position()
    {
        GridPosition initialPosition = (0, 0);
        GridEntity gridEntity = CreateCharacter(typeof(MediumDog), initialPosition);
        
        _moveService.Move(gridEntity, initialPosition);

        Assert.That(gridEntity.Position, Is.EqualTo(initialPosition));
    }

    [Test]
    public void MoveRandom_Take_No_Steps()
    {
        GridPosition initialPosition = (0, 0);
        GridEntity gridEntity = CreateCharacter(typeof(MediumDog), initialPosition);
        
        _moveService.MoveRandom(gridEntity, 0);

        Assert.That(gridEntity.Position, Is.EqualTo(initialPosition));
    }
    
    
    [TestCase(0, 0)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(-1, -1)]
    public void No_Space_Throws_ArgumentOutOfRangeException(int numRows, int numColumns)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MoveService((numRows, numColumns), _log));
    }
}