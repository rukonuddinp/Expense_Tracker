using Expense_Tracker3.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker3.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
       
        [DisplayName("Category")]
        public int CategoryId_Fk { get; set; }
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [DisplayName("Expense Date")]
        public DateTime Date { get; set; }
        [Range(0, 5000)]
        public decimal Amount { get; set; }

        [ForeignKey("CategoryId_Fk")]
        
        public  Category Category { get; set; }
    }

    
   
}
