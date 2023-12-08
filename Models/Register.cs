﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Models
{
    public class Register
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        public string PasswordAgain { get; set; }

        public virtual Login Logins { get; set; }



    }
}
