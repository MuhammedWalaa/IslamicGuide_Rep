using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace temp.Controllers
{
    public class BaseController : Controller
    {
        public Guid? SystemUserId => Request.Cookies["UserId"] == null ? (Guid?)null : new Guid(Request.Cookies["UserId"].Value);
        public string LangCode => Request.Cookies["culture"] == null ? string.Empty : Request.Cookies["culture"].Value;
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang;
            var langCookie = Request.Cookies["culture"];
            if (langCookie != null)
                lang = langCookie.Value;
            else
            {
                var userLanguage = Request.UserLanguages;
                lang = userLanguage != null ? userLanguage[0] : "ar";
            }
            LanguageSetting.SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //if (Request.Cookies["UserId"] != null)
            //{

            //    var membersss = new MemberAPI();
            //    var rssss =  membersss.GetMemberByFileNumber("0040071888_1");

            //    //  var xxxxx = apiii.RedeemPoints("01113605989", "0.5", "201005852627");
            //    var academySuperAdminNoAccess = new List<string>() { "SystemUser" };
            //    var normalAdminNoAccess = new List<string>() { "SystemUser", "Court", "Academy", "AcademyLevel" };
            //    var cookie = Request.Cookies["UserId"];
            //    if (string.IsNullOrEmpty(cookie?.Value)) return;
            //    var userId = new Guid(cookie.Value);
            //    var currentController = requestContext.RouteData.Values["controller"];
            //    var userService = new SystemUserService();
            //    var user = userService.GetMainInfo(userId);
            //    if (user == null) return;
            //    if (user.SecurityRoleId == (int)SecurityRoleEnum.AcademySuperAdmin &&
            //        academySuperAdminNoAccess.Contains(currentController))
            //    {
            //        requestContext.HttpContext.Response.Clear();
            //        requestContext.HttpContext.Response.Redirect("/Error/e404");
            //        requestContext.HttpContext.Response.End();
            //    }
            //    else if (user.SecurityRoleId == (int)SecurityRoleEnum.AcademyNormalAdmin &&
            //            normalAdminNoAccess.Contains(currentController))
            //    {
            //        requestContext.HttpContext.Response.Clear();
            //        requestContext.HttpContext.Response.Redirect("/Error/e404");
            //        requestContext.HttpContext.Response.End();
            //    }
            //    else
            //    {
            //        ViewBag.FirstName = user.FirstName;
            //        ViewBag.UserName = user.FullName;
            //        ViewBag.Language = LangCode;
            //        ViewBag.IndividualSport = user.IndividualSport;
            //        ViewBag.UserImage = string.IsNullOrEmpty(user.Image) ? "avetar.png" : user.Image;
            //        ViewBag.UserEmail = user.Email;
            //        ViewBag.UserMobile = user.Mobile;
            //        ViewBag.SecuirtyRoleId = user.SecurityRoleId;
            //        var notificationService = new NotificationService();
            //        ViewData["Notifications"] = notificationService.GetUserLatestUnread(userId, LangCode);
            //        ViewBag.NotificationsCount = notificationService.GetCountUserUnread(userId);
            //    }
            //}
            //else
            //{
            //    requestContext.HttpContext.Response.Clear();
            //    requestContext.HttpContext.Response.Redirect("/");
            //    requestContext.HttpContext.Response.End();
            //}
        }
        //public UploadImageResult UploadImage(HttpPostedFileBase image, string oldImageName, string sectionName)
        //{
        //    if (image == null || image.ContentLength <= 0)
        //        return new UploadImageResult
        //        {
        //            ImageStatus = UploadImageStatus.NoImage,
        //            FileName = string.Empty
        //        };

        //    var actualSizePath = "~/content/images/actualsize/";
        //    var imageExt = Path.GetExtension(image.FileName);
        //    if (image.ContentType.Split('/')[0] != "image")
        //    {
        //        return new UploadImageResult
        //        {
        //            FileName = string.Empty,
        //            ImageStatus = UploadImageStatus.InvalidImageFormat
        //        };
        //    }
        //    var folderName = DateTime.Now.Year + "/" + DateTime.Now.Month;
        //    if (!Directory.Exists(Server.MapPath(actualSizePath + folderName)))
        //        Directory.CreateDirectory(Server.MapPath(actualSizePath + folderName));

        //    var fileName = folderName + "/" + sectionName + "_" +
        //                   Guid.NewGuid().ToString().Replace("-", "") + imageExt;
        //    image.SaveAs(Server.MapPath(actualSizePath + fileName));
        //    var imageResizer = new ImageResizer();
        //    imageResizer.Resize(oldImageName, fileName, sectionName);
        //    return new UploadImageResult
        //    {
        //        ImageStatus = UploadImageStatus.Sucess,
        //        FileName = fileName
        //    };
        //}
        //public UploadImageResult UploadFile(HttpPostedFileBase file, string oldIFileName, string sectionName)
        //{
        //    if (file == null || file.ContentLength <= 0)
        //        return new UploadImageResult
        //        {
        //            ImageStatus = UploadImageStatus.NoImage,
        //            FileName = string.Empty
        //        };

        //    var actualSizePath = "~/content/policyFiles/";
        //    var imageExt = Path.GetExtension(file.FileName);
        //    if (imageExt != ".pdf")
        //    {
        //        return new UploadImageResult
        //        {
        //            FileName = string.Empty,
        //            ImageStatus = UploadImageStatus.InvalidImageFormat
        //        };
        //    }
        //    var folderName = DateTime.Now.Year + "/" + DateTime.Now.Month;
        //    if (!Directory.Exists(Server.MapPath(actualSizePath + folderName)))
        //        Directory.CreateDirectory(Server.MapPath(actualSizePath + folderName));

        //    var fileName = folderName + "/" + sectionName + "_" +
        //                   Guid.NewGuid().ToString().Replace("-", "") + imageExt;
        //    file.SaveAs(Server.MapPath(actualSizePath + fileName));

        //    return new UploadImageResult
        //    {
        //        ImageStatus = UploadImageStatus.Sucess,
        //        FileName = fileName
        //    };
        //}


        //public void ShowSuccessMessage()
        //{
        //    TempData["ValidationMessage"] = new ValidationMessage()
        //    {
        //        Message = GeneralResource.GeneralSuccessMessage,
        //        Success = true
        //    };
        //}
        //public PageListFilter GetPageListFilter(int? status)
        //{
        //    try
        //    {
        //        var start = Request.Form["start"];

        //        return new PageListFilter()
        //        {
        //            Active = status != 0,
        //            LangCode = LangCode,
        //            PageSize = Request.Form["length"].Any() ? Convert.ToInt32(Request.Form["length"]) : 20,
        //            SearchValue = Request.Form["search[value]"]?.ToLower(),
        //            SortColName = Request.Form["columns[" + (Request.Form["order[0][column]"] ?? "") + "]?[name]"],
        //            SortDirection = Request.Form["order[0][dir]"],
        //            Skip = start.Any() ? Convert.ToInt32(start) : 0
        //        };
        //    }
        //    catch
        //    {
        //        return new PageListFilter()
        //        {
        //            Active = true,
        //            LangCode = LangCode,
        //            PageSize = 20,
        //            SortDirection = "asc",
        //            Skip = 0
        //        };
        //    }
        //}
        //public void ShowErrorMessage(string errorMessage)
        //{
        //    TempData["ValidationMessage"] = new ValidationMessage()
        //    {
        //        Message = string.IsNullOrEmpty(errorMessage) ? GeneralResource.GeneralErrorMessage : errorMessage,
        //        Success = false
        //    };
        //}

        //public void ShowSuccessMessage(string Successmessage)
        //{
        //    TempData["ValidationMessage"] = new ValidationMessage()
        //    {
        //        Message = string.IsNullOrEmpty(Successmessage) ? GeneralResource.GeneralSuccessMessage : Successmessage,
        //        Success = true
        //    };
        //}


    }
}