namespace Tbs.Web.MessageHandlers
{
    public class WeixinUtils
    {
        public static void SendMessage(int tenantId, string toUser, string message)
        {
            // var settingManager = IocManager.Instance.Resolve<SettingManager>();
            // string corpId = settingManager.GetSettingValueForTenantAsync(SettingNames.Weixin.CorpId, tenantId).Result;
            // string secret = settingManager.GetSettingValueForTenantAsync(SettingNames.Weixin.Secret, tenantId).Result;
            // string agentId = settingManager.GetSettingValueForTenantAsync(SettingNames.Weixin.AgentId, tenantId).Result;
            // var accessToken = AccessTokenContainer.GetToken(corpId, secret);
            // MassApi.SendTextAsync(accessToken, toUser, "", "", agentId, message);    
        } 
    }
}