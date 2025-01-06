package com.example.appsprize_flutter

import android.graphics.Color
import android.os.Handler
import android.util.Log
import com.appsamurai.appsprize.AppReward
import com.appsamurai.appsprize.AppsPrize
import com.appsamurai.appsprize.AppsPrizeListener
import com.appsamurai.appsprize.AppsPrizeNotification
import com.appsamurai.appsprize.config.AppsPrizeConfig
import com.appsamurai.appsprize.config.style.AppsPrizeStyleConfig
import android.content.Context
import android.app.Activity
import io.flutter.plugin.common.MethodChannel
import io.flutter.plugin.common.MethodCall
import android.os.Looper
import io.flutter.plugin.common.EventChannel
import io.flutter.plugin.common.EventChannel.EventSink


class AppsprizeFlutterModule(private val context: Activity) : MethodChannel.MethodCallHandler {
   
    private var eventSink: EventSink? = null

    override fun onMethodCall(call: MethodCall, result: MethodChannel.Result) {
        when (call.method) {
            "init" -> {
                // log
                Log.d("[AppsPrizeAndroid]", "init")
                val rawConfig = call.argument<String>("config")
                if (rawConfig == null) {
                    result.error("MISSING_CONFIG", "Config is required", null)
                    return
                }
                Log.d("[AppsPrizeAndroid]", "init with config $rawConfig")
                // Initialize the SDK with the provided config
                init(rawConfig,
                    onSuccess = { result.success(null) },
                    onError = { result.error("INIT_ERROR", it, null) }
                )
            }
            "launch" -> {

                val activity = context as? Activity
                if (activity == null) {
                    result.error("NO_ACTIVITY", "Activity not found", null)
                    return
                }
                launch(activity) { success ->
                    result.success(success)
                }
            }
            "open" -> {
                val activity = context as? Activity
                if (activity == null) {
                    result.error("NO_ACTIVITY", "Activity not found", null)
                    return
                }
                val campaignId = call.argument<Int>("campaignId")
                if (campaignId == null) {
                    result.error("INVALID_ARGUMENT", "Campaign ID is required", null)
                    return
                }
                open(activity, campaignId) { success ->
                    result.success(success)
                }
            }
            "doReward" -> {
               
                val activity = context as? Activity

                if (activity == null) {
                    result.error("NO_ACTIVITY", "Activity not found", null)
                    return
                }
                doReward(activity) { rewards ->
                    result.success(rewards)
                }
            }
            "hasPermissions" -> {
                val activity = context as? Activity
                if (activity == null) {
                    result.error("NO_ACTIVITY", "Activity not found", null)
                    return
                }
                result.success(hasPermissions(activity))
            }
            "requestPermission" -> {
                val activity = context as? Activity
                if (activity == null) {
                    result.error("NO_ACTIVITY", "Activity not found", null)
                    return
                }
                result.success(requestPermission(activity))
            }
            else -> result.notImplemented()
        }
    }

    fun createEventChannelHandler(): EventChannel.StreamHandler {
        return object : EventChannel.StreamHandler {
            override fun onListen(arguments: Any?, events: EventSink?) {
                eventSink = events
            }

            override fun onCancel(arguments: Any?) {
                eventSink = null
            }
        }
    }

    private fun sendEvent(eventName: String, data: Map<String, Any?>) {
        Log.d("[AppsPrizeAndroid]", "sendEvent $eventName with data $data")
        eventSink?.success(mapOf("event" to eventName, "data" to data))
    }

fun init(raw: String, onSuccess: () -> Unit, onError: (String) -> Unit) {
    Log.d("[AppsPrizeAndroid]", "init called")
    // Run initialization on the main thread

    Handler(context.mainLooper).post {
        Log.d("[AppsPrizeAndroid]", "init post  ")
        val map = jsonStringToMap(raw) as? Map<String, Any?> ?: return@post
        Log.d("[AppsPrizeAndroid]", "init with config $map")

        val appsPrizeConfig = buildConfig(map) ?: return@post
        Log.d("[AppsPrizeAndroid]", "init with new config $appsPrizeConfig")
        AppsPrize.initialize(context, appsPrizeConfig, object : AppsPrizeListener {
            override fun onInitialize() {
                Log.d("[AppsPrizeAndroid]", "onInitialize")
                sendEvent("onInitialize", emptyMap())
                onSuccess()
            }

            override fun onInitializeFailed(errorMessage: String) {
                Log.d("[AppsPrizeAndroid]", "onInitializeFailed: $errorMessage")
                sendEvent("onInitializeFailed", mapOf("errorMessage" to errorMessage))
                onError(errorMessage)
            }

            override fun onRewardUpdate(rewards: List<AppReward>) {
                Log.d("[AppsPrizeAndroid]", "onRewardUpdate: $rewards")
                val rewardList = rewards.map { createAppReward(it) }

                sendEvent("onRewardUpdate", mapOf("rewards" to rewardList))
            }

            override fun onNotification(notifications: List<AppsPrizeNotification>) {
                Log.d("[AppsPrizeAndroid]", "onNotification: $notifications")
                val notificationList = notifications.map { createNotification(it) }
                sendEvent("onNotification", mapOf("notifications" to notificationList))
            }
        })
    }
}

    private fun createNotification(reward: AppsPrizeNotification): Map<String, Any?> {
        return mapOf(
            "id" to reward.id,
            "campaignId" to reward.campaignId,
            "appName" to reward.appName,
            "description" to reward.description,
            "hasRead" to reward.hasRead,
            "iconUrl" to reward.iconUrl,
            "timestamp" to reward.timestamp,
        )
    }


    fun launch(activity: Activity, onResult: (Boolean) -> Unit) {
        Log.d("[AppsPrizeAndroid]", "launch")
        // Run the launch on the main thread
        Handler(activity.mainLooper).post {
            val result = AppsPrize.launchActivity(activity)
            Log.d("[AppsPrizeAndroid]", "launch result $result")
            onResult(result)
        }
    }

    fun open(activity: Activity, campaignId: Int, onResult: (Boolean) -> Unit) {
        // Run the open on the main thread
        Handler(activity.mainLooper).post {
            Log.d("[AppsPrizeAndroid]", "open with campaignId $campaignId")
            val result = AppsPrize.open(activity, campaignId)
            Log.d("[AppsPrizeAndroid]", "open result $result")
            onResult(result)
        }
    }

    fun doReward(activity: Activity, onReward: (List<Map<String, Any?>>) -> Unit) {
        Log.d("[AppsPrizeAndroid]", "doReward")
        // Run the doReward on the main thread
        Handler(activity.mainLooper).post {
            Log.d("[AppsPrizeAndroid]", "doReward post")
            AppsPrize.doReward(activity) { rewards ->
                val appRewardsMap = rewards.map { createAppReward(it) }
                Log.d("[AppsPrizeAndroid]", "doReward rewards $appRewardsMap")
                onReward(appRewardsMap)
            }
        }
    }

    // Utility function to check for required permissions
    fun hasPermissions(activity: Activity): Boolean {
        return AppsPrize.hasPermissions(activity)
    }

    // Request permissions if necessary
    fun requestPermission(activity: Activity): Boolean {
        return AppsPrize.requestPermission(activity)
    }

    private fun createAppReward(reward: AppReward): Map<String, Any?> {
        Log.d("[AppsPrizeAndroid]", "createAppReward with reward $reward")
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

    // Build the AppsPrizeConfig using the provided map
    private fun buildConfig(map: Map<String, Any?>): AppsPrizeConfig? {
        Log.d("[AppsPrizeAndroid]", "buildConfig start with map $map")
        val token = map["token"] as? String ?: return null
        val advertisingId = map["advertisingId"] as? String ?: return null
        val userId = map["userId"] as? String ?: return null
        val country = map["country"] as? String
        val language = map["language"] as? String
        val gender = map["gender"] as? String?
        val age = map["age"] as? Int?
        val uaChannel = map["uaChannel"] as? String?
        val uaNetwork = map["uaNetwork"] as? String?
        val adPlacement = map["adPlacement"] as? String?
Log.d("[AppsPrizeAndroid]", "buildConfig with token $token")
        return AppsPrizeConfig.Builder()
            .setCountry(country)
            .setLanguage(language)
            .setStyle(buildStyleConfig(map["style"] as? Map<String, Any?>))
            .setGender(gender)
            .setAge(age)
            .setUaChannel(uaChannel)
            .setUaNetwork(uaNetwork)
            .setAdPlacement(adPlacement)
            .build(
                token,
                advertisingId,
                userId
            )
    }

    // Build the style configuration
    private fun buildStyleConfig(map: Map<String, Any?>?): AppsPrizeStyleConfig? {
        map ?: return null
        Log.d("[AppsPrizeAndroid]", "buildStyleConfig with map $map")
        val primaryColor = (map["primaryColor"] as? String)?.let { Color.parseColor(it) }
        val secondaryColor = (map["secondaryColor"] as? String)?.let { Color.parseColor(it) }
        val highlightColor = (map["highlightColor"] as? String)?.let { Color.parseColor(it) }

        return AppsPrizeStyleConfig.Builder()
            .setPrimaryColor(primaryColor)
            .setSecondaryColor(secondaryColor)
            .setHighlightColor(highlightColor)
            .build()
    }
}
