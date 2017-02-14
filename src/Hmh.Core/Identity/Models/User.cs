// -----------------------------------------------------------------------
//  <copyright file="User.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;

using OSharp.Core.Identity.Models;
using System.Collections.Generic;
using System;

namespace Hmh.Core.Identity.Models
{
    /// <summary>
    /// 实体类——用户信息
    /// </summary>
    [Description("用户信息")]
    public class User : UserBase<int>
    {
        public User()
        {
            DeliverAddresses = new List<DeliverAddress>();
            BankCards = new List<UserBankCard>();
            RmbCoinTransactions = new List<RmbCoinTransaction>();
            HCoinTransactions = new List<HCoinTransaction>();
            Orders = new List<Order.Models.Order>();
            CartGoodses = new List<Order.Models.CartGoods>();
        }
        /// <summary>
        /// 获取或设置 父级用户信息
        /// </summary>
        public virtual User Recommend { get; set; }

        /// <summary>
        /// 获取或设置 子级用户信息集合
        /// </summary>
        public virtual ICollection<User> MyRecommends { get; set; }        
            

        /// <summary>
        /// 获取或设置 用户银行卡信息
        /// </summary>
        public virtual ICollection<UserBankCard> BankCards { get; set; }

        /// <summary>
        /// 获取或设置 用户收件地址
        /// </summary>
        public virtual ICollection<DeliverAddress> DeliverAddresses { get; set; }
        /// <summary>
        /// 获取或设置 用户扩展信息
        /// </summary>
        public virtual UserExtend UserExtend { get; set; }

        /// <summary>
        /// 获取或设置 现金交易记录
        /// </summary>
        public virtual ICollection<RmbCoinTransaction> RmbCoinTransactions { get; set; }

        /// <summary>
        /// 获取或设置 现金交易记录
        /// </summary>
        public virtual ICollection<HCoinTransaction> HCoinTransactions { get; set; }

        /// <summary>
        /// 获取或设置 用户的订单
        /// </summary>
        public virtual ICollection<Order.Models.Order> Orders { get; set; }

        /// <summary>
        /// 获取或设置 用户购物车商品
        /// </summary>
        public virtual ICollection<Order.Models.CartGoods> CartGoodses { get; set; }

        /// <summary>
        /// 获取或设置 用户的店铺
        /// </summary>
        public virtual Shop.Models.Shop Shop { get; set; }
    }
}