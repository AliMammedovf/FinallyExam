using System.ComponentModel.DataAnnotations;

namespace ExamCode.ViewModels
{
    public class UserRegisterVm
    {

        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
