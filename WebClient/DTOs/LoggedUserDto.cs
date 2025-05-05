namespace WebClient.Dtos
{
    public class LoggedUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CustomerId { get; set; }
    }
}
