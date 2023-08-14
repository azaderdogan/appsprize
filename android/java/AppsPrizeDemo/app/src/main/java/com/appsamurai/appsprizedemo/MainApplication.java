package com.appsamurai.appsprizedemo;

import android.app.Application;
import android.util.Log;

import androidx.annotation.NonNull;

import com.appsamurai.appsprize.AppReward;
import com.appsamurai.appsprize.AppsPrize;
import com.appsamurai.appsprize.AppsPrizeListener;
import com.appsamurai.appsprize.config.AppsPrizeConfig;

import java.util.List;

public class MainApplication extends Application {

    private static final String APPS_PRIZE_APP_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MX0.6YZeCXIC7StDO4wf1m0wQusrVR8ZwxzXIKFVUDYLKP4";
    private static final String USER_ID = "TEST_USER_ID";
    private static final String ADVERTISING_ID = "AA1111AA-A111-11AA-A111-11AAA1A11111";

    @Override
    public void onCreate() {
        super.onCreate();

        AppsPrizeConfig config = new AppsPrizeConfig.Builder()
                .setUserId(USER_ID)
                .build(APPS_PRIZE_APP_TOKEN, ADVERTISING_ID);

        AppsPrize.initialize(getApplicationContext(), config, new AppsPrizeListener() {
            @Override
            public void onInitialize() {
                Log.d("[AppsPrize]", "MainApplication:onCreate AppsPrize:onInitialize");
            }

            @Override
            public void onInitializeFailed(@NonNull String errorMessage) {
                Log.d("[AppsPrize]", "MainApplication:onCreate AppsPrize:onInitializeFailed: err: " + errorMessage);
            }

            @Override
            public void onRewardUpdate(@NonNull List<AppReward> rewards) {
                Log.d("[AppsPrize]", "MainApplication:onCreate AppsPrize:onRewardUpdate: " + rewards);
            }
        });
    }
}

