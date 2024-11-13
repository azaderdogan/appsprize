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
        public readonly string gender = null;
        public readonly int? age = null;
        public readonly string uaChannel = null;
        public readonly string uaNetwork = null;
        public readonly string adPlacement = null;
        public readonly AppsPrizeStyleConfig styleConfig = null;

        public AppsPrizeConfig(
            string token,
            string advertisingId,
            string userId,
            string country = null,
            string language = null,
            string gender = null,
            int? age = null,
            string uaChannel = null,
            string uaNetwork = null,
            string adPlacement = null,
            AppsPrizeStyleConfig styleConfig = null
        ) {
            this.token = token;
            this.advertisingId = advertisingId;
            this.userId = userId;
            this.country = country;
            this.language = language;
            this.gender = gender; 
            this.age = age; 
            this.uaChannel = uaChannel; 
            this.uaNetwork = uaNetwork; 
            this.adPlacement = adPlacement; 
            this.styleConfig = styleConfig;
        }
    }

    public class AppsPrizeStyleConfig
    {
       public readonly Color? primaryColor = null;
       public readonly Color? secondaryColor = null;
       public readonly Color? highlightColor = null;
       public readonly Color? promotionHighlightColor = null;
       public readonly Color? cashbackHighlightColor = null;
       public readonly Color? secondChanceHighlightColor = null;
       public readonly Color? commonTaskHighlightColor = null;
       public readonly Color? epicTaskHighlightColor = null;
       public readonly Color? legendaryTaskHighlightColor = null;
       public readonly string offersTitleText = null;
       public readonly string appsTitleText = null;
       

       public AppsPrizeStyleConfig(
            Color? primaryColor = null,
            Color? secondaryColor = null,
            Color? highlightColor = null,
            Color? promotionHighlightColor = null,
            Color? cashbackHighlightColor = null,
            Color? secondChanceHighlightColor = null,
            Color? commonTaskHighlightColor = null,
            Color? epicTaskHighlightColor = null,
            Color? legendaryTaskHighlightColor = null,
            string offersTitleText = null,
            string appsTitleText = null
       ) {
            this.primaryColor = primaryColor;
            this.secondaryColor = secondaryColor;
            this.highlightColor = highlightColor;
            this.promotionHighlightColor = promotionHighlightColor;
            this.cashbackHighlightColor = cashbackHighlightColor;
            this.secondChanceHighlightColor = secondChanceHighlightColor;
            this.commonTaskHighlightColor = commonTaskHighlightColor;
            this.epicTaskHighlightColor = epicTaskHighlightColor;
            this.legendaryTaskHighlightColor = legendaryTaskHighlightColor;
            this.offersTitleText = offersTitleText;
            this.appsTitleText = appsTitleText;
       }
    }

}