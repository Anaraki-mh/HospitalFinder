namespace HospitalFinder.API.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime TokenExpirationDateTime { get; set; }
    }
}
