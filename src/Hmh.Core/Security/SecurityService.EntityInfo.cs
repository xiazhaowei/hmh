// -----------------------------------------------------------------------
//  <copyright file="SecurityService.EntityInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-11-27 20:01</last-date>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;

using OSharp.Core.Dependency;
using OSharp.Core.Security;
using Hmh.Core.Security.Dtos;
using OSharp.Utility.Data;


namespace Hmh.Core.Security
{
    public partial class SecurityService
    {
        /// <summary>
        /// 获取 实体数据信息查询数据集
        /// </summary>
        public IQueryable<EntityInfo> EntityInfos
        {
            get { return EntityInfoRepository.Entities; }
        }

        /// <summary>
        /// 更新实体数据信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的实体数据信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateEntityInfos(params EntityInfoInputDto[] dtos)
        {
            OperationResult result = await EntityInfoRepository.UpdateAsync(dtos);
            if (result.Successed)
            {
                IEntityInfoHandler handler = ServiceProvider.GetService<IEntityInfoHandler>();
                handler.RefreshCache();
            }
            return result;
        }
    }
}