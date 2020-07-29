using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Audit.Models
{
    public class LoadFiles
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int FileCategory { get; set; }

        [StringLength(100)]
        //[Required(ErrorMessage = "Please chose at least one file.")]
        public string FileName { get; set; }
    }
}