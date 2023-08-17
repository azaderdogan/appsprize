package com.appsamurai.appsprize_demo

import android.app.Application
import android.util.Log
import com.appsamurai.appsprize.AppReward
import com.appsamurai.appsprize.AppsPrize
import com.appsamurai.appsprize.AppsPrizeListener
import com.appsamurai.appsprize.config.AppsPrizeConfig
import java.util.Locale

const val APPS_PRIZE_APP_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MX0.6YZeCXIC7StDO4wf1m0wQusrVR8ZwxzXIKFVUDYLKP4"
const val USER_ID = "TEST_USER_ID"
const val ADVERTISING_ID = "AA1111AA-A111-11AA-A111-11AAA1A11111"

class MainApplication : Application() {

    override fun onCreate() {
        super.onCreate()

        val config = AppsPrizeConfig.Builder()
            .setUserId(USER_ID)
//            .setTestMode(true)
//            .setCountry(Locale.GERMANY)
            .build(
                APPS_PRIZE_APP_TOKEN,
                ADVERTISING_ID
            )
        AppsPrize.initialize(applicationContext, config, object : AppsPrizeListener {
            override fun onInitialize() {
                Log.d("[AppsPrize]", "MainApplication:onCreate AppsPrize:onInitialize")
            }

            override fun onInitializeFailed(errorMessage: String) {
                Log.d("[AppsPrize]", "MainApplication:onCreate AppsPrize:onInitializeFailed: err: $errorMessage")
            }

            override fun onRewardUpdate(rewards: List<AppReward>) {
                Log.d("[AppsPrize]", "MainApplication:onCreate AppsPrize:onRewardUpdate: $rewards")
            }
        })
    }
}