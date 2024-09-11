using UnityEngine;


namespace AppsPrizeUnity.Platforms.Android
{
    internal static class AppsPrizeConfigAndroid
    {
        public static AndroidJavaObject Create(AppsPrizeConfig config)
        {
            AndroidJavaObject configBuilder = new("com.appsamurai.appsprize.config.AppsPrizeConfig$Builder");

            if (!string.IsNullOrEmpty(config.country))
            {
                configBuilder.Call<AndroidJavaObject>("setCountry", config.country);
            }

            if (!string.IsNullOrEmpty(config.language))
            {
                configBuilder.Call<AndroidJavaObject>("setLanguage", config.language);
            }

            if (config.styleConfig != null)
            {
                configBuilder.Call<AndroidJavaObject>("setStyle", CreateStyleConfig(config.styleConfig));
            }

            AndroidJavaObject appsPrizeConfig = configBuilder.Call<AndroidJavaObject>("build", config.token, config.advertisingId, config.userId);
            return appsPrizeConfig;
        }

        static AndroidJavaObject CreateStyleConfig(AppsPrizeStyleConfig styleConfig)
        {
            AndroidJavaObject styleConfigBuilder = new("com.appsamurai.appsprize.config.style.AppsPrizeStyleConfig$Builder");

            if (styleConfig.primaryColor.HasValue) {
                styleConfigBuilder.Call<AndroidJavaObject>("setPrimaryColor", AndroidUtil.ToAndroidColor(styleConfig.primaryColor.Value));
            }

            if (styleConfig.secondaryColor.HasValue) {
                styleConfigBuilder.Call<AndroidJavaObject>("setSecondaryColor", AndroidUtil.ToAndroidColor(styleConfig.secondaryColor.Value));
            }

            if (styleConfig.highlightColor.HasValue) {
                styleConfigBuilder.Call<AndroidJavaObject>("setHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.highlightColor.Value));
            }

            if (!string.IsNullOrEmpty(styleConfig.offersTitleText))
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setOffersTitleText", styleConfig.offersTitleText);
            }

            if (!string.IsNullOrEmpty(styleConfig.appsTitleText))
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setAppsTitleText", styleConfig.appsTitleText);
            }

            AndroidJavaObject appsPrizeStyleConfig = styleConfigBuilder.Call<AndroidJavaObject>("build");
            return appsPrizeStyleConfig;
        }
    }

}
