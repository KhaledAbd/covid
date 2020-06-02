using System.ComponentModel.DataAnnotations;

namespace Coid.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "اسم المستخد مطلوب"), StringLength(20,MinimumLength =4,ErrorMessage= "اسم المستخدم لا يصلح")]
        public string username { get; set; }
        
        [Required(ErrorMessage = "كلمة المرور مطلوبه"),StringLength(20,MinimumLength =4,ErrorMessage="كلمة المرور لا تصلح")]
        public string password {get; set;}
    }
}