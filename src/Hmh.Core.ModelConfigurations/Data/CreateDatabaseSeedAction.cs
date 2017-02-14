// -----------------------------------------------------------------------
//  <copyright file="CreateDatabaseSeedAction.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-01 21:39</last-date>
// -----------------------------------------------------------------------

using System;
using System.Data.Entity;

using OSharp.Data.Entity.Migrations;
using Hmh.Core.Identity.Models;
using Hmh.Core.Shop.Models;
using System.Collections.Generic;
using Hmh.Core.Goods.Models;
using Hmh.Core.Settings.Models;

namespace Hmh.Core.ModelConfigurations.Data
{
    public class CreateDatabaseSeedAction : ISeedAction
    {
        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Action(DbContext context)
        {
            InitRoleData(context);            

            //InitCategoryData(context);
            //InitShowCategoryData(context);

            InitUserData(context);

            InitRegionData(context);
            InitContractLevelData(context);
            InitSystemSettingData(context);

        }

        /// <summary>
        /// 初始化角色信息
        /// </summary>
        /// <param name="context"></param>
        private void InitRoleData(DbContext context)
        {
            //初始化角色            
            List<Role> roles = new List<Role>
            {
                new Role {
                    Name = "系统管理员",
                    Remark = "系统管理员角色，拥有系统最高权限",
                    IsAdmin = true,
                    IsSystem = true,
                    IsLocked = false,
                    CreatedTime = DateTime.Now},
                new Role {
                    Name = "用户",
                    Remark = "网站普通用户",
                    IsAdmin = false,
                    IsSystem = true,
                    IsLocked = false,
                    CreatedTime = DateTime.Now}
            };
            context.Set<Role>().AddRange(roles);
        }

        /// <summary>
        /// 初始化发布分类数据
        /// </summary>
        /// <param name="context"></param>
        private void InitCategoryData(DbContext context)
        {
            //初始化发布分类
            context.Set<Category>()
                .Add(new Category()
                {
                    Name = "system_default",
                    Profit = 0,
                    Distribution = 0
                });
        }

        /// <summary>
        /// 初始化 展示分类
        /// </summary>
        /// <param name="context"></param>
        private void InitShowCategoryData(DbContext context)
        {
            //初始化展示分类
            context.Set<ShowCategory>()
                .Add(new ShowCategory()
                {
                    Name = "system_default"
                });
        }
        /// <summary>
        /// 初始化用户数据
        /// </summary>
        /// <param name="context"></param>
        private void InitUserData(DbContext context)
        {
            //初始化用户
            context.Set<User>()
                .Add(new User()
                {
                    UserName = "system_default",
                    NickName = "系统默认用户",
                    CreatedTime = DateTime.Now,
                    Email = "hmh@hmh.com",
                    PhoneNumber = "13561962764"
                });
        }

        /// <summary>
        /// 初始化合同级别数据
        /// </summary>
        /// <param name="context"></param>
        private void InitContractLevelData(DbContext context)
        {
            //合同级别
            List<ContractLevel> contractLevels = new List<ContractLevel>
            {
                new ContractLevel {Name="普通店铺",InitalFee=10000,HCoinLimit=800 },
                new ContractLevel {Name="黄金店铺",InitalFee=30000,HCoinLimit=1500 },
                new ContractLevel {Name="钻石店铺",InitalFee=50000,HCoinLimit=3000 }

            };
            context.Set<ContractLevel>().AddRange(contractLevels);
        }

        /// <summary>
        /// 初始化 地区信息
        /// </summary>
        /// <param name="context"></param>
        private void InitRegionData(DbContext context)
        {
            //地区
            List<Region> regions = new List<Region>
            {
                new Region {Id=110000, Province="北京市",City="",County="" ,Street="",IsOpenServices=true},
                new Region {Id=110100, Province="北京市",City="北京市",County="" ,Street="",IsOpenServices=true},
                new Region {Id=110101, Province="北京市",City="北京市",County="东城区" ,Street="",IsOpenServices=true},
                new Region {Id=110102, Province="北京市",City="北京市",County="西城区" ,Street="",IsOpenServices=true},
                new Region {Id=110105, Province="北京市",City="北京市",County="朝阳区" ,Street="",IsOpenServices=false},
                new Region {Id=110106, Province="北京市",City="北京市",County="丰台区" ,Street="",IsOpenServices=false},
                new Region {Id=110107, Province="北京市",City="北京市",County="石景山区" ,Street="",IsOpenServices=false},
                new Region {Id=110108, Province="北京市",City="北京市",County="海淀区" ,Street="",IsOpenServices=false},
                new Region {Id=110109, Province="北京市",City="北京市",County="门头沟区" ,Street="",IsOpenServices=false},
                new Region {Id=110111, Province="北京市",City="北京市",County="房山区" ,Street="",IsOpenServices=false},
                new Region {Id=110112, Province="北京市",City="北京市",County="通州区" ,Street="",IsOpenServices=false},
                new Region {Id=110113, Province="北京市",City="北京市",County="顺义区" ,Street="",IsOpenServices=false},
                new Region {Id=110114, Province="北京市",City="北京市",County="昌平区" ,Street="",IsOpenServices=false},
                new Region {Id=110115, Province="北京市",City="北京市",County="大兴区" ,Street="",IsOpenServices=false},
                new Region {Id=110116, Province="北京市",City="北京市",County="怀柔区" ,Street="",IsOpenServices=false},
                new Region {Id=110117, Province="北京市",City="北京市",County="平谷区" ,Street="",IsOpenServices=false},
                new Region {Id=110228, Province="北京市",City="北京市",County="密云县" ,Street="",IsOpenServices=false},
                new Region {Id=110229, Province="北京市",City="北京市",County="延庆县" ,Street="",IsOpenServices=false}

               
            };
            context.Set<Region>().AddRange(regions);
        }

        /// <summary>
        /// 初始化 系统参数数据
        /// </summary>
        /// <param name="context"></param>
        private void InitSystemSettingData(DbContext context)
        {
            //系统参数
            List<SystemSetting> systemSettings = new List<SystemSetting>
            {
                new SystemSetting {Key="Distribution_RMB_Persent",Description="三级分销人民币奖金比例%",Value="70"},
                new SystemSetting {Key="Withdraw_Persent",Description="提现手续费%",Value="3"},
                new SystemSetting {Key="Withdraw_Max",Description="单笔最高提现金额￥",Value="5000"},

                new SystemSetting {Key="QQ_AppId",Description="QQ接口ID",Value=""},
                new SystemSetting {Key="Alipay_PID",Description="支付宝PID",Value=""},
                new SystemSetting {Key="Alipay_Key",Description="支付宝Key",Value=""},
                new SystemSetting {Key="QQ_AppId",Description="QQ接口ID",Value=""},
                new SystemSetting {Key="WeXin_AppId",Description="微信公众号ID",Value=""},
                new SystemSetting {Key="WeXinPay_Key",Description="微信支付Key",Value=""},
                new SystemSetting {Key="WeXinPay_StoreId",Description="微信支付商户号",Value=""},

                new SystemSetting {Key="Site_Name",Description="网址名称",Value="惠民汇商城"},
                new SystemSetting {Key="ICP_Number",Description="ICP备案号",Value="沪ICP备16018651号-2"},
                new SystemSetting {Key="Copy_Rights",Description="版权所有",Value="上海悟行电子商务有限公司"}

            };
            context.Set<SystemSetting>().AddRange(systemSettings);
        }

        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}