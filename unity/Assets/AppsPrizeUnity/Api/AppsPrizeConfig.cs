using UnityEngine;


namespace AppsPrizeUnity
{
    public class AppsPrizeConfig
    {
        public readonly string token;
        public readonly string advertisingId;
        public readonly string userId;
        public readonly string country = null;
        public readonly string language = null;
        public readonly AppsPrizeStyleConfig styleConfig = null;

        public AppsPrizeConfig(
            string token,
            string advertisingId,
            string userId,
            string country = null,
            string language = null,
            AppsPrizeStyleConfig styleConfig = null
        ) {
            this.token = token;
            this.advertisingId = advertisingId;
            this.userId = userId;
            this.country = country;
            this.language = language;
            this.styleConfig = styleConfig;
        }
    }

    public class AppsPrizeStyleConfig
    {
       public readonly Color? primaryColor = null;
       public readonly Color? secondaryColor = null;
       public readonly Color? highlightColor = null;
       public readonly string offersTitleText = null;
       public readonly string appsTitleText = null;

       public AppsPrizeStyleConfig(
            Color? primaryColor = null,
            Color? secondaryColor = null,
            Color? highlightColor = null,
            string offersTitleText = null,
            string appsTitleText = null
       ) {
            this.primaryColor = primaryColor;
            this.secondaryColor = secondaryColor;
            this.highlightColor = highlightColor;
            this.offersTitleText = offersTitleText;
            this.appsTitleText = appsTitleText;
       }
    }

}