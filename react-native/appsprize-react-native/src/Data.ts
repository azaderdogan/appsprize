

export interface AppsPrizeConfig {
    token: string
    advertisingId: string
    userId?: string
    country?: boolean
    testMode?: boolean
}


export interface RewardLevel {
    currency: string;
    level: number;
    points: number;
    time: number;
}

export interface AppRewards {
    rewards: RewardLevel[];
}
