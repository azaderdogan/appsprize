import type { AppRewards, AppsPrizeNotification } from "./Data";


export enum AppsPrizeEventType {
    onInitialize = "onInitialize",
    onInitializeFailed = "onInitializeFailed",
    onRewardUpdate = "onRewardUpdate",
    onNotification = "onNotification",
}


export interface OnInitializeEvent {}

export interface OnInitializeFailedEvent {
    errorMessage?: string;
}

export interface OnRewardUpdateEvent {
    rewards?: AppRewards[];
}

export interface OnNotificationEvent {
    notifications?: AppsPrizeNotification[];
}

export interface AppsPrizeListener{
    onInitialize?: (event: OnInitializeEvent)=>void;
    onInitializeFailed?: (event: OnInitializeFailedEvent)=>void;
    onRewardUpdate?: (event: OnRewardUpdateEvent)=>void;
    onNotification?: (event: OnNotificationEvent)=>void;
}
