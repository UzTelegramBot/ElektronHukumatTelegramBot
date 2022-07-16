using Business.Helpers;
using Business.Helpers.ValidationAttributes;
using Domains;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.ModelDTO
{
    public class ManagerForCreationDTO
    {
        [Required]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password {    get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password is not valid. Please enter same password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public RoleManager Role { get; set; }
        [IsRoleOrganizator("Role")]
        public Guid? OrganizationId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        public Guid Creaetedby { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
