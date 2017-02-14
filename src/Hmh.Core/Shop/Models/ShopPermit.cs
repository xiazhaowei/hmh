using OSharp.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Shop.Models
{
    /// <summary>
    /// 实体类---合同信息
    /// </summary>
    [Description("店铺-认证信息")]
    public class ShopPermit : EntityBase<int>, ILockable, ICreatedTime
    {
        /// <summary>
        /// 获取或设置 企业法人
        /// </summary>
        [StringLength(50)]
        [Display(Name ="法人")]
        public string UserName { get; set; }
        /// <summary>
        /// 获取设置 法人身份证号码
        /// </summary>
        [StringLength(50)]
        [Display(Name = "法人证件号码")]
        public string UserCardNum { get; set; }

        /// <summary>
        /// 获取或设置 身份正正面照片
        /// </summary>
        [StringLength(50)]
        [Display(Name = "身份证正面")]
        public string UserCardFront { get; set; }

        /// <summary>
        /// 获取或设置 身份正正面照片
        /// </summary>
        [StringLength(50)]
        [Display(Name = "身份证反面")]
        public string UserCardReverse { get; set; }

        /// <summary>
        /// 获取或设置 营业执照编号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "营业执照编号")]
        public string BusinessLicenseNum { get; set; }

        /// <summary>
        /// 获取或设置 营业执照编号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "营业执照照片")]
        public string BusinessLicensePic { get; set; }

        /// <summary>
        /// 获取或设置 卫生许可证
        /// </summary>
        [StringLength(50)]
        [Display(Name = "卫生许可证")]
        public string HealthLicensePic { get; set; }

        /// <summary>
        /// 获取或设置 授权书
        /// </summary>
        [StringLength(50)]
        [Display(Name = "授权书")]
        public string AuthLicensePic { get; set; }

        /// <summary>
        /// 获取或设置店铺
        /// </summary>
        public Shop Shop { get; set; }

        /// <summary>
        /// 获取或设置 资质审核状态
        /// </summary>
        [Required]
        [Display(Name ="审核状态")]
        public ShopPermitState State { get; set; }

        /// <summary>
        /// 获取或设置 审核状态
        /// </summary>
        [StringLength(50)]
        public string ErrorMessage { get; set; }

        #region Implementation of ICreatedTime

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        #endregion

        #region Implementation of ILockable
        /// <summary>
        /// 获取或设置 是否锁定，用于禁用当前信息
        /// </summary>
        public bool IsLocked { get; set; }
        #endregion
        
    } 
    
    [Description("店铺资质状态")]
    public enum ShopPermitState
    {
        [Description("审核中")]
        Verifying,

        [Description("审核未通过")]
        VerifyFailed,

        [Description("审核通过")]
        Verifyed
    }   
    
}
