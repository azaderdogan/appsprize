import 'package:appsprize_flutter/appsprize_flutter_method_channel.dart';
import 'package:appsprize_flutter/models/apps_prize_config.dart';
import 'package:appsprize_flutter/models/rewards.dart';

import 'appsprize_flutter_platform_interface.dart';

class AppsprizeFlutter {
  Future<String?> getPlatformVersion() {
    return AppsprizeFlutterPlatform.instance.getPlatformVersion();
  }

  Future<void> init(AppsPrizeConfig config) {
    return AppsprizeFlutterPlatform.instance.init(config);
  }

  Future<bool> launch() {
    return AppsprizeFlutterPlatform.instance.launch();
  }

  Future<void> open(int campaignId) {
    return AppsprizeFlutterPlatform.instance.open(campaignId);
  }

  Future<List<Rewards>> doReward() {
    return AppsprizeFlutterPlatform.instance.doReward();
  }

  Future<bool> hasPermissions() {
    return AppsprizeFlutterPlatform.instance.hasPermissions();
  }

  Future<bool> requestPermission() {
    return AppsprizeFlutterPlatform.instance.requestPermission();
  }
}
