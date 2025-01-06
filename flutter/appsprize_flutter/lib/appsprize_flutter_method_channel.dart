import 'dart:async';
import 'dart:convert';

import 'package:advertising_id/advertising_id.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/services.dart';

import 'appsprize_flutter_platform_interface.dart';
import 'models/models.dart';

/// An implementation of [AppsprizeFlutterPlatform] that uses method channels.
class MethodChannelAppsprizeFlutter extends AppsprizeFlutterPlatform {
  /// The method channel used to interact with the native platform.
  @visibleForTesting
  final methodChannel = const MethodChannel('appsprize_flutter');

  static const EventChannel _eventChannel =
      EventChannel('appsprize_flutter_events');

  final Map<String, StreamSubscription> _eventSubscriptions = {};

  @override
  Future<String?> getPlatformVersion() async {
    final version =
        await methodChannel.invokeMethod<String>('getPlatformVersion');
    return version;
  }

  @override
  Future<void> init(AppsPrizeConfig config) async {
    try {
      await _cleanupExistingSubscriptions();
      String? advertisingId;

      try {
        advertisingId = await AdvertisingId.id(true);
      } on PlatformException {
        advertisingId = null;
      }
      var map = config.toJson();
      map['advertisingId'] = advertisingId;
      final String rawConfig = jsonEncode(map);

      _setupEventListeners();

      await methodChannel.invokeMethod('init', {'config': rawConfig});
    } catch (e) {
      debugPrint('Error during initialization: $e');
      rethrow;
    }
  }

  Future<void> _cleanupExistingSubscriptions() async {
    for (var subscription in _eventSubscriptions.values) {
      await subscription.cancel();
    }
    _eventSubscriptions.clear();
  }

  void _setupEventListeners() {
    const events = [
      'onInitialize',
      'onInitializeFailed',
      'onRewardUpdate',
      'onNotification'
    ];

    for (final eventName in events) {
      _setupEventListener(eventName);
    }
  }

  void _setupEventListener(String eventName) {
    final stream = _eventChannel.receiveBroadcastStream(eventName);
    _eventSubscriptions[eventName] = stream.listen(
      (dynamic event) => _handleEvent(eventName, event),
      onError: (error) => debugPrint('Error in $eventName stream: $error'),
    );
  }

  void _handleEvent(String eventName, dynamic event) {
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
      debugPrint('Error handling $eventName event: $e');
    }
  }

  @override
  Future<bool> launch() async {
    final result = await methodChannel.invokeMethod<bool>('launch');
    return result ?? false;
  }

  @override
  Future<bool> open(int campaignId) async {
    final result = await methodChannel.invokeMethod<bool>(
      'open',
      {'campaignId': campaignId},
    );
    return result ?? false;
  }

  @override
  Future<List<Rewards>> doReward() async {
    try {
      final rawRewards = await methodChannel.invokeMethod('doReward');
      if (rawRewards != null) {
        return List<Rewards>.from(
          rawRewards['rewards'].map((e) => Rewards.fromJson(e)),
        );
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
