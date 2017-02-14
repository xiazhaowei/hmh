using Hmh.Core.Shop.Models;
using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Dtos
{
    public class ShopPermitInputDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 企业法人
        /// </summary>
        [Required, StringLength(50)]
        [Display(Name = "法人")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取设置 法人身份证号码
        /// </summary>
        [Required, StringLength(50)]
        [Display(Name = "法人证件号码")]
        public string UserCardNum { get; set; }

        /// <summary>
        /// 获取或设置 身份正正面照片
        /// </summary>
        [Required,StringLength(50)]
        [Display(Name = "身份证正面")]
        public string UserCardFront { get; set; }

        /// <summary>
        /// 获取或设置 身份正正面照片
        /// </summary>
        [Required, StringLength(50)]
        [Display(Name = "身份证反面")]
        public string UserCardReverse { get; set; }

        /// <summary>
        /// 获取或设置 营业执照编号
        /// </summary>
        [Required,StringLength(50)]
        [Display(Name = "营业执照编号")]
        public string BusinessLicenseNum { get; set; }

        /// <summary>
        /// 获取或设置 营业执照编号
        /// </summary>
        [Required, StringLength(50)]
        [Display(Name = "营业执照照片")]
        public string BusinessLicensePic { get; set; }

        [StringLength(50)]
        [Display(Name = "卫生许可证")]
        public string HealthLicensePic { get; set; }
      
        [StringLength(50)]
        [Display(Name = "授权书")]
        public string AuthLicensePic { get; set; }

        /// <summary>
        /// 获取或设置 资质审核状态
        /// </summary>
        [Required]
        [Display(Name = "审核状态")]
        public ShopPermitState State { get; set; }

        /// <summary>
        /// 获取或设置 审核状态
        /// </summary>
        [StringLength(50)]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 获取设置 店铺编号
        /// </summary>
        public int? ShopId { get; set; }
    }
}
