// -----------------------------------------------------------------------
//  <copyright file="IdentityService.Role.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-12-04 17:50</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using OSharp.Core.Identity;
using OSharp.Core.Mapping;
using Hmh.Core.Identity.Dtos;
using Hmh.Core.Identity.Models;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace Hmh.Core.Identity
{
    public partial class IdentityService
    {

        #region Implementation of IIdentityContract
        /// <summary>
        /// 获取 查询数据集
        /// </summary>
        public IQueryable<HCoinTransaction> HCoinTransactions
        {
            get { return HCoinTransactionRepository.Entities; }
        }
        #endregion
    }
}