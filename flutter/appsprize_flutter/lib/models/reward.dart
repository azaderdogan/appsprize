import 'package:flutter/foundation.dart';
import 'rewards.dart';

@immutable
class Reward {
  final List<Rewards>? rewards;

  const Reward({this.rewards});

  factory Reward.fromJson(Map<String, dynamic> json) {
    return Reward(
      rewards: json['rewards'] != null
          ? List<Rewards>.from(
              json['rewards'].map((v) => Rewards.fromJson(v)),
            )
          : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      if (rewards != null) 'rewards': rewards!.map((v) => v.toJson()).toList(),
    };
  }
}
