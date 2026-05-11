using System.Runtime.InteropServices;
using Game339.Shared;
using Game339.Shared.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

namespace Game339.Tests;

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
    
    private static Character CreateCharacter(Type characterType, [Optional] GridPosition position)
    {
        object[] parameters = [ position.row, position.col ];
        return (Activator.CreateInstance(characterType, parameters) as Character)!;
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
        Character character = CreateCharacter(characterType);

        GridPosition destination = (9, 9);
        _moveService.Move(character, destination);

        Assert.That(character.Position, Is.EqualTo(destination));
    }
    
    [Test]
    public void MoveRandom_Moves_Randomly()
    {
        GridPosition initialPosition = (4, 4);
        
        List<Type> characterTypes = [typeof(SmallDog), typeof(MediumDog), typeof(LargeDog), typeof(PlayerDog)];
        List<Character> characters = characterTypes.Select(type => CreateCharacter(type, initialPosition)).ToList();

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
        Character character = CreateCharacter(typeof(PlayerDog));
        
        int stepCount_expected = _CalculateStepCount(character.Position, (row, column));
        int stepCount_actual = 0;
        
        character.MoveEvent += _IncrementStepCount;
        _moveService.Move(character, (row, column));
        character.MoveEvent -= _IncrementStepCount;

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
        Character character = CreateCharacter(typeof(MediumDog));
        
        int stepCount_expected = steps;
        int stepCount_actual = 0;
        
        character.MoveEvent += _IncrementStepCount;
        _moveService.MoveRandom(character, steps);
        character.MoveEvent -= _IncrementStepCount;

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
        Character character = CreateCharacter(typeof(LargeDog));
        
        _moveService.Move(character, (row, column));
        GridPosition newPosition = character.Position;

        Assert.That(() => _moveService.IsValidPosition(newPosition), $"{newPosition} is not valid.");
    }

    [Test]
    public void MoveRandom_Stays_In_Bounds()
    {
        I_MoveService miniMoveService = new MoveService((1, 1), _log);
        Character character = CreateCharacter(typeof(SmallDog));
        
        miniMoveService.MoveRandom(character, 10);
        GridPosition newPosition = character.Position;
        
        Assert.That(miniMoveService.IsValidPosition(newPosition), Is.True);
    }

    [Test]
    public void Move_To_Current_Position()
    {
        GridPosition initialPosition = (0, 0);
        Character character = CreateCharacter(typeof(MediumDog), initialPosition);
        
        _moveService.Move(character, initialPosition);

        Assert.That(character.Position, Is.EqualTo(initialPosition));
    }

    [Test]
    public void MoveRandom_Take_No_Steps()
    {
        GridPosition initialPosition = (0, 0);
        Character character = CreateCharacter(typeof(MediumDog), initialPosition);
        
        _moveService.MoveRandom(character, 0);

        Assert.That(character.Position, Is.EqualTo(initialPosition));
    }
}