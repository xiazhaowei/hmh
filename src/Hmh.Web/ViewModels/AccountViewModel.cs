using Hmh.Core.Identity.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hmh.Web.ViewModels
{
    /// <summary>
    /// 登录--视图模型
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "下次自动登录")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {       
        
        public string UserName { get; set; }
        
        public string NickName { get; set; }

        public string Email { get; set; }

       
        public string EmailCode { get; set; }

       
        public string Password { get; set; }

        public int RecommendId { get; set; }
        
    }


    public class EditProfileViewModel
    {

        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
    }

    
   
    /// <summary>
    /// 我的账户--视图模型
    /// </summary>
    public class MyAccountViewModel
    {
        public User User { get; set; }
        public int MyNewInvoteCount { get; set; }
    }
}