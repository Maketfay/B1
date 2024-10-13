using System.ComponentModel.DataAnnotations.Schema;

namespace SqlModels.Entities;

public class RandomFile
{
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required DateTime Date { get; set; }
    
    public required string LatinWord { get; set; }
    
    public required string CyrillicWord { get; set; }
    
    public required int RandomInteger { get; set; }
    
    public required decimal RandomDouble { get; set; }
}