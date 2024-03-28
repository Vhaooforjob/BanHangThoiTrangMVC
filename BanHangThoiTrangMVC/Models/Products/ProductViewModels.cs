using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

public class ProductViewModel
{
    public int Id { get; set; }
    [StringLength(250)]
    public string Title { get; set; }
    [StringLength(250)]
    public string Alias { get; set; }

    [StringLength(50)]
    public string ProductCode { get; set; }
    public string Description { get; set; }

    [AllowHtml]
    public string Detail { get; set; }

    [StringLength(250)]
    public string Image { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal Price { get; set; }
    public decimal? PriceSale { get; set; }
    public int Quantity { get; set; }
    public int ViewCount { get; set; }
    public bool IsHome { get; set; }
    public bool IsSale { get; set; }
    public bool IsFeature { get; set; }
    public bool IsHot { get; set; }
    public bool IsActive { get; set; }
    public int ProductCategoryId { get; set; }
    [StringLength(250)]
    public string SeoTitle { get; set; }
    [StringLength(500)]
    public string SeoDescription { get; set; }
    [StringLength(250)]
    public string SeoKeywords { get; set; }
    public DateTime CreateDate { get; set; }

    public virtual ProductCategoryViewModel ProductCategory { get; set; }
    public virtual List<ProductImageViewModel> ProductImages { get; set; }
    public virtual List<OrderDetailViewModel> OrderDetails { get; set; }
}