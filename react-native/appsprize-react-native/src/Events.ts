import type { AppRewards } from "./Data";


export enum AppsPrizeEventType {
    onInitialize = "onInitialize",
    onInitializeFailed = "onInitializeFailed",
    onRewardUpdate = "onRewardUpdate"
};


export interface OnInitializeEvent {};

export interface OnInitializeFailedEvent {
    errorMessage?: string
};

export interface OnRewardUpdateEvent {
    rewards?: AppRewards[]
};

export interface AppsPrizeListener{
    onInitialize?: (event: OnInitializeEvent)=>void;
    onInitializeFailed?: (event: OnInitializeFailedEvent)=>void;
    onRewardUpdate?: (event: OnRewardUpdateEvent)=>void;
}
