using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
         IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public List<Message> MessageListWithReceiverUser()
        {
            return _messageDal.MessageListWithReceiverUser();
        }

        public List<Message> MessageListWithSenderUser()
        {
            return _messageDal.MessageListWithSenderUser();
        }

        public void TAdd(Message t)
        {
            _messageDal.TAdd(t);
        }

        public void TDelete(Message t)
        {
            _messageDal.TDelete(t);
        }

        public Message TGetByID(int id)
        {
            return _messageDal.TGetByID(id);
        }

        public List<Message> TGetList()
        {
           return _messageDal.TGetList();
        }

        public List<Message> TGetList(Expression<Func<Message, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Message t)
        {
            throw new NotImplementedException();
        }
    }
}
