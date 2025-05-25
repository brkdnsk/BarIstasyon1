using System.ComponentModel.DataAnnotations;

public class CreateEquipmentDto
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Ekipman adı gereklidir.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Açıklama gereklidir.")]
    public string Description { get; set; } = null!;

    public int CoffeeRecipeId { get; set; }

    [Required(ErrorMessage = "Görsel URL gereklidir.")]
    public string ImageUrl { get; set; } = null!;

    [Required(ErrorMessage = "Kullanım açıklaması gereklidir.")]
    public string Usage { get; set; } = null!;
}
