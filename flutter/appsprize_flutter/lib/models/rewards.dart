import 'package:flutter/foundation.dart';

@immutable
class Rewards {
  final String? currency;
  final int? levels;
  final int? points;

  const Rewards({
    this.currency,
    this.levels,
    this.points,
  });

  factory Rewards.fromJson(Map<String, dynamic> json) {
    return Rewards(
      currency: json['currency'],
      levels: json['levels'],
      points: json['points'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'currency': currency,
      'levels': levels,
      'points': points,
    };
  }
}
