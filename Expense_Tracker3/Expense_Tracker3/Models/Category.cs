using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker3.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Please Enter CategoryName")]
        [Display(Name ="Category Name")]
        public string Name { get; set; }
        public virtual IList<Expense> Expenses { get; set; }
    }
}
