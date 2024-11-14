// using Microsoft.AspNetCore.Identity;

namespace WebConversor.Models;
//
// [Table("User")] //corresponde con la tabla de la base de datos: User
// public class User 
// {
//     [Key]  // ID del usuario, clave primaria de la tabla en la bbdd
//     public int Id { get; set; }
//
//     // Nombre del cliente
//     [Required(ErrorMessage = "El nombre es obligatorio")]
//     [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")] // Limita la longitud del nombre
//     public string Name { get; set; } = string.Empty; // Establece un valor por defecto vacío
//
//     // Apellido del cliente
//     [Required(ErrorMessage = "El apellido es obligatorio")] 
//     [StringLength(500, ErrorMessage = "El apellido no puede tener más de 500 caracteres")] // Limita la longitud del apellido
//     public string LastName { get; set; }
//
//     // Correo electrónico del cliente
//     [Required(ErrorMessage = "El correo electrónico es obligatorio")]
//     [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido")] // Valida que sea un correo electrónico válido
//     [StringLength(150, ErrorMessage = "El correo electrónico no puede tener más de 150 caracteres")] // Limita la longitud del correo
//     public string Email { get; set; } = string.Empty;
//
//     // Fecha de nacimiento del cliente
//     [DataType(DataType.Date)] 
//     public DateTime? FechaNacimiento { get; set; }
//
//     // Identificador del usuario de ASP.NET Core Identity asociado al cliente
//     // public string? IdentityUserId { get; set; }
//
//     // Contraseña del usuario (se recomienda no guardar contraseñas como texto plano)
//     [Required(ErrorMessage = "La contraseña es obligatoria")]
//     [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
//     public string Password { get; set; } = string.Empty;
//
//     // Imagen del perfil (URL o ruta del archivo de imagen)
//     [StringLength(500, ErrorMessage = "La URL de la imagen no puede tener más de 500 caracteres")]
//     public string? Img { get; set; } // Opcional: campo para almacenar la URL de la imagen del perfil del usuario
// }

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string Password { get; set; } = string.Empty;
    public string? Img { get; set; } = string.Empty;
}
