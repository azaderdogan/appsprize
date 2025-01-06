import 'package:appsprize_flutter/appsprize_flutter_method_channel.dart';
import 'package:appsprize_flutter/models/models.dart';
import 'package:flutter/material.dart';
import 'dart:async';

import 'package:flutter/services.dart';
import 'package:appsprize_flutter/appsprize_flutter.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  String _platformVersion = 'Unknown';
  final _appsprizeFlutterPlugin = AppsprizeFlutter();
  List<Rewards> _rewards = [];
  final styleConfig = AppsPrizeStyleConfig(
    primaryColor: "#465073",
    secondaryColor: "#F2FADA",
    highlightColor: "#FF11FF",
    promotionHighlightColor: "#00FFFF",
    cashbackHighlightColor: "#00FFFF",
    secondChanceHighlightColor: "#00FFFF",
    commonTaskHighlightColor: "#00FFFF",
    epicTaskHighlightColor: "#00FFFF",
    legendaryTaskHighlightColor: "#00FFFF",
    bannerDrawable: "custom_banner",
    offersTitleText: "Offer Apps",
    appsTitleText: "Aktif Uygulamalars",
    typeface: "kodemono.ttf",
    // currencyIcon is optional
  );
  @override
  void initState() {
    super.initState();

    _initAppsprize();
  }

  Future<void> _initAppsprize() async {
    await _appsprizeFlutterPlugin.init(AppsPrizeConfig(
        token:
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MX0.6YZeCXIC7StDO4wf1m0wQusrVR8ZwxzXIKFVUDYLKP4",
        userId: "1111-6",
        country: "TR",
        language: "tr",
        gender: "test-gender",
        age: 30,
        uaChannel: "test-uaChannel",
        uaNetwork: "test-uaNetwork",
        adPlacement: "test-adPlacement",
        style: styleConfig));
  }

  Future<void> _launchAppsprize() async {
    var result = await _appsprizeFlutterPlugin.launch();
    print(result);
  }

  Future<void> _openCampaign() async {
    await _appsprizeFlutterPlugin
        .open(122545); // Replace with actual campaign ID
  }

  Future<void> _doReward() async {
    final rewards = await _appsprizeFlutterPlugin.doReward();
    setState(() {
      _rewards = rewards;
    });
  }

  // Platform messages are asynchronous, so we initialize in an async method.
  Future<void> initPlatformState() async {
    String platformVersion;
    // Platform messages may fail, so we use a try/catch PlatformException.
    // We also handle the message potentially returning null.
    try {
      platformVersion = await _appsprizeFlutterPlugin.getPlatformVersion() ??
          'Unknown platform version';
    } on PlatformException {
      platformVersion = 'Failed to get platform version.';
    }

    // If the widget was removed from the tree while the asynchronous platform
    // message was in flight, we want to discard the reply rather than calling
    // setState to update our non-existent appearance.
    if (!mounted) return;

    setState(() {
      _platformVersion = platformVersion;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          title: const Text('Appsprize Example'),
        ),
        body: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text('Running on: $_platformVersion\n'),
              ElevatedButton(
                onPressed: _launchAppsprize,
                child: const Text('Launch Appsprize'),
              ),
              ElevatedButton(
                onPressed: _openCampaign,
                child: const Text('Open Campaign'),
              ),
              ElevatedButton(
                onPressed: _doReward,
                child: const Text('Do Reward'),
              ),
              //listen events

              if (_rewards.isNotEmpty) Text('Rewards: ${_rewards.toString()}'),
              ElevatedButton(
                onPressed: () async {
                  final result =
                      await _appsprizeFlutterPlugin.requestPermission();
                  debugPrint('Permission request result: $result');
                },
                child: const Text('Request Permission'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
