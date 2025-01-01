import 'package:plugin_platform_interface/plugin_platform_interface.dart';

import 'appsprize_flutter_method_channel.dart';

abstract class AppsprizeFlutterPlatform extends PlatformInterface {
  /// Constructs a AppsprizeFlutterPlatform.
  AppsprizeFlutterPlatform() : super(token: _token);

  static final Object _token = Object();

  static AppsprizeFlutterPlatform _instance = MethodChannelAppsprizeFlutter();

  /// The default instance of [AppsprizeFlutterPlatform] to use.
  ///
  /// Defaults to [MethodChannelAppsprizeFlutter].
  static AppsprizeFlutterPlatform get instance => _instance;

  /// Platform-specific implementations should set this with their own
  /// platform-specific class that extends [AppsprizeFlutterPlatform] when
  /// they register themselves.
  static set instance(AppsprizeFlutterPlatform instance) {
    PlatformInterface.verifyToken(instance, _token);
    _instance = instance;
  }

  Future<String?> getPlatformVersion() {
    throw UnimplementedError('platformVersion() has not been implemented.');
  }

  Future<void> init(AppsPrizeConfig config) {
    throw UnimplementedError('init() has not been implemented.');
  }

  Future<bool> launch() {
    throw UnimplementedError('launch() has not been implemented.');
  }

  Future<bool> open(int campaignId) {
    throw UnimplementedError('open() has not been implemented.');
  }

  Future<List<Rewards>> doReward() {
    throw UnimplementedError('doReward() has not been implemented.');
  }

  Future<bool> hasPermissions() {
    throw UnimplementedError('hasPermissions() has not been implemented.');
  }

  Future<bool> requestPermission() {
    throw UnimplementedError('requestPermission() has not been implemented.');
  }
}
