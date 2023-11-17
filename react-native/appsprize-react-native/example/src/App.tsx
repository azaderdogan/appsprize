import * as React from 'react';

import { StyleSheet, View, Button } from 'react-native';
import AppsPrize from 'appsprize-react-native';


export default function App() {

  React.useEffect(() => {
    AppsPrize.init({
      token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MTV9.XmX_0neDCxs7FKSSOnTk1KRSN8FSm9lBhJhlOAX0HHs",
      userId: "melih",
      advertisingId: "AA1111AA-A111-11AA-A111-11AAA1A11111"
    }, {
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
  }, []);

  return (
    <View style={styles.container}>
      <Button
        title='AppsPrize Launch'
        onPress={() => {
        AppsPrize.launch()
      }} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  box: {
    width: 60,
    height: 60,
    marginVertical: 20,
  },
});
