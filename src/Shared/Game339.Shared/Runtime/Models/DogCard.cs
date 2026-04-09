using Game339.Shared;
using Game339.Shared.Models;

public enum DogSize
{
    Small,  // HP: 14, ATK: 7, SPD: 10
    Medium, // HP: 20, ATK: 6, SPD: 5
    Large   // HP: 18, ATK: 8, SPD: 2
}


public class DogCard
{
    public DogSize Size;
    
    public ObservableValue<string> Name = new();
    
    public DogCard(DogSize size, Character owner = null)
    {
        Size = size;
        owner ??= new();
        
        SetOwner(owner);
    }

    public void SetOwner(Character owner)
    {
        owner.Dog = this;
        
        switch (Size)
        {
            case DogSize.Small:
                Name.Value = "Small Dog";
                owner.MaxHealth.Value = 14;
                owner.AttackPower.Value = 7;
                owner.Speed.Value = 8;
                break;
            
            case DogSize.Medium:
                Name.Value = "Medium Dog";
                owner.MaxHealth.Value = 20;
                owner.AttackPower.Value = 6;
                owner.Speed.Value = 5;
                break;
            
            case DogSize.Large:
                Name.Value = "Large Dog";
                owner.MaxHealth.Value = 18;
                owner.AttackPower.Value = 8;
                owner.Speed.Value = 2;
                break;
        }
    }
}