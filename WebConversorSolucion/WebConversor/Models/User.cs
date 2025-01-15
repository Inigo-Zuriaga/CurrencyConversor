namespace WebConversor.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string Password { get; set; } = string.Empty;
    public string? Img { get; set; } 
}
