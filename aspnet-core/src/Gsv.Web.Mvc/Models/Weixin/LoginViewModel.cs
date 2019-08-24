using System.Collections.Generic;

namespace Gsv.Web.Models.Weixin
{
    public class LoginViewModel
    {
        public string WorkerCn { get; set; }

        public string Password { get; set; }

        public int ObjectId { get; set; }

        public List<LoginObject> Objects { get; set; }
        public string ReturnUrl { get; set; }
    }
}