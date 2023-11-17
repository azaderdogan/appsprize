import { AppsPrizeEventType, type AppsPrizeListener, type OnInitializeEvent, type OnInitializeFailedEvent, type OnRewardUpdateEvent } from "./Events"
import AppsPrizeNative, { addEventListener, decodeData, encodeData } from "./AppsPrizeNative"
import type { AppsPrizeConfig } from "./Data";


const init = (config: AppsPrizeConfig, listener: AppsPrizeListener) => {
    let rawConfig = encodeData({ config });
    if (!rawConfig) return;

    addEventListener(AppsPrizeEventType.onInitialize, (nativeEvent) => {
        let event = decodeData(nativeEvent.raw) as OnInitializeEvent
        if (listener.onInitialize) listener.onInitialize(event) 
    })
    addEventListener(AppsPrizeEventType.onInitializeFailed, (nativeEvent) => {
        let event = decodeData(nativeEvent.raw) as OnInitializeFailedEvent
        if (listener.onInitializeFailed) listener.onInitializeFailed(event) 
    })
    addEventListener(AppsPrizeEventType.onRewardUpdate, (nativeEvent) => {
        let event = decodeData(nativeEvent.raw) as OnRewardUpdateEvent
        if (listener.onRewardUpdate) listener.onRewardUpdate(event) 
    })
    AppsPrizeNative?.init(rawConfig)
}

const launch = () => {
    AppsPrizeNative?.launch();
}

export default {
    init,
    launch
};
