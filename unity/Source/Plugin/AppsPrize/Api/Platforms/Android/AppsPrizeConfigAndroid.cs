using UnityEngine;


namespace AppsPrizeUnity.Platforms.Android
{
    internal static class AppsPrizeConfigAndroid
    {
        public static AndroidJavaObject CreateConfig(string token, string advertisingId, string userId, string country = null, string language = null, AndroidJavaObject styleConfig = null)
        {
            AndroidJavaObject configBuilder = new AndroidJavaObject("com.appsamurai.appsprize.config.AppsPrizeConfig$Builder");

            if (!string.IsNullOrEmpty(country))
            {
                configBuilder.Call<AndroidJavaObject>("setCountry", country);
            }

            if (!string.IsNullOrEmpty(language))
            {
                configBuilder.Call<AndroidJavaObject>("setLanguage", language);
            }

            if (styleConfig != null)
            {
                configBuilder.Call<AndroidJavaObject>("setStyle", styleConfig);
            }

            AndroidJavaObject appsPrizeConfig = configBuilder.Call<AndroidJavaObject>("build", token, advertisingId, userId);
            return appsPrizeConfig;
        }

        public static AndroidJavaObject CreateStyleConfig(int primaryColor, int secondaryColor, int highlightColor, string offersTitleText = null, string appsTitleText = null)
        {
            AndroidJavaObject styleConfigBuilder = new AndroidJavaObject("com.appsamurai.appsprize.config.style.AppsPrizeStyleConfig$Builder");

            styleConfigBuilder.Call<AndroidJavaObject>("setPrimaryColor", primaryColor);
            styleConfigBuilder.Call<AndroidJavaObject>("setSecondaryColor", secondaryColor);
            styleConfigBuilder.Call<AndroidJavaObject>("setHighlightColor", highlightColor);

            if (!string.IsNullOrEmpty(offersTitleText))
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setOffersTitleText", offersTitleText);
            }

            if (!string.IsNullOrEmpty(appsTitleText))
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setAppsTitleText", appsTitleText);
            }

            AndroidJavaObject appsPrizeStyleConfig = styleConfigBuilder.Call<AndroidJavaObject>("build");
            return appsPrizeStyleConfig;
        }
    }

}
