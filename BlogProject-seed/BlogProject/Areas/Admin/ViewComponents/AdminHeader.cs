using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Areas.Admin.ViewComponents
{
    public class AdminHeader : ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EfMessageRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var writerId = c.Users.Where(x => x.Email == usermail).FirstOrDefault().Id;
            var values = messageManager.MessageListWithSenderUser().Where(x => x.ReceiverID == writerId);
            return View(values);
        }
    }
}
