using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BlogProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());
        Context c = new Context();
        public IActionResult InBox(int page=1)
        {
            var usermail = User.Identity.Name;
            var writerID = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            var values = messageManager.MessageListWithSenderUser().Where(x => x.ReceiverID == writerID).ToList().ToPagedList(page, 10);
            var sendeValues = messageManager.MessageListWithReceiverUser().Where(x => x.SenderID == writerID).ToList();
            ViewBag.sender = sendeValues.Count();
            return View(values);
        }

        public IActionResult SendBox(int page=1)
        {
            var usermail = User.Identity.Name;
            var writerID = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            var values = messageManager.MessageListWithReceiverUser().Where(x => x.SenderID == writerID).ToList().ToPagedList(page, 10);
            var receiverValues = messageManager.MessageListWithSenderUser().Where(x => x.ReceiverID == writerID).ToList();
            ViewBag.receiver = receiverValues.Count();
            return View(values);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            List<SelectListItem> userEmails = new List<SelectListItem>();
            userEmails.Add(new SelectListItem
            {
                Text = "Lütfen alıcının epostasını seçiniz.. ",
                Value = ""
            });
            foreach (var user in c.Users)
            {
                userEmails.Add(new SelectListItem
                {
                    Text = user.Email,
                    Value = user.Id.ToString(),
                });
            }
            var usermail = User.Identity.Name;
            userEmails.RemoveAll(x => x.Text == usermail); ;
            ViewBag.users = userEmails;
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message p)
        {
            List<SelectListItem> userEmails = new List<SelectListItem>();
            userEmails.Add(new SelectListItem
            {
                Text = "Lütfen alıcının epostasını seçiniz.. ",
                Value = ""
            });
            foreach (var user in c.Users)
            {
                userEmails.Add(new SelectListItem
                {
                    Text = user.Email,
                    Value = user.Id.ToString(),
                });
            }
            ViewBag.users = userEmails;

            MessageValidator mv = new MessageValidator();
            ValidationResult results =mv.Validate(p);
            if (results.IsValid)
            {
                var usermail = User.Identity.Name;
                var writerID = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
                p.SenderID = writerID;
                p.MessageDate = DateTime.Now;
                messageManager.TAdd(p);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult DeleteSendBoxMessage(int id)
        {
            var value = messageManager.TGetByID(id);
            messageManager.TDelete(value);
            return RedirectToAction("SendBox");
        }
        public IActionResult DeleteInBoxMessage(int id)
        {
            var value = messageManager.TGetByID(id);
            messageManager.TDelete(value);
            return RedirectToAction("SendBox");
        }

        public IActionResult InBoxMessageDetails(int id)
        {
            var value = messageManager.MessageListWithSenderUser().Where(x => x.MessageID == id).FirstOrDefault();
            return View(value);
        }

        public IActionResult SendBoxMessageDetails(int id)
        {
            var value = messageManager.MessageListWithSenderUser().Where(x => x.MessageID == id).FirstOrDefault();
            return View(value);
        }
    }
}
