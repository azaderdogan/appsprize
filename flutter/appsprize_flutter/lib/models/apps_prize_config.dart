import 'package:appsprize_flutter/models/apps_prize_style_config.dart';
import 'package:flutter/foundation.dart';

@immutable
class AppsPrizeConfig {
  final String token;
  final String userId;
  final String country;
  final String language;
  final String gender;
  final int age;
  final String uaChannel;
  final String uaNetwork;
  final String adPlacement;
  final AppsPrizeStyleConfig? style;

  const AppsPrizeConfig({
    required this.token,
    required this.userId,
    required this.country,
    required this.language,
    required this.gender,
    required this.age,
    required this.uaChannel,
    required this.uaNetwork,
    required this.adPlacement,
    this.style,
  });

  Map<String, dynamic> toJson() {
    return {
      'token': token,
      'userId': userId,
      'country': country,
      'language': language,
      'gender': gender,
      'age': age,
      'uaChannel': uaChannel,
      'uaNetwork': uaNetwork,
      'adPlacement': adPlacement,
      if (style != null) 'style': style,
    };
  }
}
