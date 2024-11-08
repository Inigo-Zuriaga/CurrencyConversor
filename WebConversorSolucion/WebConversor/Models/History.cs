
using System.ComponentModel.DataAnnotations;

namespace WebConversor.Models
{
    
    [Table("ExchangeHistory")]
    public class History
    {
        [Key]
        public int Id { get; set; }

        // Declaramos una variable User la cual sera de tipo User,
        //esta sera la variable que podremos estar usando para mostrar cualquier dato de la tabla User
        [Required]
        [ForeignKey("User")]
        //Declaramos una variable UserId la cual pertenece a la tabla User,
        //de esta manera logramos indicar la relacion entre las tablas mediante el Id
        public int UserId { get; set; }
        
        public User User { get; set; }
        [Required]
        public string FromCoin { get; set; }
        [Required]
        public string ToCoin { get; set; }
        // public double Result { get; set; }
        [Required]
        public DateTime Date { get; set; }=DateTime.Now;

}
