package com.appsamurai.appsprizedemo;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import androidx.annotation.Nullable;

import com.appsamurai.appsprize.AppReward;
import com.appsamurai.appsprize.AppsPrize;
import com.appsamurai.appsprize.AppsPrizeRewardListener;

import java.util.List;

public class MainActivity extends Activity {

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Button button = findViewById(R.id.offers_button);

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                boolean available = AppsPrize.launchActivity(MainActivity.this);
                if (!available) {
                    Log.e("[AppsPrize]", "AppsPrize not initialized");
                }
            }
        });

        AppsPrize.doReward(MainActivity.this, new AppsPrizeRewardListener() {
            @Override
            public void onSessionReward(List<AppReward> list) {
                // used for handling session reward without AppsPrizeListener.onRewardUpdate event method
            }
        });
    }
}