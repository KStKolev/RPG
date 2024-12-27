namespace RPG.Data.Entities.Interfaces
{
    public interface IGameEntity
    {

        int Strength { get; set; }

        int Agility { get; set; }

        int Intelligence { get; set; }

        int Range { get; set; }

        char CharacterSymbol { get; set; }

        int Health { get; set; }

        int Mana { get; set; }

        int Damage { get; set; }

        int FieldRow { get; set; }

        int FieldColumn { get; set; }

        void Setup();

    }
}
