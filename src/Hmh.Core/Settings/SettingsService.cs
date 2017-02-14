
using Hmh.Core.Settings.Models;
using OSharp.Core.Data;


namespace Hmh.Core.Settings
{
    /// <summary>
    /// 业务实现-店铺模块
    /// </summary>
    public partial class SettingsService:ISettingsContract
    {
        /// <summary>
        /// 获取或设置 信息仓储操作对象
        /// </summary>
        public IRepository<SystemSetting, int> SystemSettingRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 信息仓储操作对象
        /// </summary>
        public IRepository<ShopDistributionLevel, int> ShopDistributionLevelRepository { protected get; set; }

        /// <summary>
        /// 获取或设置 信息仓储操作对象
        /// </summary>
        public IRepository<UserDistributionLevel, int> UserDistributionLevelRepository { protected get; set; }
        
    }
}
