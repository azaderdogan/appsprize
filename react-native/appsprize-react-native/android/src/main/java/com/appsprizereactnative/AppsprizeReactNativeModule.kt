package com.appsprizereactnative

import android.graphics.Color
import android.os.Handler
import android.util.Log
import com.appsamurai.appsprize.AppReward
import com.appsamurai.appsprize.AppsPrize
import com.appsamurai.appsprize.AppsPrizeListener
import com.appsamurai.appsprize.config.AppsPrizeConfig
import com.appsamurai.appsprize.config.style.AppsPrizeStyleConfig
import com.facebook.react.bridge.Arguments
import com.facebook.react.bridge.Callback
import com.facebook.react.bridge.Promise
import com.facebook.react.bridge.ReactApplicationContext
import com.facebook.react.bridge.ReactContextBaseJavaModule
import com.facebook.react.bridge.ReactMethod
import com.facebook.react.modules.core.DeviceEventManagerModule

class AppsprizeReactNativeModule(reactContext: ReactApplicationContext): ReactContextBaseJavaModule(reactContext) {

    override fun getName(): String {
        return NAME
    }

    @ReactMethod
    fun init(raw: String) {
        Handler(reactApplicationContext.mainLooper).post {
            val map = jsonStringToMap(raw)?.get("config") as? Map<String, Any?> ?: return@post
            Log.d("[AppsPrizeAndroid]", "init with config $map")

            val appsPrizeConfig = buildConfig(map) ?: return@post
            AppsPrize.initialize(reactApplicationContext, appsPrizeConfig, object: AppsPrizeListener {
                override fun onInitialize() {
                    Log.d("[AppsPrizeAndroid]", "onInitialize")
                    sendEvent(Events.OnInitialize)
                }

                override fun onInitializeFailed(errorMessage: String) {
                    Log.d("[AppsPrizeAndroid]", "onInitializeFailed: $errorMessage")
                    sendEvent(Events.OnInitializeFailed, mapOf(
                        "errorMessage" to errorMessage
                    ))
                }

                override fun onRewardUpdate(rewards: List<AppReward>) {
                    Log.d("[AppsPrizeAndroid]", " onRewardUpdate: $rewards")
                    sendEvent(Events.OnRewardUpdate, mapOf(
                        "rewards" to rewards.map {
                            createAppReward(it)
                        }
                    ))
                }
            })
        }
    }

    @ReactMethod
    fun launch(promise: Promise) {
        Log.d("[AppsPrizeAndroid]", " launch()")
        val activity = currentActivity ?: run {
            promise.reject(Exception("AppsPrize:Android: no current activity found"))
            return
        }
        Handler(activity.mainLooper).post {
            val result = AppsPrize.launchActivity(activity)
            promise.resolve(result)
        }
    }

    @ReactMethod
    fun doReward(callback: Callback) {
        Log.d("[AppsPrizeAndroid]", " doReward()")
        val activity = currentActivity ?: return
        Handler(activity.mainLooper).post {
            AppsPrize.doReward(activity) { rewards ->
                val appRewardsMap = rewards.mapNotNull { createAppReward(it) }
                callback.invoke(mapToJsonString(mapOf(
                    "rewards" to appRewardsMap
                )))
            }
        }
    }

    @ReactMethod
    fun hasPermissions(promise: Promise) {
        Log.d("[AppsPrizeAndroid]", " hasPermissions()")
        val activity = currentActivity ?: run {
            promise.reject(Exception("AppsPrize:Android: no current activity found"))
            return
        }
        Handler(activity.mainLooper).post {
            val result = AppsPrize.hasPermissions(activity)
            promise.resolve(result)
        }
    }

    @ReactMethod
    fun requestPermission(promise: Promise) {
        Log.d("[AppsPrizeAndroid]", " requestPermission()")
        val activity = currentActivity ?: run {
            promise.reject(Exception("AppsPrize:Android: no current activity found"))
            return
        }
        Handler(activity.mainLooper).post {
            val result = AppsPrize.requestPermission(activity)
            promise.resolve(result)
        }
    }

    @ReactMethod
    fun addListener(eventName: String?) {
        Log.d("[AppsPrizeAndroid]", " addListener:eventName:${eventName}")
    }

    @ReactMethod
    fun removeListeners(count: Int?) {
        Log.d("[AppsPrizeAndroid]", " removeListeners:count:${count}")
    }

    private fun createAppReward(reward: AppReward): Map<String, Any?> {
        return mapOf(
            "rewards" to reward.rewards.map {
                mapOf(
                    "currency" to it.currency,
                    "level" to it.level,
                    "points" to it.points,
                )
            }
        )
    }

    private fun buildConfig(map: Map<String, Any?>): AppsPrizeConfig? {
        val token = map["token"] as? String ?: return null
        val advertisingId =  map["advertisingId"] as? String ?: return null
        val userId =  map["userId"] as? String ?: return null
        val country = map["country"] as? String
        val language = map["language"] as? String

        return AppsPrizeConfig.Builder()
            .setCountry(country)
            .setLanguage(language)
            .setStyle(buildStyleConfig(map["style"] as? Map<String, Any?>))
            .build(
                token,
                advertisingId,
                userId
            )
    }

    private fun buildStyleConfig(map: Map<String, Any?>?): AppsPrizeStyleConfig? {
        map ?: return null
        val primaryColor = (map["primaryColor"] as? String)?.let { Color.parseColor(it) }
        val secondaryColor = (map["secondaryColor"] as? String)?.let { Color.parseColor(it) }
        val highlightColor = (map["highlightColor"] as? String)?.let { Color.parseColor(it) }
        val typeface = getTypeface(reactApplicationContext, map["typeface"] as? String)
        val bannerDrawable = getDrawable(reactApplicationContext, map["bannerDrawable"] as? String)
        val offersTitleText = map["offersTitleText"] as? String
        val appsTitleText = map["appsTitleText"] as? String
        val currencyIcon = getDrawable(reactApplicationContext, map["currencyIcon"] as? String)

        return AppsPrizeStyleConfig.Builder()
            .setPrimaryColor(primaryColor)
            .setSecondaryColor(secondaryColor)
            .setHighlightColor(highlightColor)
            .setTypeface(typeface)
            .setBannerDrawable(bannerDrawable)
            .setOffersTitleText(offersTitleText)
            .setAppsTitleText(appsTitleText)
            .setCurrencyIcon(currencyIcon)
            .build()
    }

    private fun sendEvent(event: Events, map: Map<String, Any?>? = null) {
        val raw = mapToJsonString(map)
        reactApplicationContext
            .getJSModule(DeviceEventManagerModule.RCTDeviceEventEmitter::class.java)
            .emit(event.description, Arguments.createMap().apply {
                putString("raw", raw)
            })
    }

    private enum class Events(val description: String) {
        OnInitialize("onInitialize"),
        OnInitializeFailed("onInitializeFailed"),
        OnRewardUpdate("onRewardUpdate");
    }

    companion object {
        const val NAME = "AppsprizeReactNative"
    }
}

