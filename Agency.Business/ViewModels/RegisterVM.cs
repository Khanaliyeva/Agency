using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }





        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Surname { get; set; }




        [Required]
        [MaxLength(20)]
        public string Username { get; set; }



        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [DataType(DataType.Password)]
        [MinLength(2)]
        public string Password { get; set; }



        [DataType(DataType.Password), Compare(nameof(Password))]
        [MinLength(2)]
        public string ConfirmPassword { get; set; }
    }


}
