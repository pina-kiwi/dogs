using Game339.Shared.Models;

public enum DogSize
{
    Small,  // HP: 14, ATK: 7, SPD: 10
    Medium, // HP: 20, ATK: 6, SPD: 5
    Large   // HP: 18, ATK: 8, SPD: 2
}


public class DogCard : Character
{
    public DogSize Size { get; private set; }
    
    public DogCard(DogSize size)
    {
        Size = size;
        
        switch (size)
        {
            case DogSize.Small:
                Name.Value = "Small Dog";
                MaxHealth.Value = 14;
                Damage.Value = 7;
                Speed.Value = 8;
                break;
            
            case DogSize.Medium:
                Name.Value = "Medium Dog";
                MaxHealth.Value = 20;
                Damage.Value = 6;
                Speed.Value = 5;
                break;
            
            case DogSize.Large:
                Name.Value = "Large Dog";
                MaxHealth.Value = 18;
                Damage.Value = 8;
                Speed.Value = 2;
                break;
        }
        Health.Value = MaxHealth.Value;
    }

    public void SetCharacter(Character character)
    {
        character.Name.Value = Name.Value;
        character.Health.Value = Health.Value;
        character.MaxHealth.Value = MaxHealth.Value;
        character.Damage.Value = Damage.Value;
        character.Speed.Value = Speed.Value;
    }
}