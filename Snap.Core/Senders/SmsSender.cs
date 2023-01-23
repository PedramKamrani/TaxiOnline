using Kavenegar;
namespace Snap.Core.Senders
{
    public static class SmsSender
    {
        public static void VerifySender(string to, string template, string token)
        {
            var api = new KavenegarApi("");
            string receptor = to;
            string apiTemplate=template;
            string apiToken=token;
           api.VerifyLookup(receptor,apiTemplate,apiToken);
        }
    }
}
