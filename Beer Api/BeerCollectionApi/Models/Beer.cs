using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Beer
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public List<int> Ratings { get; set; } = new List<int>();
    public double AverageRating { get; set; }

}

public class CreateBeer
{

    public string Name { get; set; }
    public string Type { get; set; }
    public int Rating { get; set; } = 0;
    
}
