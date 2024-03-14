using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BanHangThoiTrangMVC.HelperModels.Paging
{
    public class PagingModel<T> : PagingSizeModel
    {
        public T Filter { get; set; }
        public List<SortItems> Sorts { get; set; }
    }

    public class PagingSizeModel
    {
        [Required]
        public int Page { get; set; }
        [Required]
        public int Limit { get; set; }
    }

    public class SortItems
    {
        public string Column { get; set; }
        public bool IsDesc { get; set; }
    }
}