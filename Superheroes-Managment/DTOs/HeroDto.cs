namespace Superheroes_Managment.DTOs
{
    public class HeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Alias { get; set; }

        public int PowersCount { get; set; }
    }
}
