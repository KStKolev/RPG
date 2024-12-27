namespace RPG.Data.Entities
{
    using RPG.Data.Entities.Interfaces;
    using System.ComponentModel.DataAnnotations.Schema;

    public class GameEntity : IGameEntity
    {

        public int Strength { get; set; }

        public int Agility { get; set; }

        public int Intelligence { get; set; }

        public int Range { get; set; }

        public char CharacterSymbol { get; set; }

        public int Health { get; set; }

        public int Mana { get; set; }

        public int Damage { get; set; }

        [NotMapped]
        public int FieldRow { get; set; }

        [NotMapped]
        public int FieldColumn { get; set; }

        public virtual void Setup()
        {
            this.Health = this.Strength * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;
        }

    }
}
