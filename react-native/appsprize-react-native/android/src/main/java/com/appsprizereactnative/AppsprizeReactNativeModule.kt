package com.appsprizereactnative

import android.os.Handler
import android.util.Log
import com.appsamurai.appsprize.AppReward
import com.appsamurai.appsprize.AppsPrize
import com.appsamurai.appsprize.AppsPrizeListener
import com.appsamurai.appsprize.RewardLevel
import com.appsamurai.appsprize.config.AppsPrizeConfig
import com.facebook.react.bridge.Arguments
import com.facebook.react.bridge.Callback
import com.facebook.react.bridge.Promise
import com.facebook.react.bridge.ReactApplicationContext
import com.facebook.react.bridge.ReactContextBaseJavaModule
import com.facebook.react.bridge.ReactMethod
import com.facebook.react.modules.core.DeviceEventManagerModule
import java.util.Locale

class AppsprizeReactNativeModule(reactContext: ReactApplicationContext): ReactContextBaseJavaModule(reactContext) {

    override fun getName(): String {
        return NAME
    }

    @ReactMethod
    fun init(raw: String) {
        Handler(reactApplicationContext.mainLooper).post {
            val map = jsonStringToMap(raw)?.get("config") as? Map<String, Any?> ?: return@post
            Log.d("[AppsPrizeAndroid]", "init with config $map")
            val token = map["token"] as? String ?: return@post
            val advertisingId =  map["advertisingId"] as? String ?: return@post
            val userId =  map["userId"] as? String
            val country = map["country"] as? String
            val testMode = map["testMode"] as? Boolean

            val appsPrizeConfig = buildConfig(token, advertisingId, userId, country, testMode)
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
                    "time" to it.time,
                )
            }
        )
    }

    private fun buildConfig(token: String, advertisingId: String, userId: String?, country: String?, testMode: Boolean?): AppsPrizeConfig {
        var builder = AppsPrizeConfig.Builder()
        userId?.let { builder = builder.setUserId(it) }
        country?.let {  builder = builder.setCountry(Locale.forLanguageTag(it)) }
        testMode?.let { builder = builder.setTestMode(it) }
        return builder.build(
            token,
            advertisingId
        )
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
