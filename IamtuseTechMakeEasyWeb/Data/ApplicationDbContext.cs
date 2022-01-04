using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IamtuseTechMakeEasyWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(maximumLength:250)]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 250)]
        public string LastName { get; set; }
        [StringLength(maximumLength: 250)]
        public string Address1 { get; set; }
        [StringLength(maximumLength: 250)]
        public string Address2 { get; set; }
        [StringLength(maximumLength: 50)]
        public string PostCode { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
