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
            
            if (!string.IsNullOrEmpty(config.gender))
            {
                configBuilder.Call<AndroidJavaObject>("setGender", config.gender);
            }

            if (config.age.HasValue)
            {
                configBuilder.Call<AndroidJavaObject>("setAge", AndroidUtil.ToAndroidInt(config.age.Value));
            }

            if (!string.IsNullOrEmpty(config.uaChannel))
            {
                configBuilder.Call<AndroidJavaObject>("setUaChannel", config.uaChannel);
            }

            if (!string.IsNullOrEmpty(config.uaNetwork))
            {
                configBuilder.Call<AndroidJavaObject>("setUaNetwork", config.uaNetwork);
            }

            if (!string.IsNullOrEmpty(config.adPlacement))
            {
                configBuilder.Call<AndroidJavaObject>("setAdPlacement", config.adPlacement);
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

            if (styleConfig.primaryColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setPrimaryColor", AndroidUtil.ToAndroidColor(styleConfig.primaryColor.Value));
            }
            if (styleConfig.secondaryColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setSecondaryColor", AndroidUtil.ToAndroidColor(styleConfig.secondaryColor.Value));
            }
            if (styleConfig.highlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.highlightColor.Value));
            }

            if (styleConfig.promotionHighlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setPromotionHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.promotionHighlightColor.Value));
            }
            if (styleConfig.cashbackHighlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setCashbackHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.cashbackHighlightColor.Value));
            }
            if (styleConfig.secondChanceHighlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setSecondChanceHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.secondChanceHighlightColor.Value));
            }
            if (styleConfig.commonTaskHighlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setCommonTaskHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.commonTaskHighlightColor.Value));
            }
            if (styleConfig.epicTaskHighlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setEpicTaskHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.epicTaskHighlightColor.Value));
            }
            if (styleConfig.legendaryTaskHighlightColor.HasValue)
            {
                styleConfigBuilder.Call<AndroidJavaObject>("setLegendaryTaskHighlightColor", AndroidUtil.ToAndroidColor(styleConfig.legendaryTaskHighlightColor.Value));
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
