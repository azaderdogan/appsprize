class AppsPrizeStyleConfig {
  final String primaryColor;
  final String secondaryColor;
  final String highlightColor;
  final String promotionHighlightColor;
  final String cashbackHighlightColor;
  final String secondChanceHighlightColor;
  final String commonTaskHighlightColor;
  final String epicTaskHighlightColor;
  final String legendaryTaskHighlightColor;
  final String bannerDrawable;
  final String offersTitleText;
  final String appsTitleText;
  final String typeface;
  final String? currencyIcon;

  const AppsPrizeStyleConfig({
    required this.primaryColor,
    required this.secondaryColor,
    required this.highlightColor,
    required this.promotionHighlightColor,
    required this.cashbackHighlightColor,
    required this.secondChanceHighlightColor,
    required this.commonTaskHighlightColor,
    required this.epicTaskHighlightColor,
    required this.legendaryTaskHighlightColor,
    required this.bannerDrawable,
    required this.offersTitleText,
    required this.appsTitleText,
    required this.typeface,
    this.currencyIcon,
  });

  Map<String, dynamic> toJson() {
    return {
      'primaryColor': primaryColor,
      'secondaryColor': secondaryColor,
      'highlightColor': highlightColor,
      'promotionHighlightColor': promotionHighlightColor,
      'cashbackHighlightColor': cashbackHighlightColor,
      'secondChanceHighlightColor': secondChanceHighlightColor,
      'commonTaskHighlightColor': commonTaskHighlightColor,
      'epicTaskHighlightColor': epicTaskHighlightColor,
      'legendaryTaskHighlightColor': legendaryTaskHighlightColor,
      'bannerDrawable': bannerDrawable,
      'offersTitleText': offersTitleText,
      'appsTitleText': appsTitleText,
      'typeface': typeface,
      if (currencyIcon != null) 'currencyIcon': currencyIcon,
    };
  }

  factory AppsPrizeStyleConfig.fromJson(Map<String, dynamic> json) {
    return AppsPrizeStyleConfig(
      primaryColor: json['primaryColor'] as String,
      secondaryColor: json['secondaryColor'] as String,
      highlightColor: json['highlightColor'] as String,
      promotionHighlightColor: json['promotionHighlightColor'] as String,
      cashbackHighlightColor: json['cashbackHighlightColor'] as String,
      secondChanceHighlightColor: json['secondChanceHighlightColor'] as String,
      commonTaskHighlightColor: json['commonTaskHighlightColor'] as String,
      epicTaskHighlightColor: json['epicTaskHighlightColor'] as String,
      legendaryTaskHighlightColor:
          json['legendaryTaskHighlightColor'] as String,
      bannerDrawable: json['bannerDrawable'] as String,
      offersTitleText: json['offersTitleText'] as String,
      appsTitleText: json['appsTitleText'] as String,
      typeface: json['typeface'] as String,
      currencyIcon: json['currencyIcon'] as String?,
    );
  }
}
