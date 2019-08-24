﻿using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar.Context;

namespace Gsv.Web.MessageHandlers
{
    public class WorkAppMessageContext : MessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>
    {
        public WorkAppMessageContext()
        {
            base.MessageContextRemoved += WorkAppMessageContext_MessageContextRemoved;
        }

        void WorkAppMessageContext_MessageContextRemoved(object sender, WeixinContextRemovedEventArgs<IWorkRequestMessageBase, IWorkResponseMessageBase> e)
        {
            /* 注意，这个事件不是实时触发的（当然你也可以专门写一个线程监控）
             * 为了提高效率，根据WeixinContext中的算法，这里的过期消息会在过期后下一条请求执行之前被清除
             */
            var messageContext = e.MessageContext as WorkAppMessageContext;
            if (messageContext == null)
            {
                return;//如果是正常的调用，messageContext不会为null
            }

            //TODO:这里根据需要执行消息过期时候的逻辑，下面的代码仅供参考
            //Log.InfoFormat("{0}的消息上下文已过期",e.OpenId);
            //api.SendMessage(e.OpenId, "由于长时间未搭理客服，您的客服状态已退出！");
        }
    }
}
