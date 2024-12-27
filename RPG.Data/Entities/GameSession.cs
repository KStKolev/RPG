namespace RPG.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using RPG.Data.Entities.GameEntityTypes;

    public class GameSession
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }

        public Character Character { get; set; } = null!;

        public int MonsterKillCount { get; set; }

    }
}
