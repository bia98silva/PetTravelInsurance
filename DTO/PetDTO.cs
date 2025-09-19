namespace PetTravelInsurance.DTO
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public string Idade { get; set; } = string.Empty;
        public int TutorId { get; set; }

        
    }
}