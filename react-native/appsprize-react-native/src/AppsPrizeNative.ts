import { Platform, NativeModules, NativeEventEmitter, type EmitterSubscription } from "react-native";
import { AppsPrizeEventType } from "./Events";

const LINKING_ERROR =
    `The package 'appsprize-react-native' doesn't seem to be linked. Make sure: \n\n` +
    Platform.select({ ios: "- You have run 'pod install'\n", default: '' }) +
    '- You rebuilt the app after installing the package\n' +
    '- You are not using Expo Go\n';

interface AppsprizeReactNativeInterface {
    init(raw: string): void;
    launch(): Promise<boolean>;
    doReward(callback: (raw: string)=>void): void;
    hasPermissions(): Promise<boolean>;
    requestPermission(): Promise<boolean>
}

const AppsPrizeNative = Platform.OS === "android" ? (
        NativeModules.AppsprizeReactNative ? NativeModules.AppsprizeReactNative : new Proxy({},{ get() { throw new Error(LINKING_ERROR); } } )        
    ) as AppsprizeReactNativeInterface : undefined;
    
interface AppsPrizeEvent {
    raw: string
}
    
interface OnAppsPrizeEvent {
    (raw: AppsPrizeEvent): void
};

const getEventEmitter = () => (
    AppsPrizeNative ? new NativeEventEmitter(NativeModules.AppsprizeReactNative) : undefined
)
const _eventsSubscriptions = new Map<OnAppsPrizeEvent, EmitterSubscription>();

export const addEventListener = (event: AppsPrizeEventType, handler: OnAppsPrizeEvent) => {
    let isValidEventType = Object.keys(AppsPrizeEventType).includes(event)
    if (isValidEventType) {
        let listener = getEventEmitter()?.addListener(event, handler)
        if (listener) {
            _eventsSubscriptions.set(handler, listener)
        }
        return {
            remove: () => removeEventListener(handler),
        }
    } else {
        console.warn(`Trying to subscribe to unknown event: "${event}"`)
        return {
            remove: () => {},
        }
    }
}


export const removeEventListener = (handler: OnAppsPrizeEvent) => {
    const listener = _eventsSubscriptions.get(handler)
    if (!listener) {
        return
    }
    listener.remove()
    _eventsSubscriptions.delete(handler)
}

export const removeAllListeners = () => {
    _eventsSubscriptions.forEach((listener, key, map) => {
        listener.remove()
        map.delete(key)
    })
}

export const decodeData = (raw: string) => {
    try {
        return JSON.parse(raw)
    } catch {
        return undefined
    }
}


export const encodeData = (raw?: any) => {
    if (raw) {
        let rawConfig = JSON.stringify({
            "raw": raw
        })
        return rawConfig
    }
    return undefined
}

export default AppsPrizeNative;