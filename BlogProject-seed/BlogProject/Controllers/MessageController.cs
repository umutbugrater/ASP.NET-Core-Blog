using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.Controllers
{
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());
        Context c = new Context();
        public IActionResult InBox()
        {
            //Gelen kutusunda bize mesaj gönderen kişilerin adı olacağı için gönderen(sender) user yapılmalı
            var usermail = User.Identity.Name;
            var writerId = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            var values = messageManager.MessageListWithSenderUser().Where(x => x.ReceiverID == writerId).ToList();
            return View(values);
        }

        public IActionResult SendBox()
        {
            //Gönderilen kutusunda mesaj gönderdiğimiz kişilerin adı geleceği için alıcı user
            var usermail = User.Identity.Name;
            var writerID = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            var values = messageManager.MessageListWithReceiverUser().Where(x => x.SenderID == writerID).ToList();
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = messageManager.TGetByID(id);
            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            List<SelectListItem> receivers = new List<SelectListItem>();
            receivers.Add(new SelectListItem
            {
                Text = "Alıcının e-posta adresini seçiniz..",
                Value = ""
            });
            foreach (var user in c.Users)
            {
                receivers.Add(new SelectListItem
                {
                    Text = user.Email,
                    Value = user.Id.ToString(),
                });
            }
            var usermail = User.Identity.Name;
            receivers.RemoveAll(x => x.Text == usermail);
            ViewBag.receivers = receivers;
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message p)
        {
            MessageValidator mv = new MessageValidator();
            ValidationResult results = mv.Validate(p);
            List<SelectListItem> receivers = new List<SelectListItem>();
            receivers.Add(new SelectListItem
            {
                Text = "Alıcının e-posta adresini seçiniz..",
                Value = ""
            });
            foreach (var user in c.Users)
            {
                receivers.Add(new SelectListItem
                {
                    Text = user.Email,
                    Value = user.Id.ToString(),
                });
            }
            ViewBag.receivers = receivers;

            if (results.IsValid)
            {
                var usermail = User.Identity.Name;
                var writerID = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
                p.SenderID = writerID;
                p.MessageDate = DateTime.Now;
                messageManager.TAdd(p);
                return RedirectToAction("SendBox", "Message");
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

        public IActionResult InBoxDeleteMessage(int id)
        {
            var message = messageManager.TGetByID(id);
            messageManager.TDelete(message);
            return RedirectToAction("InBox");
        }
        public IActionResult SendBoxDeleteMessage(int id)
        {
            var message = messageManager.TGetByID(id);
            messageManager.TDelete(message);
            return RedirectToAction("SendBox");
        }
    }
}
