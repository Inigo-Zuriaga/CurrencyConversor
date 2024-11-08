namespace WebConversor.Models;

public class History
{

    public int Id { get; set; }

    // Declaramos una variable User la cual sera de tipo User,
    //esta sera la variable que podremos estar usando para mostrar cualquier dato de la tabla User
    public User User { get; set; }

    //Declaramos una variable UserId la cual pertenece a la tabla User,
    //de esta manera logramos indicar la relacion entre las tablas mediante el Id
    public int UserId { get; set; }
    public string FromCoin { get; set; }
    public string ToCoin { get; set; }
    public double Result { get; set; }
    public DateTime Date { get; set; }

}
