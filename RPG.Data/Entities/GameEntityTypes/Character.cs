namespace RPG.Data.Entities.GameEntityTypes
{
    using System.ComponentModel.DataAnnotations;

    public class Character : GameEntity
    {

        [Key]
        public int Id { get; set; }

        public GameSession GameSession { get; set; } = null!;

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
