using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangThoiTrangMVC.Models.EF
{
    [Table("tb_News")]
    public class News : CommonAbstract
    {
        public News() { }

        public News(string title, string alias, string description, string detail, string image, int categoryId, string seoTitle,
        string seoDescription, string seoKeywords, bool isActive, string createBy, DateTime createDate, DateTime modifiedDate, string modifiedBy) : base(createBy, createDate, modifiedDate, modifiedBy)
        {
            Title = title;
            Alias = alias;
            Description = description;
            Detail = detail;
            Image = image;
            CategoryId = categoryId;
            SeoTitle = seoTitle;
            SeoDescription = seoDescription;
            SeoKeywords = seoKeywords;
            IsActive = isActive;
        }

        public News(string createBy, DateTime createDate, DateTime modifiedDate, string modifiedBy) : base(createBy, createDate, modifiedDate, modifiedBy)
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không để trống tiêu đề tin")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }

        public override CommonAbstract Clone()
        {
            return (News)MemberwiseClone();
        }

    }
}