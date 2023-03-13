using System.ComponentModel.DataAnnotations;

public class AddMenu{
    [Key]
    public int Id {get;set;}
       [Required]
        [MaxLength(100)]
    public string? DishName {get;set;}
     [Required]
        [MaxLength(100)]
    public string? Category {get;set;}
 [Required]
[MaxLength(100)]
    public string? Type {get;set;}
    public string? Image {get;set;}
     [Required]
         [Range(1,100000, ErrorMessage ="Price must be between 1-1000.")]
    public int? Price {get;set;}
     [Required]
        [MaxLength(100)]
    public string? Quantity {get;set;}
}

public enum cate
{
    veg,
    nonveg    
}