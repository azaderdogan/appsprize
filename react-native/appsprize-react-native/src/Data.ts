

export interface AppsPrizeConfig {
    token: string;
    advertisingId: string;
    userId?: string;
    country?: string;
    language?: string;
    style?: AppsPrizeStyleConfig;
}

export interface AppsPrizeStyleConfig {
    primaryColor?: string;
    secondaryColor?: string;
    typeface?: string;
    bannerDrawable?: string;
    offersTitleText?: string;
    appsTitleText?: string;
    item?: AppsPrizeItemStyling;
    navigation?: AppsPrizeNavigationStyling;
}

export interface AppsPrizeItemStyling {
    backgroundGradientColors?: string[][];
    currencyIconDrawable?: string;
}

export interface AppsPrizeNavigationStyling {
    backgroundColor?: string;
    selectColor?: string;
    deselectColor?: string;
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
