using System.ComponentModel.DataAnnotations;

namespace ExamCode.ViewModels
{
    public class AdminLoginVm
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}
