using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Diagnostics;

public class PreFlightScreen : MonoBehaviour
{
    //---button---
    //Category
    Button ButtonCategory1, ButtonCategory2, ButtonCategory3, ButtonCategory4;
    //Setting2
    Button ButtonFlightMode, ButtonViewMode, ButtonScreenDataDisplay, ButtonCSVExport, ButtonCSVAirCraftLoad;
    //Setting4
    Button ButtonRandomWindMode, ButtonRudderFailure, ButtonCenterOfGravityFailure, ButtonRandomCenterOfGravity, ButtonRandomRudder, ButtonRandomGroundEffect, ButtonRandomWindEffect;
    //Control
    Button ButtonSwitchMode, ButtonPositionEstimationMode, ButtonDualScreenView, ButtonControlMethod;
    //Start
    Button ButtonStart;
    //---buttonここまで---

    //---SliderInt---
    //Setting1
    SliderInt SliderIntWindSpeed, SliderIntWindDirection, SliderIntTakeoffSpeed, SliderIntTakeoffYaw, SliderIntTakeoffPitch, SliderIntTakeoffRoll, SliderIntWingMountAngle;
    //Setting2
    SliderInt SliderIntPitchSensitivity, SliderIntSpeakerVolume, SliderIntFOV;
    //Setting3
    SliderInt SliderIntSensorLengthBackward, SliderIntSensorLengthForward;

    //---SliderIntここまで

    //---Dropdown---
    //Setting1
    DropdownField DropdownFieldAircraft, DropdownFieldFlightModel;
    //Control
    DropdownField DropdownFieldCOMPort;
    //---Dropdownここまで---
    //---TextField---
    //Control
    TextField TextFieldWeight;
    //---TextFieldここまで---

    //---Values---
    //Setting1
    Label ValueWindSpeed, ValueWindDirection, ValueTakeoffSpeed, ValueTakeoffYaw, ValueTakeoffPitch, ValueTakeoffRoll, ValueWingMountAngle;
    //Setting2
    Label ValueFlightMode, ValueViewMode, ValueScreenDataDisplay, ValueCSVExport, ValueCSVAirCraftLoad, ValuePitchSensitivity, ValueSpeakerVolume, ValueFOV;
    //Setting3
    Label ValueConnectionStatus, ValueCenterOfGravity, ValuePilotCenterOfGravity, ValueRudder, ValueSensorLengthBackward, ValueSensorLengthForward;
    //Setting4
    Label ValueRandomWindMode, ValueRudderFailure, ValueCenterOfGravityFailure, ValueRandomCenterOfGravity, ValueRandomRudder, ValueRandomGroundEffect, ValueRandomWindEffect;
    //Control
    Label ValueSwitchMode, ValuePositionEstimationMode, ValueDualScreenView, ValueControlMethod;
    //Status
    Label ValueStatus;
    Label ValueVersion;
    //---Valuesここまで---
    //---雑多---
    VisualElement root;
    UIDocument uiDocument;
    //---雑多ここまで---

    //---変数---いったんコメントでカテゴリー分けだけ
    //Setting1
    string Aircraft;
    List<string> AircraftList;

    float WindSpeed, WindDirection, TakeoffSpeed, TakeoffYaw, TakeoffPitch, TakeoffRoll, WingMountAngle;
    //Setting2
    bool ViewMode, ScreenDataDisplay, CSVExport, CSVAirCraftLoad;
    float PitchSensitivity, SpeakerVolume, FOV;
    //Setting3
    string ConnectionStatus;
    float CenterOfGravity, PilotCenterOfGravity, Rudder, SensorLengthBackward, SensorLengthForward;
    //Setting4
    bool RandomWindMode, RudderFailure, CenterOfGravityFailure, RandomCenterOfGravity, RandomRudder, RandomGroundEffect, RandomWindEffect;
    //Control
    bool ControlMethod, SwitchMode;
    string Status, Version;
    float Weight;
    int PositionEstimationMode;
    string ComPort;
    List<string> COMPortList;


    //---変数ここまで---



    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        FindUIElements();
        InitializeUI();
    }

    void Update()
    {
        PushValuesToProject();
    }

    //UIの要素を変数に格納するための関数
    private void FindUIElements()
    {
        ButtonCategory1 = root.Q<Button>("Button_Category1");
        ButtonCategory2 = root.Q<Button>("Button_Category2");
        ButtonCategory3 = root.Q<Button>("Button_Category3");
        ButtonCategory4 = root.Q<Button>("Button_Category4");
        ButtonFlightMode = root.Q<Button>("Button_FlightMode");
        ButtonViewMode = root.Q<Button>("Button_ViewMode");
        ButtonScreenDataDisplay = root.Q<Button>("Button_ScreenDataDisplay");
        ButtonCSVExport = root.Q<Button>("Button_CSVExport");
        ButtonCSVAirCraftLoad = root.Q<Button>("Button_CSVAirCraftLoad");
        ButtonRandomWindMode = root.Q<Button>("Button_RandomWindMode");
        ButtonRudderFailure = root.Q<Button>("Button_RudderFailure");
        ButtonCenterOfGravityFailure = root.Q<Button>("Button_CenterOfGravityFailure");
        ButtonRandomCenterOfGravity = root.Q<Button>("Button_RandomCenterOfGravity");
        ButtonRandomRudder = root.Q<Button>("Button_RandomRudder");
        ButtonRandomGroundEffect = root.Q<Button>("Button_RandomGroundEffect");
        ButtonRandomWindEffect = root.Q<Button>("Button_RandomWindEffect");
        ButtonSwitchMode = root.Q<Button>("Button_SwitchMode");
        ButtonPositionEstimationMode = root.Q<Button>("Button_PositionEstimationMode");
        ButtonDualScreenView = root.Q<Button>("Button_DualScreenView");
        ButtonControlMethod = root.Q<Button>("Button_ControlMethod");
        ButtonStart = root.Q<Button>("Button_Start");

        SliderIntWindSpeed = root.Q<SliderInt>("SliderInt_WindSpeed");
        SliderIntWindDirection = root.Q<SliderInt>("SliderInt_WindDirection");
        SliderIntTakeoffSpeed = root.Q<SliderInt>("SliderInt_TakeoffSpeed");
        SliderIntTakeoffYaw = root.Q<SliderInt>("SliderInt_TakeoffYaw");
        SliderIntTakeoffPitch = root.Q<SliderInt>("SliderInt_TakeoffPitch");
        SliderIntTakeoffRoll = root.Q<SliderInt>("SliderInt_TakeoffRoll");
        SliderIntWingMountAngle = root.Q<SliderInt>("SliderInt_WingMountAngle");
        SliderIntPitchSensitivity = root.Q<SliderInt>("SliderInt_PitchSensitivity");
        SliderIntSpeakerVolume = root.Q<SliderInt>("SliderInt_SpeakerVolume");
        SliderIntFOV = root.Q<SliderInt>("SliderInt_FOV");
        SliderIntSensorLengthBackward = root.Q<SliderInt>("SliderInt_SensorLengthBackward");
        SliderIntSensorLengthForward = root.Q<SliderInt>("SliderInt_SensorLengthForward");

        DropdownFieldAircraft = root.Q<DropdownField>("DropdownField_Aircraft");
        DropdownFieldFlightModel = root.Q<DropdownField>("DropdownField_FlightModel");
        DropdownFieldCOMPort = root.Q<DropdownField>("DropdownField_COMPort");

        TextFieldWeight = root.Q<TextField>("TextField_Weight");

        ValueWindSpeed = root.Q<Label>("Value_WindSpeed");
        ValueWindDirection = root.Q<Label>("Value_WindDirection");
        ValueTakeoffSpeed = root.Q<Label>("Value_TakeoffSpeed");
        ValueTakeoffYaw = root.Q<Label>("Value_TakeoffYaw");
        ValueTakeoffPitch = root.Q<Label>("Value_TakeoffPitch");
        ValueTakeoffRoll = root.Q<Label>("Value_TakeoffRoll");
        ValueWingMountAngle = root.Q<Label>("Value_WingMountAngle");
        ValueFlightMode = root.Q<Label>("Value_FlightMode");
        ValueViewMode = root.Q<Label>("Value_ViewMode");
        ValueScreenDataDisplay = root.Q<Label>("Value_ScreenDataDisplay");
        ValueCSVExport = root.Q<Label>("Value_CSVExport");
        ValueCSVAirCraftLoad = root.Q<Label>("Value_CSVAirCraftLoad");
        ValuePitchSensitivity = root.Q<Label>("Value_PitchSensitivity");
        ValueSpeakerVolume = root.Q<Label>("Value_SpeakerVolume");
        ValueFOV = root.Q<Label>("Value_FOV");
        ValueConnectionStatus = root.Q<Label>("Value_ConnectionStatus");
        ValueCenterOfGravity = root.Q<Label>("Value_CenterOfGravity");
        ValuePilotCenterOfGravity = root.Q<Label>("Value_PilotCenterOfGravity");
        ValueRudder = root.Q<Label>("Value_Rudder");
        ValueRandomWindMode = root.Q<Label>("Value_RandomWindMode");
        ValueRudderFailure = root.Q<Label>("Value_RudderFailure");
        ValueCenterOfGravityFailure = root.Q<Label>("Value_CenterOfGravityFailure");
        ValueRandomCenterOfGravity = root.Q<Label>("Value_RandomCenterOfGravity");
        ValueRandomRudder = root.Q<Label>("Value_RandomRudder");
        ValueRandomGroundEffect = root.Q<Label>("Value_RandomGroundEffect");
        ValueRandomWindEffect = root.Q<Label>("Value_RandomWindEffect");
        ValueSwitchMode = root.Q<Label>("Value_SwitchMode");
        ValuePositionEstimationMode = root.Q<Label>("Value_PositionEstimationMode");
        ValueDualScreenView = root.Q<Label>("Value_DualScreenView");
        ValueControlMethod = root.Q<Label>("Value_ControlMethod");
        ValueStatus = root.Q<Label>("Value_Status");
        ValueVersion = root.Q<Label>("Value_Version");
        ValueSensorLengthBackward = root.Q<Label>("Value_SensorLengthBackward");
        ValueSensorLengthForward = root.Q<Label>("Value_SensorLengthForward");

    }

    private void InitializeUI()
    {
        InitializeValues();

        InitializeCategoryButtons();
        InitializeStartButton();
        InitializeSliderInts();
        InitializeDropdowns();

        PullValuesFromProject();
    }

    private void InitializeCategoryButtons()
    {
        //クラスhiddenがついているとき非表示になるので, それをCategory1 <-> Setting1でVisualElementが対応しているので, hiddenをつけたり外したりする
        ButtonCategory1.clicked += () =>
        {
            root.Q<VisualElement>("Setting1").RemoveFromClassList("hidden");
            root.Q<VisualElement>("Setting2").AddToClassList("hidden");
            root.Q<VisualElement>("Setting3").AddToClassList("hidden");
            root.Q<VisualElement>("Setting4").AddToClassList("hidden");
        };
        ButtonCategory2.clicked += () =>
        {
            root.Q<VisualElement>("Setting1").AddToClassList("hidden");
            root.Q<VisualElement>("Setting2").RemoveFromClassList("hidden");
            root.Q<VisualElement>("Setting3").AddToClassList("hidden");
            root.Q<VisualElement>("Setting4").AddToClassList("hidden");
        };
        ButtonCategory3.clicked += () =>
        {
            root.Q<VisualElement>("Setting1").AddToClassList("hidden");
            root.Q<VisualElement>("Setting2").AddToClassList("hidden");
            root.Q<VisualElement>("Setting3").RemoveFromClassList("hidden");
            root.Q<VisualElement>("Setting4").AddToClassList("hidden");
        };
        ButtonCategory4.clicked += () =>
        {
            root.Q<VisualElement>("Setting1").AddToClassList("hidden");
            root.Q<VisualElement>("Setting2").AddToClassList("hidden");
            root.Q<VisualElement>("Setting3").AddToClassList("hidden");
            root.Q<VisualElement>("Setting4").RemoveFromClassList("hidden");
        };
    }

    private void InitializeStartButton()
    {
        ButtonStart.clicked += StartFlight;
    }

    private void StartFlight()
    {
        if (GameManager.instance.EnterFlight)
        {
            return;
        }

        PushValuesToProject();
        ApplyTakeoffRotation();

        GameManager.instance.EnterFlight = true;
        GameManager.instance.FlightSettingActive = false;
        GameManager.instance.status = GameManager.Status.InFlight;
        Time.timeScale = GameManager.instance.Landing ? 0f : 1f;

        SaveCsvScript saveCsvScript = GetComponent<SaveCsvScript>();
        if (saveCsvScript == null)
        {
            saveCsvScript = FindObjectOfType<SaveCsvScript>();
        }

        if (saveCsvScript != null)
        {
            saveCsvScript.SetFile();
        }
        GameManager.instance.status = GameManager.Status.InFlight;
    }

    private void InitializeSliderInts()
    {
        // Setting1
        if (SliderIntWindSpeed != null)
        {
            SliderIntWindSpeed.lowValue = 0;
            SliderIntWindSpeed.highValue = 60;
            SliderIntWindSpeed.pageSize = 0;
            SliderIntWindSpeed.value = (int)(WindSpeed * 10);
            SliderIntWindSpeed.RegisterValueChangedCallback(evt =>
            {
                WindSpeed = evt.newValue / 10f;
                if (ValueWindSpeed != null) ValueWindSpeed.text = $"{WindSpeed} [m/s]";
            });
        }
        if (SliderIntWindDirection != null)
        {
            SliderIntWindDirection.lowValue = -1800;
            SliderIntWindDirection.highValue = 1800;
            SliderIntWindDirection.pageSize = 0;
            SliderIntWindDirection.value = (int)(WindDirection * 10);
            SliderIntWindDirection.RegisterValueChangedCallback(evt =>
            {
                WindDirection = evt.newValue / 10f;
                if (ValueWindDirection != null) ValueWindDirection.text = $"{WindDirection} [deg]";
            });
        }
        if (SliderIntTakeoffSpeed != null)
        {
            SliderIntTakeoffSpeed.lowValue = 0;
            SliderIntTakeoffSpeed.highValue = 70;
            SliderIntTakeoffSpeed.pageSize = 0;
            SliderIntTakeoffSpeed.value = (int)(TakeoffSpeed * 10);
            SliderIntTakeoffSpeed.RegisterValueChangedCallback(evt =>
            {
                TakeoffSpeed = evt.newValue / 10f;
                if (ValueTakeoffSpeed != null) ValueTakeoffSpeed.text = $"{TakeoffSpeed} [m/s]";
            });
        }
        if (SliderIntTakeoffYaw != null)
        {
            SliderIntTakeoffYaw.lowValue = -200;
            SliderIntTakeoffYaw.highValue = 200;
            SliderIntTakeoffYaw.pageSize = 0;
            SliderIntTakeoffYaw.value = (int)(TakeoffYaw * 10);
            SliderIntTakeoffYaw.RegisterValueChangedCallback(evt =>
            {
                TakeoffYaw = evt.newValue / 10f;
                if (ValueTakeoffYaw != null) ValueTakeoffYaw.text = $"{TakeoffYaw} [deg]";
                ApplyTakeoffRotation();
            });
        }
        if (SliderIntTakeoffPitch != null)
        {
            SliderIntTakeoffPitch.lowValue = -100;
            SliderIntTakeoffPitch.highValue = 100;
            SliderIntTakeoffPitch.pageSize = 0;
            SliderIntTakeoffPitch.value = (int)(TakeoffPitch * 10);
            SliderIntTakeoffPitch.RegisterValueChangedCallback(evt =>
            {
                TakeoffPitch = evt.newValue / 10f;
                if (ValueTakeoffPitch != null) ValueTakeoffPitch.text = $"{TakeoffPitch} [deg]";
                ApplyTakeoffRotation();
            });
        }
        if (SliderIntTakeoffRoll != null)
        {
            SliderIntTakeoffRoll.lowValue = -100;
            SliderIntTakeoffRoll.highValue = 100;
            SliderIntTakeoffRoll.pageSize = 0;
            SliderIntTakeoffRoll.value = (int)(TakeoffRoll * 10);
            SliderIntTakeoffRoll.RegisterValueChangedCallback(evt =>
            {
                TakeoffRoll = evt.newValue / 10f;
                if (ValueTakeoffRoll != null) ValueTakeoffRoll.text = $"{TakeoffRoll} [deg]";
                ApplyTakeoffRotation();
            });
        }
        if (SliderIntWingMountAngle != null)
        {
            SliderIntWingMountAngle.lowValue = -30;
            SliderIntWingMountAngle.highValue = 30;
            SliderIntWingMountAngle.pageSize = 0;
            SliderIntWingMountAngle.value = (int)(WingMountAngle * 10);
            SliderIntWingMountAngle.RegisterValueChangedCallback(evt =>
            {
                WingMountAngle = evt.newValue / 10f;
                if (ValueWingMountAngle != null) ValueWingMountAngle.text = $"{WingMountAngle} [deg]";
            });
        }

        // Setting2
        if (SliderIntPitchSensitivity != null)
        {
            SliderIntPitchSensitivity.lowValue = 5;
            SliderIntPitchSensitivity.highValue = 20;
            SliderIntPitchSensitivity.pageSize = 0;
            SliderIntPitchSensitivity.value = (int)(PitchSensitivity * 10);
            SliderIntPitchSensitivity.RegisterValueChangedCallback(evt =>
            {
                PitchSensitivity = evt.newValue / 10f;
                if (ValuePitchSensitivity != null) ValuePitchSensitivity.text = $"{PitchSensitivity}";
            });
        }
        if (SliderIntSpeakerVolume != null)
        {
            SliderIntSpeakerVolume.lowValue = 0;
            SliderIntSpeakerVolume.highValue = 100;
            SliderIntSpeakerVolume.pageSize = 0;
            SliderIntSpeakerVolume.value = (int)SpeakerVolume;
            SliderIntSpeakerVolume.RegisterValueChangedCallback(evt =>
            {
                SpeakerVolume = evt.newValue;
                if (ValueSpeakerVolume != null) ValueSpeakerVolume.text = $"{SpeakerVolume}";
            });
        }
        if (SliderIntFOV != null)
        {
            SliderIntFOV.lowValue = 45;
            SliderIntFOV.highValue = 180;
            SliderIntFOV.pageSize = 0;
            SliderIntFOV.value = (int)FOV;
            SliderIntFOV.RegisterValueChangedCallback(evt =>
            {
                FOV = evt.newValue;
                if (ValueFOV != null) ValueFOV.text = $"{FOV} [deg]";
                ApplyFieldOfView();
            });
        }
        //0.00mから1.00mまで
        if (SliderIntSensorLengthBackward != null)
        {
            SliderIntSensorLengthBackward.lowValue = 0;
            SliderIntSensorLengthBackward.highValue = 100;
            SliderIntSensorLengthBackward.pageSize = 0;
            SliderIntSensorLengthBackward.value = (int)(SensorLengthBackward * 100);
            SliderIntSensorLengthBackward.RegisterValueChangedCallback(evt =>
            {
                SensorLengthBackward = evt.newValue / 100f;
                if (ValueSensorLengthBackward != null) ValueSensorLengthBackward.text = $"{SensorLengthBackward} [m]";
            });
        }
        if (SliderIntSensorLengthForward != null)
        {
            SliderIntSensorLengthForward.lowValue = 0;
            SliderIntSensorLengthForward.highValue = 100;
            SliderIntSensorLengthForward.pageSize = 0;
            SliderIntSensorLengthForward.value = (int)(SensorLengthForward * 100);
            SliderIntSensorLengthForward.RegisterValueChangedCallback(evt =>
            {
                SensorLengthForward = -evt.newValue / 100f;
                if (ValueSensorLengthForward != null) ValueSensorLengthForward.text = $"{SensorLengthForward} [m]";
            });
        }
    }

    private void InitializeDropdowns()
    {

        AircraftList = new List<string>
    {
        "Tatsumi",
        "Ray",
        "Mio",
        "QX-18",
        "QX-19",
        "QX-20",
        "ARG-2",
        "ORCA18",
        "UL01B",
        "ORCA18",
        "ORCA22",
        "Gardenia",
        "Aria",
        "Camellia"
    };

        DropdownFieldAircraft.choices = AircraftList;
        DropdownFieldAircraft.value = Aircraft;

        DropdownFieldAircraft.RegisterValueChangedCallback(evt =>
        {
            Aircraft = evt.newValue;
        });

        //comport
        COMPortList = new List<string>
        {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
        };
        DropdownFieldCOMPort.choices = COMPortList;
        DropdownFieldCOMPort.value = ComPort;

    }

    private void PullValuesFromProject()
    {
        //プロジェクトから値を引っ張ってきてUIに反映させる関数. これもGamemanagerとかいろんなところから値を引っ張ってきてUIに反映させる.
    }
    private void PushValuesToProject()
    {
        //値を更新する関数. これもGamemanagerとかいろんなところに値を反映させる.
        GameManager.instance.GustMag = WindSpeed;
        GameManager.instance.GustDirection = WindDirection;
        GameManager.instance.Airspeed_TO = TakeoffSpeed;
        GameManager.instance.StartRotation = TakeoffYaw;
        GameManager.instance.TailRotation = TakeoffPitch;
        GameManager.instance.StartRoll = TakeoffRoll;
        GameManager.instance.TailSetDeg = WingMountAngle;
        GameManager.instance.isMainDisplayTPS = ViewMode;
        GameManager.instance.HUDActive = ScreenDataDisplay;
        GameManager.instance.SaveCsv = CSVExport;
        GameManager.instance.customPlaneDataEnabled = CSVAirCraftLoad;
        GameManager.instance.MouseSensitivity = PitchSensitivity;
        GameManager.instance.SoundVolume = SpeakerVolume;
        GameManager.instance.FieldOfView = FOV;
        GameManager.instance.RandomWind = RandomWindMode;
        GameManager.instance.RudderError = RudderFailure;
        GameManager.instance.CenterOfMassError = CenterOfGravityFailure;
        GameManager.instance.CenterOfMassRand = RandomCenterOfGravity;
        GameManager.instance.RudderRand = RandomRudder;
        GameManager.instance.CgeRand = RandomGroundEffect;
        GameManager.instance.GustRand = RandomWindEffect;
        GameManager.instance.VRMode = SwitchMode;
        GameManager.instance.MousePitchControl = ControlMethod;
        GameManager.instance.PlaneName = Aircraft;
        GameManager.instance.lengthBackward = SensorLengthBackward;
        GameManager.instance.lengthForward = SensorLengthForward;
    }

    private void InitializeValues()
    {
        //変数代入
        ValueVersion.text = Application.productName + " " + Application.version;//こいつだけこのcs内

        WindSpeed = GameManager.instance.GustMag;
        WindDirection = GameManager.instance.GustDirection;
        TakeoffSpeed = GameManager.instance.Airspeed_TO;
        TakeoffYaw = GameManager.instance.StartRotation;
        TakeoffPitch = GameManager.instance.TailRotation;
        TakeoffRoll = GameManager.instance.StartRoll;
        WingMountAngle = GameManager.instance.TailSetDeg;
        ViewMode = GameManager.instance.isMainDisplayTPS;
        ScreenDataDisplay = GameManager.instance.HUDActive;
        CSVExport = GameManager.instance.SaveCsv;
        PitchSensitivity = GameManager.instance.MouseSensitivity;
        SpeakerVolume = GameManager.instance.SoundVolume;
        FOV = GameManager.instance.FieldOfView;
        CenterOfGravity = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>().centerOfMass;
        PilotCenterOfGravity = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>().centerOfMassPilot;
        Rudder = GameManager.instance.Plane.GetComponent<AerodynamicCalculator>().dr;
        RandomWindMode = GameManager.instance.RandomWind;
        RudderFailure = GameManager.instance.RudderError;
        CenterOfGravityFailure = GameManager.instance.CenterOfMassError;
        RandomCenterOfGravity = GameManager.instance.CenterOfMassRand;
        RandomRudder = GameManager.instance.RudderRand;
        RandomGroundEffect = GameManager.instance.CgeRand;
        RandomWindEffect = GameManager.instance.GustRand;
        SwitchMode = GameManager.instance.VRMode;
        ControlMethod = GameManager.instance.MousePitchControl;
        Weight = AerodynamicCalculator.massPilot;
        Status = GameManager.instance.StatusText;

        SensorLengthBackward = GameManager.instance.lengthBackward;
        SensorLengthForward = GameManager.instance.lengthForward;

        CSVAirCraftLoad = GameManager.instance.customPlaneDataEnabled;

        //実際のlabel反映 PreflightScreen.uxmlの初期値はDisplay.WindSpeed [m/s]のように書いてあるので、それをWindSpeed [m/s]のように書き換える
        ValueWindSpeed.text = $"{WindSpeed} [m/s]";
        ValueWindDirection.text = $"{WindDirection} [deg]";
        ValueTakeoffSpeed.text = $"{TakeoffSpeed} [m/s]";
        ValueTakeoffYaw.text = $"{TakeoffYaw} [deg]";
        ValueTakeoffPitch.text = $"{TakeoffPitch} [deg]";
        ValueTakeoffRoll.text = $"{TakeoffRoll} [deg]";
        ValueWingMountAngle.text = $"{WingMountAngle} [deg]";
        ValueFlightMode.text = ViewMode ? "TPS" : "FPS";
        ValueViewMode.text = ViewMode ? "Main Display" : "Sub Display";
        ValueScreenDataDisplay.text = ScreenDataDisplay ? "ON" : "OFF";
        ValueCSVExport.text = CSVExport ? "ON" : "OFF";
        ValueCSVAirCraftLoad.text = CSVAirCraftLoad ? "ON" : "OFF";
        ValuePitchSensitivity.text = $"{PitchSensitivity}";
        ValueSpeakerVolume.text = $"{SpeakerVolume}";
        ValueFOV.text = $"{FOV} [deg]";
        ValueCenterOfGravity.text = $"{CenterOfGravity} [m]";
        ValuePilotCenterOfGravity.text = $"{PilotCenterOfGravity} [m]";
        ValueRudder.text = $"{Rudder} [deg]";
        ValueRandomWindMode.text = RandomWindMode ? "ON" : "OFF";
        ValueRudderFailure.text = RudderFailure ? "ON" : "OFF";
        ValueCenterOfGravityFailure.text = CenterOfGravityFailure ? "ON" : "OFF";
        ValueRandomCenterOfGravity.text = RandomCenterOfGravity ? "ON" : "OFF";
        ValueRandomRudder.text = RandomRudder ? "ON" : "OFF";
        ValueRandomGroundEffect.text = RandomGroundEffect ? "ON" : "OFF";
        ValueRandomWindEffect.text = RandomWindEffect ? "ON" : "OFF";
        ValueSwitchMode.text = SwitchMode ? "VR" : "Non-VR";
        ValueControlMethod.text = ControlMethod ? "Mouse Pitch Control" : "Joystick Control";
        ValueStatus.text = Status;
        ValueSensorLengthBackward.text = $"{SensorLengthBackward} [m]";
        ValueSensorLengthForward.text = $"{SensorLengthForward} [m]";


    }

    private void ApplyTakeoffRotation()
    {
        if (GameManager.instance.status != GameManager.Status.PreFlight)
        {
            return;
        }

        GameManager.instance.StartRotation = TakeoffYaw;
        GameManager.instance.TailRotation = TakeoffPitch;
        GameManager.instance.StartRoll = TakeoffRoll;

        if (GameManager.instance.Plane == null)
        {
            return;
        }

        GameManager.instance.Plane.transform.rotation = Quaternion.Euler(
            GameManager.instance.StartRoll,
            GameManager.instance.StartRotation,
            GameManager.instance.TailRotation
        );
    }


    private void ApplyFieldOfView()
    {
        GameManager.instance.FieldOfView = FOV;

        if (GameManager.instance.Plane == null)
        {
            return;
        }

        Camera fpsCamera = GameManager.instance.Plane
            .transform.Find("FPSCamera")
            ?.GetComponent<Camera>();

        Camera tpsCamera = GameManager.instance.Plane
            .transform.Find("TPSCamera")
            ?.GetComponent<Camera>();

        if (fpsCamera != null)
        {
            fpsCamera.fieldOfView = FOV;
        }

        if (tpsCamera != null)
        {
            tpsCamera.fieldOfView = FOV;
        }
    }
}
