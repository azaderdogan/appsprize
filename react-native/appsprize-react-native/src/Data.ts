

export interface AppsPrizeConfig {
    token: string;
    advertisingId: string;
    userId: string;
    country?: string;
    language?: string;
    style?: AppsPrizeStyleConfig;
}

export interface AppsPrizeStyleConfig {
    primaryColor?: string;
    secondaryColor?: string;
    highlightColor?: string;
    typeface?: string;
    bannerDrawable?: string;
    offersTitleText?: string;
    appsTitleText?: string;
    currencyIcon?: string;
}

export interface RewardLevel {
    level: number;
    points: number;
    currency: string;
}

export interface AppRewards {
    rewards: RewardLevel[];
}
