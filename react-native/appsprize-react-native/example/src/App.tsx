import * as React from 'react';

import { StyleSheet, View, Button } from 'react-native';
import AppsPrize, { type AppsPrizeConfig, type AppsPrizeStyleConfig } from 'appsprize-react-native';


const buildConfig = (customization: Boolean): AppsPrizeConfig => {
  return {
    token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTV9.XmX_0neDCxs7FKSSOnTk1KRSN8FSm9lBhJhlOAX0HHs",
    userId: "melih",
    advertisingId: "AA1111AA-A111-11AA-A111-11AAA1A11111",
    country: "US",
    language: "en",
    style: customization ? styleConfig : undefined
  };
}

let styleConfig: AppsPrizeStyleConfig =  {
  primaryColor: "#265073",
  secondaryColor: "#F1FADA",
  bannerDrawable: "custom_banner",
  offersTitleText: "Offer Apps",
  appsTitleText: "Active Apps",
  typeface: "kodemono.ttf",
  // currencyIcon: undefined,
}

export default function App() {

  React.useEffect(() => {
    AppsPrize.init(buildConfig(false), {
      onInitialize: () => {
        console.log("AppsPrize:TS:onInitialize")
      },
      onInitializeFailed: (event) => {
        console.log(`AppsPrize:TS:onInitializeFailed:${JSON.stringify(event)}`)
      },
      onRewardUpdate: (event) => {
        console.log(`AppsPrize:TS:onRewardUpdate:${JSON.stringify(event)}`)
      }
    })
  }, [])

  return (
    <View style={styles.container}>
      <Button
        title='AppsPrize init'
        onPress={() => {
          AppsPrize.init(buildConfig(true), {
            onInitialize: (event) => {
              console.log("AppsPrize:TS:onInitialize")
            },
            onInitializeFailed: (event) => {
              console.log(`AppsPrize:TS:onInitializeFailed:${JSON.stringify(event)}`)
            },
            onRewardUpdate: (event) => {
              console.log(`AppsPrize:TS:onRewardUpdate:${JSON.stringify(event)}`)
            }
          })
        }}
      />
      <Button
        title='AppsPrize Launch'
        onPress={() => {
          AppsPrize.launch()
        }}
      />
      <Button
        title='AppsPrize hasPermissions'
        onPress={() => {
          AppsPrize.hasPermissions()
            .then(has => console.log(`AppsPrize:TS:hasPermissions - ${has}`))
            .catch(err => console.log(`AppsPrize:TS:hasPermissions - ${err}`))
        }}
      />
      <Button
        title='AppsPrize requestPermission'
        onPress={() => {
          AppsPrize.requestPermission()
            .then(has => console.log(`AppsPrize:TS:requestPermission - ${has}`))
            .catch(err => console.log(`AppsPrize:TS:requestPermission - ${err}`))
        }}
      />
      <Button
        title='AppsPrize doReward'
        onPress={() => {
          AppsPrize.doReward((rewards) => {
            console.log(`AppsPrize:TS:doReward - ${JSON.stringify(rewards)}`)
          })
        }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    width: "auto",
  },
  box: {
    width: 60,
    height: 60,
    marginVertical: 20,
  },
});
