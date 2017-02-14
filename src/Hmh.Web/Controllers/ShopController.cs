using Hmh.Core.Shop.Dtos;
using Hmh.Core.Shop.Models;
using Hmh.Web.ViewModels;
using OSharp.Utility;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hmh.Core.Goods.Models;
using System.Dynamic;
using Newtonsoft.Json;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Extensions;
using OSharp.Web.Mvc.UI;
using System.Threading.Tasks;
using Hmh.Core.Identity.Dtos;
using OSharp.Core.Mapping;
using Hmh.Core.Goods.Dtos;
using System.IO;
using System.Linq.Expressions;
using OSharp.Core.Data.Extensions;
using OSharp.Utility.Filter;
using System.ComponentModel;

namespace Hmh.Web.Controllers
{
    [Authorize]
    public class ShopController : CommonController
    {
        
        public ActionResult Index()
        {
            if (CurrentUser.Shop == null)
                return RedirectToAction("OpenShop");
            return View();
        }

        #region 订单管理

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderLists()
        {
            return View();
        }

        #endregion

        #region 商品管理
        /// <summary>
        /// 获取类别的 属性集合
        /// </summary>
        /// <param name="catid"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetAttrs(int catid)
        {
            if (catid <= 0)
                return Json(new OperationResult(OperationResultType.Error, "参数错误"));
            //select new object 只能在icollection中使用所以要tolist()
            var attrs = GoodsContract.Attrs.Where(a => a.Category.Id == catid).OrderBy(a => a.SortCode).ToList().Select(c => new {
                Name = c.Name,
                IsMust = c.IsMust,
                SortCode = c.SortCode,
                Type = c.Type,
                SelectableValues = string.IsNullOrEmpty(c.SelectableValues) ? new string[] { } : c.SelectableValues.Split(",", true)
            });

            return Json(attrs);

        }
        /// <summary>
        /// 获取类别的 规格
        /// </summary>
        /// <param name="catid"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetGoodsSpecifications(int catid)
        {
            if (catid <= 0)
                return Json(new OperationResult(OperationResultType.Error, "参数错误"));
            //分类是否启用颜色规格
            Category category = GoodsContract.Categorys.SingleOrDefault(c => c.Id == catid);
            if (category == null)
            {
                return Json(new OperationResult(OperationResultType.Error, "未找到发布分类"));
            }

            //获取类别下的所有规格
            var goodsSpecifications = GoodsContract.GoodsSpecifications.Where(gs => gs.Category.Id == catid).ToList().Select(gs => new {
                Name = gs.Name,
                SelectableValues = string.IsNullOrEmpty(gs.SelectableValues) ? new string[] { } : gs.SelectableValues.Split(",", true)
            });

            //如果启用了颜色
            if (category.IsUseColor)
            {
                List<GoodsColorSpecificationItem> colors = GoodsContract.GoodsColorSpecificationItems.ToList();
                
                string[] selectableValues = colors.Select(c => c.Name).ToArray();
                //添加颜色规格信息
                goodsSpecifications = goodsSpecifications.AsEnumerable().Concat(new[] { new { Name = "颜色", SelectableValues = selectableValues } }).AsQueryable();

            }
            //反转排序
            goodsSpecifications = goodsSpecifications.Reverse();

            return Json(goodsSpecifications);
        }
        /// <summary>
        /// 获取所有的发布分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult PubCategorys()
        {
            Func<Category, object> getNodeData = null;
            getNodeData = cat => {
                dynamic node = new ExpandoObject();
                node.id = cat.Id;
                node.name = cat.Name;
                node.children = new List<dynamic>();

                var children = cat.Children.OrderBy(c => c.SortCode).ToList();
                foreach (var child in children)
                {
                    node.children.Add(getNodeData(child));
                }
                return node;
            };

            List<Category> roots = GoodsContract.Categorys.Where(c => c.Parent == null).OrderBy(c => c.SortCode).ToList();
            List<object> nodes = roots.Select(getNodeData).ToList();
            //用JsonConvert序列化 直接用Json(nodes) 返回的为 Key Value 形式 不是标准json格式
            string json = JsonConvert.SerializeObject(nodes);
            return Content(json, "application/json");
        }
        /// <summary>
        /// 商品管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Goodses()
        {
            return View();
        }

        /// <summary>
        /// 商品管理-获取店铺的所以商品
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGoodses()
        {
            GridRequest request = new GridRequest(Request);            
            Expression<Func<Goods, bool>> predicate = FilterHelper.GetExpression<Goods>(request.FilterGroup);
            var page = GoodsContract.Goodss.Where(g=>g.Shop.Id==CurrentUser.Shop.Id).ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.GoodsPics,
                    m.Name,
                    m.Price,
                    m.Stock,
                    m.GoodsNumber,
                    m.CreatedTime                   
                });
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 选择分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Publish()
        {
            return View();
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateGoods()
        {
            return View();
        }

        /// <summary>
        /// 添加商品 post
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateGoods(GoodsInputDto dto)
        {
            dto.CheckNotNull(nameof(dto));
            dto.ShopId = CurrentUser.Shop.Id;
            dto.CategoryId = 3;

            OperationResult result =await GoodsContract.AddGoodss(dto);
            return Json(result.ToAjaxResult());
        }

        #endregion

        #region 运费模板
        /// <summary>
        /// 运费模板管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpressTemplates()
        {
            return View();
        }
        /// <summary>
        /// 运费模板分页数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExpressTemplates()
        {
            GridRequest request = new GridRequest(Request);
            Expression<Func<ExpressTemplate, bool>> predicate = FilterHelper.GetExpression<ExpressTemplate>(request.FilterGroup);
            var page = ShopContract.ExpressTemplates.Where(et=>et.Shop.Id==CurrentUser.Shop.Id).ToPage(predicate,
                request.PageCondition,
                m => new
                {
                    m.Id,
                    m.Name,
                    m.DeliverTime,
                    m.DeliverAddress,
                    m.Count,
                    m.CountAdd,
                    m.IsFree,
                    m.Price,
                    m.PriceAdd,
                    SpecialExpressAddresses = m.SpecialExpressAddresses.Select(s => new {//解决循环引用问题
                        s.Address,
                        s.Count,
                        s.CountAdd,
                        s.Price,
                        s.PriceAdd
                    }).ToList()
                });
            return Json(page.ToGridData(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加运费模板
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateExpressTemplate()
        {
            return View();
        }

        /// <summary>
        /// 创建运费模板
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateExpressTemplate(ExpressTemplateDto dto)
        {
            dto.CheckNotNull(nameof(dto));
            dto.ShopId = CurrentUser.Shop.Id;

            OperationResult result = await ShopContract.AddExpressTemplates(dto);
            return Json(result.ToAjaxResult());            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelExpressTemplate(int? id)
        {
            OperationResult result = OperationResult.NoChanged;
            if(id.HasValue && id.Value>0)
            {
                result = ShopContract.DeleteExpressTemplates(id.Value);
            }            
            return Json(result.ToAjaxResult());
        }

        #endregion

        #region 创建店铺


        /// <summary>
        /// 生成预览合同
        /// </summary>
        /// <param name="years"></param>
        /// <param name="contractLevel"></param>
        /// <returns></returns>
        public ActionResult GenContract(int years,string contractLevel)
        {
            //从数据查找合同级别
            ContractLevel cLevel = ShopContract.ContractLevels.SingleOrDefault(cl => cl.Name == contractLevel);
            ContractPreViewModel model = new ContractPreViewModel()
            {
                Shop = CurrentUser.Shop,
                Years = years,
                ContractLevel = new ContractLevel()
                {
                    HCoinLimit = cLevel.HCoinLimit,
                    InitalFee = cLevel.InitalFee
                }
            };

            return PartialView("_ContractInfo", model);
        }

        /// <summary>
        /// 开店首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenShop()
        {
            OpenShopViewModel model = new OpenShopViewModel
            {
                Shop = null,
                ErrorList = new List<string>()                
            };
            //判断店铺开张情况
            if(CurrentUser.Shop!=null)
            {
                model.Shop = CurrentUser.Shop;

                //判断资质情况
                //ShopPermit shopPermit = ShopContract.ShopPermits.FirstOrDefault(sp => sp.Shop.Id == model.Shop.Id);
                Hmh.Core.Shop.Models.ShopPermit shopPermit = CurrentUser.Shop.ShopPermit;
                if(shopPermit == null)
                {
                    model.ErrorList.Add("未上传认证资料");
                }
                else
                {
                    if (shopPermit.State == ShopPermitState.Verifying)
                        model.ErrorList.Add("您的店铺正在审核中");
                }

                Hmh.Core.Shop.Models.Contract currentContract = CurrentUser.Shop.CurrentContract;              
                if(currentContract==null)
                {
                    model.ErrorList.Add("未签合同");
                }
                
                //合同付款情况
                
            }

            return View(model);
        }

        /// <summary>
        /// 创建店铺-视图
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateShop()
        {
            if (CurrentUser.Shop != null)
                return RedirectToAction("ShopPermit");
            return View();
        }


        /// <summary>
        /// 创建店铺-提交
        /// </summary>
        /// <param name="Dtos"></param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult CreateShop(ShopInputDto Dtos)
        {            
            Dtos.CheckNotNull(nameof(Dtos));

            Dtos.UserId = CurrentUser.Id;
            Dtos.State = ShopState.Locked;//创建的店铺默认锁定
            Dtos.Type = StoreType.Store;  //非自营
            Dtos.HCoinLimit = 800;
            Dtos.Description = "暂无说明";

            //检查地区
            #region 检查地区
            string[] regionStrs = Dtos.RegionStr.Split(new char[] { '/'},StringSplitOptions.RemoveEmptyEntries);
            Region region = null;
            string val = "";
            if(regionStrs.Count()>0)
            {
                val = regionStrs[regionStrs.Length - 1];
            }
            switch(regionStrs.Length)
            {
                case 3:                    
                    region = ShopContract.Regions.SingleOrDefault(reg => reg.County == val);
                    break;
                case 2:
                    region = ShopContract.Regions.SingleOrDefault(reg => reg.City == val);
                    break;
                case 1:
                    region = ShopContract.Regions.SingleOrDefault(reg => reg.Province == val);
                    break;
            }
            if(region!=null)
            {
                Dtos.RegionId = region.Id;
            }
            #endregion

            OperationResult result = ShopContract.AddShops(Dtos);
            return Json(result.ToAjaxResult());
        }

        /// <summary>
        /// 店铺资料-视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopPermit()
        {
            if (CurrentUser.Shop == null)
                return RedirectToAction("CreateShop");            
            return View();
        }


        /// <summary>
        /// 店铺认证资料
        /// </summary>
        /// <param name="Dtos"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopPermit(ShopPermitInputDto Dtos)
        {             
            Dtos.CheckNotNull(nameof(Dtos));

            Dtos.ShopId = CurrentUser.Shop.Id;
            OperationResult result = ShopContract.AddShopPermits(Dtos);
            return Json(result.ToAjaxResult());           
        }

        /// <summary>
        /// 签订合同-视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SignContract()
        {
            if (CurrentUser.Shop == null)
                return RedirectToAction("CreateShop");          

            return View();
        }

        /// <summary>
        /// 签订合同-提交
        /// </summary>
        /// <param name="signContractViewModel"></param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult SignContract(SignContractViewModel signContractViewModel)
        {            
            signContractViewModel.CheckNotNull(nameof(signContractViewModel));
            //获取合同级别
            ContractLevel contractLevel = ShopContract.ContractLevels.SingleOrDefault(cl => cl.Name == signContractViewModel.ContractLevel);
            if(contractLevel==null)
            {
                return Json(new AjaxResult("没有该类型的合同", AjaxResultType.Error));
            }

            ContractInputDto dto = new ContractInputDto
            {
                ShopId = CurrentUser.Shop.Id,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddYears(signContractViewModel.Year),
                State = ContractState.UnAvliable,
                HCoinLimit = contractLevel.HCoinLimit,
                InitalFee = contractLevel.InitalFee,
                Year = signContractViewModel.Year,
                Fee = contractLevel.InitalFee * signContractViewModel.Year,
                Number = "HMH" + DateTime.Now.ToString("yyMMddHHmmss") + new Random().GetRandomNumberString(4)//合同编号HMH0000000000000000
            };
            OperationResult result = ShopContract.AddContracts(dto);
            return Json(result.ToAjaxResult());

        }

        /// <summary>
        /// 开店缴纳保证金-视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractPay()
        {
            if (CurrentUser.Shop == null)
                return RedirectToAction("CreateShop");          

            return View();
        }

        [HttpPost]        
        public ActionResult ContractPay(ContractPayViewModel contractPayViewModel)
        {           
            contractPayViewModel.CheckNotNull(nameof(contractPayViewModel));
            ContractPayInputDto dto = new ContractPayInputDto() {
                ContractId = CurrentUser.Shop.CurrentContract.Id,
                PayStreamId = contractPayViewModel.PayStreamId,
                PayType=PayType.Bank,
                Money=contractPayViewModel.Money
            };

            //添加付款信息
            OperationResult result = ShopContract.AddContractPays(dto);
            return Json(result.ToAjaxResult());            
        }


        #endregion

        #region 合同管理
        /// <summary>
        /// 获取合同级别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContractLevel()
        {
            var levels = ShopContract.ContractLevels.OrderBy(cl => cl.InitalFee).Select(cl => new {
                Name = cl.Name,
                InitalFee = cl.InitalFee,
                HCoinLimit = cl.HCoinLimit
            });
            return Json(levels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 合同详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ContractDetail(int? id)
        {
            
            if(id.HasValue && id.Value>0)
            {
                Contract model = CurrentUser.Shop.Contracts.SingleOrDefault(c=>c.Id==id.Value);
                if(model!=null)
                {
                    return View(model);
                }
            }
            return View();
        }

        /// <summary>
        /// 合同管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ContractList()
        {            
            return View(CurrentUser.Shop.Contracts);
        }

        #endregion
    }
}