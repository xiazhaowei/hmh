// -----------------------------------------------------------------------
//  <copyright file="HomeController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-07-14 1:02</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Web.Mvc;

using OSharp.Utility.Logging;
using OSharp.Web.Mvc.Logging;
using System.Linq;
using System.Text;
using Hmh.Core.Goods.Models;
using System;
using System.Dynamic;
using System.Collections.Generic;
using Newtonsoft.Json;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;
using OSharp.Utility.Filter;
using System.Linq.Expressions;
using OSharp.Core.Data.Extensions;
using OSharp.Utility.Extensions;
using Hmh.Web.ViewModels;
using OSharp.Utility.Data;
using Hmh.Core.Extensions;

namespace Hmh.Web.Controllers
{
    [OperateLogFilter]
    [Description("网站")]
    public class HomeController : CommonController
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(HomeController));



        [Description("网站-首页")]
        public ActionResult Index()
        {
            return View();
        }

        #region 公告

        /// <summary>
        /// 主导航展示分类显示
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMainNavCategory()
        {
            //递归获取分类包含子类
            Func<ShowCategory, object> getNodeData = null;

            getNodeData = cat => {
                dynamic node = new ExpandoObject();
                node.id = cat.Id;
                node.name = cat.Name;
                node.logo = cat.Logo;
                node.link = cat.Link;
                node.children = new List<dynamic>();

                var children = cat.Children.OrderBy(c => c.SortCode).ToList();
                foreach (var child in children)
                {
                    node.children.Add(getNodeData(child));
                }

                return node;
            };

            List<ShowCategory> roots = GoodsContract.ShowCategorys.Where(c => c.Parent == null).OrderBy(c => c.SortCode).ToList();
            List<object> nodes = roots.Select(getNodeData).ToList();
            string json = JsonConvert.SerializeObject(nodes);
            return Content(json, "application/json");
        }

        #endregion

        #region 全部分类
        public ActionResult AllCategory()
        {
            return View();
        }

        #endregion        

        #region 商品筛选


        public ActionResult GoodsList()
        {
            return View();
        }

        public ActionResult GetCategory(int catid)
        {
            Category category = GoodsContract.Categorys.SingleOrDefault(c => c.Id == catid);

            if (category == null)
            {
                return Content("{}", "application/json");
            }

            //颜色规格信息
            List<GoodsColorSpecificationItem> colors = GoodsContract.GoodsColorSpecificationItems.ToList();
            string[] selectableValues = colors.Select(c => c.Name).ToArray();

            var cat = new
            {
                category.Id,
                category.Name,
                category.IsUseColor,
                Attrs = category.Attrs.Where(a => a.IsMust == true).Select(a => new {
                    a.Id,
                    a.Name,
                    a.Type,
                    SelectableValues = a.SelectableValues.Split(",", true)
                }),
                GoodsSpecifications = category.GoodsSpecifications.Select(gs=> new {                    
                    gs.Name,                    
                    SelectableValues= gs.SelectableValues.Split(",",true)
                }).AsEnumerable().Concat(new[] { new { Name = "颜色", SelectableValues = selectableValues } }).AsQueryable(),
                Children = category.Children.Select(c => new {//解决循环引用问题
                    c.Id,
                    c.Name
                })
            };            

            return Json(cat, JsonRequestBehavior.AllowGet);
        }

        private bool getattrvaluefilter(string attrvalue,ICollection<string> values)
        {
            bool result = true;
            foreach(string value in values)
            {
                result = result || attrvalue.Contains(value);
            }
            return result;
        }

        public ActionResult GetGoodsList()
        {
            int catId = Request.Params["catId"].CastTo(2);//分类
            int pageIndex = Request.Params["pageIndex"].CastTo(1);
            int pageSize = Request.Params["pageSize"].CastTo(20);

            decimal minPrice = Request.Params["minPrice"].CastTo(0);//开始价格
            decimal maxPrice = Request.Params["maxPrice"].CastTo(0);

            bool isSelf = Request.Params["isSelf"].CastTo(false);//自营
            bool isExpressFree = Request.Params["isExpressFree"].CastTo(false);//包邮
            bool isStock = Request.Params["isStock"].CastTo(false);//有货

            string sort = Request.Params["sort"];//排序

            string attrJson = Request.Params["attr"];//属性
            string specJson = Request.Params["spec"];//规格

            List<AttrFilter> attrFilters = !attrJson.IsNullOrEmpty() ? JsonHelper.FromJson<List<AttrFilter>>(attrJson) : new List<AttrFilter>();
            List<SpecFilter> specFilters = !specJson.IsNullOrEmpty() ? JsonHelper.FromJson<List<SpecFilter>>(specJson) : new List<SpecFilter>();
            int total = 0;

            IQueryable<Goods> data = GoodsContract.Goodss;

            if (catId > 0)//分类
            {
                data = data.Where(c => c.Category.Id == catId || c.Category.Parent.Id == catId);
            }
            if (minPrice>=0 && maxPrice>0)//价格
            {
                data = data.Where(g => g.Price > minPrice && g.Price < maxPrice);
            }
            
            if(attrFilters.Count>0)//属性
            {
                foreach(AttrFilter attrFilter in attrFilters)
                {                    
                    data = data.Where(c => c.GoodsAttrs.Where(                        
                        ga => ga.AttrName == attrFilter.Name && attrFilter.Values.Any(v=>ga.AttrValue.Contains(v))
                        ).Count() > 0);                    
                }
                
            }            
            if (specFilters.Count > 0)//规格
            {
                foreach (SpecFilter specFilter in specFilters)
                {
                    data = data.Where(c => c.Skus.Where(
                        sku => specFilter.Values.Any(v => sku.Values.Contains(v))
                        ).Count() > 0);
                }

            }
            if (isSelf)//是否自营
            {
                data = data.Where(g => g.Shop.Type == Core.Shop.Models.StoreType.Store);
            }
            if(isExpressFree)//是否包邮
            {
                data = data.Where(g=>g.ExpressTemplate.IsFree==true);
            }
            if (isStock)//有货
            {
                data = data.Where(g=>g.Stock>0);
            }

            IQueryable<object> data2;
            
            if(!sort.IsNullOrEmpty())
            {
                string[] sortInfo = sort.Split("_");
                if(sortInfo.Length!=2)
                {
                    throw new ArgumentException("排序格式错误");
                }
                string sortField = sortInfo[0];
                string sortOrder = sortInfo[1];
                if(sortField=="price" && sortOrder=="asc")
                {
                    data2 = data.OrderBy(g => g.Price).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else if(sortField=="sellcount" && sortOrder=="asc")
                {
                    data2 = data.OrderBy(g => g.SellCount).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else if (sortField == "price" && sortOrder == "desc")
                {
                    data2 = data.OrderByDescending(g => g.Price).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else if (sortField == "sellcount" && sortOrder == "desc")
                {
                    data2 = data.OrderByDescending(g => g.SellCount).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else
                {
                    //默认排序
                    data2 = data.OrderBy(g => g.SortCode).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }

            }
            else
            {
                //默认排序
               data2 = data.OrderBy(g=>g.SortCode).Select(c => new {
                    c.Id,
                    c.Name,
                    c.Price,
                   c.SellCount,
                   ShopName = c.Shop.Name,
                    c.GoodsPics
                });
            }
            

            total = data.Count();

            //获取分页数据
            var pagedata = data2.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var pageResult = new PageResult<object> {
                Data = pagedata.ToArray(),
                Total = total
            };

            return Json(pageResult.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 商品详情

        public ActionResult GetGoodsInfo(int? id)
        {
            if (!id.HasValue)
                return Content("{}", "application/json");

            Goods goods = GoodsContract.Goodss.SingleOrDefault(g => g.Id == id.Value);
            if(goods==null)
                return Content("{}", "application/json");

            StringBuilder sb = new StringBuilder();

            var Skus = goods.Skus.ToList();
            string[] SkuInfo= { };
            for (int i = 0; i < Skus.Count; i++)
            {
                SkuInfo = Skus[i].Names.Split(',');
                sb.Append("'" + Skus[i].Values.Replace(',', '#') + "':{");
                sb.Append("id:"+Skus[i].Id+",");
                sb.Append("goodsnumber:\""+Skus[i].GoodsNumber+"\",");
                sb.Append("skupic:\""+Skus[i].SkuPic+"\",");
                sb.Append("count:" + Skus[i].Stock +",");
                sb.Append("price:"+Skus[i].Price);
                if (i + 1 == Skus.Count)
                    sb.Append("}");
                else
                    sb.Append("},");
            }

            var gd = new {
                goods.Id,
                goods.Name,
                goods.Description,
                goods.Price,
                goods.Stock,
                Skus = "{" + sb.ToString() + "}",

                GoodsAttrs = goods.GoodsAttrs.Select(ga => new {
                    ga.AttrName,
                    ga.AttrValue
                }),
                Shop = new {
                    goods.Shop.Id,
                    goods.Shop.Name,
                    goods.Shop.HCoinLimit,
                    Region = goods.Shop.Region.Province + " " + goods.Shop.Region.City + " " + goods.Shop.Region.County
                },
                SkuInfo = SkuInfo,
                SkuCount = goods.Skus.Count,
                CommentCount = goods.GoodsComments.Count,
                goods.IsReceipt,
                goods.IsReplacement,
                goods.IsGuarantee,
                IsBaoyou=goods.ExpressTemplate.IsFree,
                goods.GoodsPics,
                goods.GoodsNumber,                
                goods.Detail
            };
            
            
            return Json(gd, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetGoodsComments()
        {
            int goodsId = Request.Params["goodsId"].CastTo(0);

            GridRequest request = new GridRequest(Request);
            Expression<Func<GoodsComment, bool>> predicate = FilterHelper.GetExpression<GoodsComment>(request.FilterGroup);
            var page = GoodsContract.GoodsComments.Where(gc => gc.Goods.Id == goodsId).ToPage(predicate, request.PageCondition, gc => new {
                gc.User.UserName,
                gc.CreatedTime,
                gc.Content,
                gc.SkuInfo,
                gc.User.UserExtend.HeadImage,
                gc.User.NickName,
                gc.Pics
            });
            return Json(page.ToGridData(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoodsInfo(int? id)
        {
            if (!id.HasValue || id.Value == 0)
                return new HttpStatusCodeResult(400);

            Goods goods = GoodsContract.Goodss.SingleOrDefault(g=>g.Id==id.Value);
            if (goods == null)
                return new HttpStatusCodeResult(400);            

            return View(goods);
        }
        #endregion

        #region 店铺页面
        public ActionResult ShopHome()
        {
            return View();
        }

        public ActionResult ShopGoods()
        {
            return View();
        }

        public ActionResult GetShopInfo(int? shopId)
        {
            if (!shopId.HasValue)
                return new HttpStatusCodeResult(400);
            Hmh.Core.Shop.Models.Shop shop = ShopContract.Shops.SingleOrDefault(s => s.Id == shopId.Value);
            if(shop==null)
                return new HttpStatusCodeResult(400);

            var sp = new {
                shop.Name,
                shop.Region.Province,
                shop.Region.City,
                shop.Region.County,
                shop.HCoinLimit,
                ShopGoodsCategoryes=shop.ShopGoodsCategoryes.Select(sgc=>new {
                    sgc.Id,
                    sgc.Name,
                    sgc.SortCode
                })
            };

            return Json(sp, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetShopGoods()
        {
            int shopId = Request.Params["shopId"].CastTo(0);//店铺
            int catId = Request.Params["catId"].CastTo(0);//分类
            int pageIndex = Request.Params["pageIndex"].CastTo(1);
            int pageSize = Request.Params["pageSize"].CastTo(20); 
            bool isExpressFree = Request.Params["isExpressFree"].CastTo(false);//包邮
            bool isStock = Request.Params["isStock"].CastTo(false);//有货

            string sort = Request.Params["sort"];//排序            
            int total = 0;

            IQueryable<Goods> data = GoodsContract.Goodss;

            if(shopId>0)
            {
                data = data.Where(g=>g.Shop.Id==shopId);
            }
            if (catId > 0)//分类
            {
                data = data.Where(c => c.ShopGoodsCategory.Id == catId);
            }            
            if (isExpressFree)//是否包邮
            {
                data = data.Where(g => g.ExpressTemplate.IsFree == true);
            }
            if (isStock)//有货
            {
                data = data.Where(g => g.Stock > 0);
            }

            IQueryable<object> data2;

            if (!sort.IsNullOrEmpty())
            {
                string[] sortInfo = sort.Split("_");
                if (sortInfo.Length != 2)
                {
                    throw new ArgumentException("排序格式错误");
                }
                string sortField = sortInfo[0];
                string sortOrder = sortInfo[1];
                if (sortField == "price" && sortOrder == "asc")
                {
                    data2 = data.OrderBy(g => g.Price).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else if (sortField == "sellcount" && sortOrder == "asc")
                {
                    data2 = data.OrderBy(g => g.SellCount).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else if (sortField == "price" && sortOrder == "desc")
                {
                    data2 = data.OrderByDescending(g => g.Price).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else if (sortField == "sellcount" && sortOrder == "desc")
                {
                    data2 = data.OrderByDescending(g => g.SellCount).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }
                else
                {
                    //默认排序
                    data2 = data.OrderBy(g => g.SortCode).Select(c => new {
                        c.Id,
                        c.Name,
                        c.Price,
                        c.SellCount,
                        ShopName = c.Shop.Name,
                        c.GoodsPics
                    });
                }

            }
            else
            {
                //默认排序
                data2 = data.OrderBy(g => g.SortCode).Select(c => new {
                    c.Id,
                    c.Name,
                    c.Price,
                    c.SellCount,
                    ShopName = c.Shop.Name,
                    c.GoodsPics
                });
            }
            total = data.Count();

            //获取分页数据
            var pagedata = data2.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var pageResult = new PageResult<object>
            {
                Data = pagedata.ToArray(),
                Total = total
            };

            return Json(pageResult.ToGridData(), JsonRequestBehavior.AllowGet);            
        }

        public ActionResult GetShopHotGoods(int? shopId)
        {
            if (!shopId.HasValue)
                return new HttpStatusCodeResult(400);
            var hotGoodses = GoodsContract.Goodss.Where(g => g.Shop.Id == shopId).OrderByDescending(g => g.SellCount).Take(8).Select(g => new {
                g.Name,
                g.GoodsPics,
                g.Id,
                g.Price,
                g.SellCount
            });

            return Json(hotGoodses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShopNewGoods(int? shopId)
        {
            if (!shopId.HasValue)
                return new HttpStatusCodeResult(400);
            var hotGoodses = GoodsContract.Goodss.Where(g => g.Shop.Id == shopId).OrderByDescending(g => g.Id).Take(8).Select(g => new {
                g.Name,
                g.GoodsPics,
                g.Id,
                g.Price,
                g.SellCount
            });

            return Json(hotGoodses, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}