using System;
using System.IO;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Gsv.Web.MessageHandlers
{
    public class WorkAppMessageHandler : WorkMessageHandler<WorkAppMessageContext>
    {
        // public IWeixinAppService WeixinAppService { get; set; }
        public WorkAppMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
        }

        public override IWorkResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了消息：" + requestMessage.Content;
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您点击了菜单" + requestMessage.EventKey;
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format("X = {0}, Y = {1}", requestMessage.Location_X, requestMessage.Location_Y);
            return responseMessage;
        }

        public override IWorkResponseMessageBase DefaultResponseMessage(IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "";   //"这是一条没有找到合适回复信息的默认消息。";
            return responseMessage;
         }
    }
}
