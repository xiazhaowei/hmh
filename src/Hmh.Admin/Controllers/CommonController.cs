// -----------------------------------------------------------------------
//  <copyright file="CommonController.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-02-27 23:16</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

using OSharp.Core;
using OSharp.Utility.Drawing;
using OSharp.Utility.Extensions;
using OSharp.Web.Mvc;
using OSharp.Web.Mvc.Security;


namespace Hmh.Admin.Controllers
{
    [Description("网站-通用")]
    public class CommonController : BaseController
    {
        [HttpPost]
        [AjaxOnly]
        [Description("网站-通用-验证验证码")]
        public ActionResult CheckVerify(string code)
        {
            if (code.IsMissing())
            {
                return Json(false);
            }
            return Json(SecurityHelper.CheckVerify(code));
        }

        [Description("网站-通用-生成验证码")]
        public ActionResult VerifyCode()
        {
            ValidateCoder coder = new ValidateCoder()
            {
                RandomColor = true,
                RandomItalic = true,
                RandomPosition = true,
                RandomLineCount = 5
            };
            string code;
            Bitmap bitmap = coder.CreateImage(4, out code);
            Session[Constants.VerifyCodeSession] = code.ToUpper();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                return File(ms.ToArray(), @"image/jpeg");
            }
        }
    }
}