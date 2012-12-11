using System.Web.Script.Serialization;

namespace SocialAuth4Net.Hepler
{
    public static class SerilizationHelper
    {
        public static TObject Deserialize<TObject>(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            TObject obj = serializer.Deserialize<TObject>(jsonString);
            return obj;
        }
    }
}