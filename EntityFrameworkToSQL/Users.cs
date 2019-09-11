using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkToSQL
{
    public partial class Users
    {
        public Users()
        {
            Requests = new HashSet<Requests>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(30)]
        public string Lastname { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        public bool? IsReviewer { get; set; }
        public bool? IsAdmin { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Requests> Requests { get; set; }
    }
}
