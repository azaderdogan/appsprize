import 'dart:async';
import 'dart:convert';

import 'package:flutter/foundation.dart';
import 'package:flutter/services.dart';

import 'appsprize_flutter_platform_interface.dart';

/// An implementation of [AppsprizeFlutterPlatform] that uses method channels.
class MethodChannelAppsprizeFlutter extends AppsprizeFlutterPlatform {
  /// The method channel used to interact with the native platform.
  @visibleForTesting
  final methodChannel = const MethodChannel('appsprize_flutter');

  @override
  Future<String?> getPlatformVersion() async {
    final version =
        await methodChannel.invokeMethod<String>('getPlatformVersion');
    return version;
  }

  static const EventChannel _eventChannel =
      EventChannel('appsprize_flutter_events');
  Map<String, StreamSubscription> _eventSubscriptions = {};

  @override
  @override
  Future<void> init(AppsPrizeConfig config) async {
    try {
      _eventSubscriptions.values
          .forEach((subscription) => subscription.cancel());
      _eventSubscriptions.clear();

      final String rawConfig = jsonEncode(config.toJson());

      _setupEventListener('onInitialize');
      _setupEventListener('onInitializeFailed');
      _setupEventListener('onRewardUpdate');
      _setupEventListener('onNotification');

      await methodChannel.invokeMethod('init', {'config': rawConfig});
    } catch (e) {
      debugPrint('Error during initialization: $e');
    }
  }

  void _setupEventListener(String eventName) {
    final stream = _eventChannel.receiveBroadcastStream(eventName);
    _eventSubscriptions[eventName] = stream.listen((dynamic event) {
      try {
        final Map<String, dynamic> decodedEvent = jsonDecode(event['data']);
        switch (eventName) {
          case 'onInitialize':
            // Handle initialize event
            break;
          case 'onInitializeFailed':
            // Handle initialize failed event
            break;
          case 'onRewardUpdate':
            // Handle reward update event
            break;
          case 'onNotification':
            // Handle notification event
            break;
        }
      } catch (e) {
        debugPrint('Error in event listener for $eventName: $e');
      }
    });
  }

  @override
  Future<bool> launch() async {
    final result = await methodChannel.invokeMethod<bool>('launch');
    return result ?? false;
  }

  @override
  Future<bool> open(int campaignId) async {
    final result = await methodChannel
        .invokeMethod<bool>('open', {'campaignId': campaignId});
    return result ?? false;
  }

  @override
  Future<List<Map<String, dynamic>>> doReward() async {
    try {
      final String? rawRewards = await methodChannel.invokeMethod('doReward');
      if (rawRewards != null) {
        final decoded = jsonDecode(rawRewards);
        final rewards = List<Map<String, dynamic>>.from(decoded['rewards']);
        return rewards;
      }
    } catch (e) {
      debugPrint('Error in doReward: $e');
    }
    return [];
  }

  @override
  Future<bool> hasPermissions() async {
    final result = await methodChannel.invokeMethod<bool>('hasPermissions');
    return result ?? false;
  }

  @override
  Future<bool> requestPermission() async {
    final result = await methodChannel.invokeMethod<bool>('requestPermission');
    return result ?? false;
  }
}

class AppsPrizeConfig {
  final String token;
  final String userId;
  final String advertisingId;
  final String country;
  final String language;
  final String gender;
  final int age;
  final String uaChannel;
  final String uaNetwork;
  final String adPlacement;
  final Map<String, dynamic>? style;

  AppsPrizeConfig({
    required this.token,
    required this.userId,
    required this.advertisingId,
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
      'advertisingId': advertisingId,
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
