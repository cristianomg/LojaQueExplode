using Br.Com.LojaQueExplode.Util.Smtp.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Com.LojaQueExplode.Util.Smtp.Abstract
{
    public interface ISendMailService
    {
        Task SendEmail(string email
                                   , string template
                                   , string subjectEmail
                                   , List<Attachment> attchaments = null
                                   , bool isHtml = false);
    }
}
