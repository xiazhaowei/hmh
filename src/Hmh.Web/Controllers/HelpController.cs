using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hmh.Web.Controllers
{
    public class HelpController : CommonController
    {
        
        public ActionResult Index()
        {
            return View();
        }

        #region 反馈
        /// <summary>
        /// 反馈
        /// </summary>
        /// <returns></returns>
        public ActionResult FeedBack()
        {
            return View();
        }

        #endregion
    }
}