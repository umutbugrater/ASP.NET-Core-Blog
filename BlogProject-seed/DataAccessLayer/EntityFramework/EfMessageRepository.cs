using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessageRepository : EfGenericRepository<Message>, IMessageDal
    {
        public List<Message> MessageListWithReceiverUser()
        {
            using (var c = new Context())
            {
                return c.Messages.Include(x => x.ReceiverUser).ToList();
            }
        }

        public List<Message> MessageListWithSenderUser()
        {
            using (var c = new Context())
            {
                return c.Messages.Include(x => x.SenderUser).ToList();
            }
        }

    }
}
