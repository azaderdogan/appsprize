import 'package:flutter_test/flutter_test.dart';
import 'package:appsprize_flutter/appsprize_flutter.dart';
import 'package:appsprize_flutter/appsprize_flutter_platform_interface.dart';
import 'package:appsprize_flutter/appsprize_flutter_method_channel.dart';
import 'package:plugin_platform_interface/plugin_platform_interface.dart';

class MockAppsprizeFlutterPlatform
    with MockPlatformInterfaceMixin
    implements AppsprizeFlutterPlatform {

  @override
  Future<String?> getPlatformVersion() => Future.value('42');
}

void main() {
  final AppsprizeFlutterPlatform initialPlatform = AppsprizeFlutterPlatform.instance;

  test('$MethodChannelAppsprizeFlutter is the default instance', () {
    expect(initialPlatform, isInstanceOf<MethodChannelAppsprizeFlutter>());
  });

  test('getPlatformVersion', () async {
    AppsprizeFlutter appsprizeFlutterPlugin = AppsprizeFlutter();
    MockAppsprizeFlutterPlatform fakePlatform = MockAppsprizeFlutterPlatform();
    AppsprizeFlutterPlatform.instance = fakePlatform;

    expect(await appsprizeFlutterPlugin.getPlatformVersion(), '42');
  });
}
