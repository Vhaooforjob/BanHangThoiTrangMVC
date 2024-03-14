using System.ComponentModel.DataAnnotations;

public class ProductCategoryViewModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(150)]
    public string Title { get; set; }
    [Required]
    [StringLength(150)]
    public string Alias { get; set; }
    public string Description { get; set; }
    [StringLength(250)]
    public string Icon { get; set; }
    [StringLength(250)]
    public string SeoTitle { get; set; }
    [StringLength(500)]
    public string SeoDescription { get; set; }
    [StringLength(250)]
    public string SeoKeywords { get; set; }
}