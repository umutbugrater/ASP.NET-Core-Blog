using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Admin.ViewComponents
{
    public class AdminOptions : ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var user = c.Users.Where(x => x.Email == usermail).FirstOrDefault();

            var writerID = c.Users.Where(x => x.Email == usermail).Select(x => x.Id).FirstOrDefault();
            var values = messageManager.MessageListWithSenderUser().Where(x => x.ReceiverID == writerID).ToList();
            ViewBag.InBoxCount = values.Count;
            var values2 = messageManager.MessageListWithReceiverUser().Where(x => x.SenderID == writerID).ToList();
            ViewBag.SendBoxCount = values2.Count;
            return View(user);
        }
    }
}
