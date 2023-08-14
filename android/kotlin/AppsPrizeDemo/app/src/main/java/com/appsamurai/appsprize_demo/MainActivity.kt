package com.appsamurai.appsprize_demo

import android.app.Activity
import android.os.Bundle
import android.util.Log
import com.appsamurai.appsprize.AppsPrize
import com.appsamurai.appsprize_demo.databinding.ActivityMainBinding

class MainActivity : Activity() {

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.offersButton.setOnClickListener {
            val available = AppsPrize.launchActivity(this@MainActivity)
            if (!available) {
                Log.e("[AppsPrize]", "AppsPrize not initialized")
            }
        }

        AppsPrize.doReward(this) { sessionReward ->
            // used for handling session reward without AppsPrizeListener.onRewardUpdate event method
        }
    }
}