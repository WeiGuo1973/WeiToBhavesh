using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Audit.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter Organization/Company Name.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter Email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter phone number.")]

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number. Phone number should be 10 digit number.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter contact person name.")]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Display(Name = "Application Date")]
        public DateTime LoadDate { get; set; }

        [Required(ErrorMessage = "Please select the physical year")]
        [Display(Name = "Fiscal Year")]
        public int Physicalyear { get; set; }

        [Required(ErrorMessage = "Please select the revenue range.")]
        [Display(Name = "Revenue")]
        public RevenueRange? Revenue { get; set; }

        public Status Status { get; set; }

        public ICollection<LoadFiles> LoadFiles  { get; set; }


    }


    public enum Status
    {
        [Display(Name = "Awaiting Review")]
        AwaitingReview=1,
        [Display(Name = "Under Review")]
        UnderReview=2,
        [Display(Name = "Reviewed")]
        Reviewed=3
    }

    public enum RevenueRange
    {
        [Display(Name = "0~10,000.00")]
        RevenueRange1=1,
        [Display(Name = "10,001.00~50,000.00")]
        RevenueRange2=2,
        [Display(Name = "50,001.00~100,000.00")]
        RevenueRange3=3,
        [Display(Name = "100,001.00~150,000.00")]
        RevenueRange4=4,
        [Display(Name = "150,001.00~200,000.00")]
        RevenueRange5=5
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
        }
    }

}