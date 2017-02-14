using Hmh.Core.Settings.Models;
using OSharp.Data.Entity;
using System;



namespace Hmh.Core.ModelConfigurations.Settings
{
    public class SystemSettingConfiguration : EntityConfigurationBase<SystemSetting, Int32>
    {
        public SystemSettingConfiguration()
        {
            SystemSettingConfigurationAppend();
        }

        void SystemSettingConfigurationAppend()
        {
            
        }
    }
}
