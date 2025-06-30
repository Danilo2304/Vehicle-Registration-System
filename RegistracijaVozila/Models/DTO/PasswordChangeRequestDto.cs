namespace RegistracijaVozila.Models.DTO
{
    public class PasswordChangeRequestDto
    {
        public string Id { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }


    }
}
