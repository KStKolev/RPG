namespace RPG.Data.Entities
{
    using RPG.Data.Entities.Interfaces;
    using RPG.Utilities.DataConstants.EntityConstants;
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

        public void Setup()
        {
            const int STRENGTH_MULTIPLIER = StatConstants.STRENGTH_SETUP_MULTIPLIER;
            const int INTELLIGENCE_MULTIPLIER = StatConstants.INTELLIGENCE_SETUP_MULTIPLIER;
            const int AGILITY_MULTIPLIER = StatConstants.AGILITY_SETUP_MULTIPLIER;

            this.Health = this.Strength * STRENGTH_MULTIPLIER;
            this.Mana = this.Intelligence * INTELLIGENCE_MULTIPLIER;
            this.Damage = this.Agility * AGILITY_MULTIPLIER;
        }

    }
}
