using System;
using UnityEngine;

public static class AircraftData
{
    // 機体の重量と慣性モーメント - 6

    private static float mass; // 重量[kg]
    private static Vector3 centerOfMass = Vector3.zero; // 重心位置[m]
    private static Vector3 inertiaTensor = Vector3.zero; // 慣性モーメント[kg*m^2]
    private static Quaternion inertiaTensorRotation = Quaternion.identity; // 慣性モーメントの回転
    private static float massAircraft; // 機体のみ全重量[kg]
    private static float centerOfMassAircraft; // 機体のみ全重心(パイロットなし,ピッチのみ)[m]

    // 巡航時 - 5

    private static float Airspeed0; // Magnitude of ground speed [m/s]
    private static float alpha0; // Angle of attack [deg]
    private static float CDp0; // Parasitic drag [-]
    private static float Cmw0; // Pitching momentum [-]
    private static float CLMAX; // Maximum lift coefficient [-]

    // 主翼 - 7

    private static float Sw; // Wing area of wing [m^2]
    private static float bw; // Wing span [m]
    private static float cMAC; // Mean aerodynamic chord [m]
    private static float aw; // Wing Lift Slope [1/deg]
    private static float hw; // Length between Wing a.c. and c.g.
    private static float ew; // Wing efficiency
    private static float AR; // Aspect Ratio

    // 水平尾翼 - 7

    private static bool Downwash; // Conventional Tail: True, T-Tail: False
    private static float St; // Wing area of tail
    private static float at; // Tail Lift Slope [1/deg]
    private static float lt; // Length between Tail a.c. and c.g.
    private static float deMAX; // Maximum elevator angle
    private static float tau; // Control surface angle of attack effectiveness [-]
    private static float VH; // Tail Volume

    // 垂直尾翼 - 1

    private static float drMAX; // Maximum rudder angle

    // 地面効果 - 1

    private static float CGEMIN; // Minimum Ground Effect Coefficient [-]

    // 安定微係数 - 12

    private static float Cyb; // [1/deg]
    private static float Cyp; // [1/rad]
    private static float Cyr; // [1/rad]
    private static float Cydr; // [1/deg]
    private static float Clb; // [1/deg]
    private static float Clp; // [1/rad]
    private static float Clr; // [1/rad]
    private static float Cldr; // [1/deg]
    private static float Cnb; // [1/deg]
    private static float Cnp; // [1/rad]
    private static float Cnr; // [1/rad]
    private static float Cndr; // [1/deg]

    // 離陸 - 1

    private static float YL; // 機体中心から翼持ち棒までの長さ[m]

    // 合計 - 40

    // フラグ

    private static readonly bool[] isSetted = new bool[40]; // 各パラメータがCSVから読み込まれたかどうかを保持するフラグ.

    // ===== 指定されたCSVファイルから機体データを読み込む ===========================================================
    private static bool Load(string csvPath)
    {
        int recordCount = 50;
        int fieldCount = 20;

        using CsvIO csv = new(recordCount, fieldCount);
        csv.Load(csvPath);

        for (int i = 0; i < 40; i++)
        {
            isSetted[i] = false; // フラグを初期化.
        }

        try
        {
            for (int i = 1; i < recordCount; i++) // `csv.Read()`は(1, 1)から開始する.
            {
                for (int j = 1; j < fieldCount; j++)
                {
                    string rawValue = csv.Read(i, j);
                    if (string.IsNullOrEmpty(rawValue)) continue; // 空白セルはスキップ.

                    string value = rawValue.ToLower(); // 小文字に変換.
                    //Debug.Log($"csv.Read({i}, {j}) = {value}");

                    switch (value)
                    {
                        // 機体の重量と慣性モーメント - 6
                        case "mass":
                            mass = float.Parse(csv.Read(i, j + 1)); // 重量[kg]
                            isSetted[0] = true; // フラグを立てる.
                            Debug.Log("`mass` setted.");
                            break;

                        case "centerofmass":
                            centerOfMass = new Vector3(float.Parse(csv.Read(i, j + 1)), float.Parse(csv.Read(i, j + 2)), float.Parse(csv.Read(i, j + 3))); // 重心位置[m]
                            isSetted[1] = true;
                            Debug.Log("`centerOfMass` setted.");
                            break;

                        case "inertiatensor":
                            inertiaTensor = new Vector3(float.Parse(csv.Read(i, j + 1)), float.Parse(csv.Read(i, j + 2)), float.Parse(csv.Read(i, j + 3))); // 慣性モーメント[kg*m^2]
                            isSetted[2] = true;
                            Debug.Log("`inertiaTensor` setted.");
                            break;

                        case "inertiatensorrotation":
                            inertiaTensorRotation = Quaternion.AngleAxis(float.Parse(csv.Read(i, j + 1)), Vector3.forward); // 慣性モーメントの回転
                            isSetted[3] = true;
                            Debug.Log("`inertiaTensorRotation` setted.");
                            break;

                        case "massaircraft":
                            massAircraft = float.Parse(csv.Read(i, j + 1)); // 機体のみ全重量[kg]
                            isSetted[4] = true;
                            Debug.Log("`massAircraft` setted.");
                            break;

                        case "centerofmassaircraft":
                            centerOfMassAircraft = float.Parse(csv.Read(i, j + 1)); // 機体のみ全重心(パイロットなし,ピッチのみ)[m]
                            isSetted[5] = true;
                            Debug.Log("`centerOfMassAircraft` setted.");
                            break;

                        // 巡航時 - 5
                        case "airspeed0":
                            Airspeed0 = float.Parse(csv.Read(i, j + 1)); // Magnitude of ground speed [m/s]
                            isSetted[6] = true;
                            Debug.Log("`Airspeed0` setted.");
                            break;

                        case "alpha0":
                            alpha0 = float.Parse(csv.Read(i, j + 1)); // Angle of attack [deg]
                            isSetted[7] = true;
                            Debug.Log("`alpha0` setted.");
                            break;

                        case "cdp0":
                            CDp0 = float.Parse(csv.Read(i, j + 1)); // Parasitic drag [-]
                            isSetted[8] = true;
                            Debug.Log("`CDp0` setted.");
                            break;

                        case "cmw0":
                            Cmw0 = float.Parse(csv.Read(i, j + 1)); // Pitching momentum [-]
                            isSetted[9] = true;
                            Debug.Log("`Cmw0` setted.");
                            break;

                        case "clmax":
                            CLMAX = float.Parse(csv.Read(i, j + 1)); // Maximum lift coefficient [-]
                            isSetted[10] = true;
                            Debug.Log("`CLMAX` setted.");
                            break;

                        // 主翼 - 7
                        case "sw":
                            Sw = float.Parse(csv.Read(i, j + 1)); // Wing area of wing [m^2]
                            isSetted[11] = true;
                            Debug.Log("`Sw` setted.");
                            break;

                        case "bw":
                            bw = float.Parse(csv.Read(i, j + 1)); // Wing span [m]
                            isSetted[12] = true;
                            Debug.Log("`bw` setted.");
                            break;

                        case "cmac":
                            cMAC = float.Parse(csv.Read(i, j + 1)); // Mean aerodynamic chord [m]
                            isSetted[13] = true;
                            Debug.Log("`cMAC` setted.");
                            break;

                        case "aw":
                            aw = float.Parse(csv.Read(i, j + 1)); // Wing Lift Slope [1/deg]
                            isSetted[14] = true;
                            Debug.Log("`aw` setted.");
                            break;

                        case "hw":
                            hw = float.Parse(csv.Read(i, j + 1)); // Length between Wing a.c. and c.g.
                            isSetted[15] = true;
                            Debug.Log("`hw` setted.");
                            break;

                        case "ew":
                            ew = float.Parse(csv.Read(i, j + 1)); // Wing efficiency
                            isSetted[16] = true;
                            Debug.Log("`ew` setted.");
                            break;

                        case "ar":
                            AR = float.Parse(csv.Read(i, j + 1)); // Aspect Ratio
                            isSetted[17] = true;
                            Debug.Log("`AR` setted.");
                            break;

                        // 水平尾翼 - 7
                        case "downwash":
                            Downwash = Convert.ToBoolean(csv.Read(i, j + 1)); // Conventional Tail: True, T-Tail: False
                            isSetted[18] = true;
                            Debug.Log("`Downwash` setted.");
                            break;

                        case "st":
                            St = float.Parse(csv.Read(i, j + 1)); // Wing area of tail
                            isSetted[19] = true;
                            Debug.Log("`St` setted.");
                            break;

                        case "at":
                            at = float.Parse(csv.Read(i, j + 1)); // Tail Lift Slope [1/deg]
                            isSetted[20] = true;
                            Debug.Log("`at` setted.");
                            break;

                        case "lt":
                            lt = float.Parse(csv.Read(i, j + 1)); // Length between Tail a.c. and c.g.
                            isSetted[21] = true;
                            Debug.Log("`lt` setted.");
                            break;

                        case "demax":
                            deMAX = float.Parse(csv.Read(i, j + 1)); // Maximum elevator angle
                            isSetted[22] = true;
                            Debug.Log("`deMAX` setted.");
                            break;

                        case "tau":
                            tau = float.Parse(csv.Read(i, j + 1)); // Control surface angle of attack effectiveness [-]
                            isSetted[23] = true;
                            Debug.Log("`tau` setted.");
                            break;

                        case "vh":
                            VH = float.Parse(csv.Read(i, j + 1)); // Tail Volume
                            isSetted[24] = true;
                            Debug.Log("`VH` setted.");
                            break;

                        // 垂直尾翼 - 1
                        case "drmax":
                            drMAX = float.Parse(csv.Read(i, j + 1)); // Maximum rudder angle
                            isSetted[25] = true;
                            Debug.Log("`drMAX` setted.");
                            break;

                        // 地面効果 - 1
                        case "cgemin":
                            CGEMIN = float.Parse(csv.Read(i, j + 1)); // Minimum Ground Effect Coefficient [-]
                            isSetted[26] = true;
                            Debug.Log("`CGEMIN` setted.");
                            break;

                        // 安定微係数 - 12
                        case "cyb":
                            Cyb = float.Parse(csv.Read(i, j + 1)); // [1/deg]
                            isSetted[27] = true;
                            Debug.Log("`Cyb` setted.");
                            break;

                        case "cyp":
                            Cyp = float.Parse(csv.Read(i, j + 1)); // [1/rad]
                            isSetted[28] = true;
                            Debug.Log("`Cyp` setted.");
                            break;

                        case "cyr":
                            Cyr = float.Parse(csv.Read(i, j + 1)); // [1/rad]
                            isSetted[29] = true;
                            Debug.Log("`Cyr` setted.");
                            break;

                        case "cydr":
                            Cydr = float.Parse(csv.Read(i, j + 1)); // [1/deg]
                            isSetted[30] = true;
                            Debug.Log("`Cydr` setted.");
                            break;

                        case "clb":
                            Clb = float.Parse(csv.Read(i, j + 1)); // [1/deg]
                            isSetted[31] = true;
                            Debug.Log("`Clb` setted.");
                            break;

                        case "clp":
                            Clp = float.Parse(csv.Read(i, j + 1)); // [1/rad]
                            isSetted[32] = true;
                            Debug.Log("`Clp` setted.");
                            break;

                        case "clr":
                            Clr = float.Parse(csv.Read(i, j + 1)); // [1/rad]
                            isSetted[33] = true;
                            Debug.Log("`Clr` setted.");
                            break;

                        case "cldr":
                            Cldr = float.Parse(csv.Read(i, j + 1)); // [1/deg]
                            isSetted[34] = true;
                            Debug.Log("`Cldr` setted.");
                            break;

                        case "cnb":
                            Cnb = float.Parse(csv.Read(i, j + 1)); // [1/deg]
                            isSetted[35] = true;
                            Debug.Log("`Cnb` setted.");
                            break;

                        case "cnp":
                            Cnp = float.Parse(csv.Read(i, j + 1)); // [1/rad]
                            isSetted[36] = true;
                            Debug.Log("`Cnp` setted.");
                            break;

                        case "cnr":
                            Cnr = float.Parse(csv.Read(i, j + 1)); // [1/rad]
                            isSetted[37] = true;
                            Debug.Log("`Cnr` setted.");
                            break;

                        case "cndr":
                            Cndr = float.Parse(csv.Read(i, j + 1)); // [1/deg]
                            isSetted[38] = true;
                            Debug.Log("`Cndr` setted.");
                            break;

                        // 離陸 - 1
                        case "yl":
                            YL = float.Parse(csv.Read(i, j + 1)); // 機体中心から翼持ち棒までの長さ[m]
                            isSetted[39] = true;
                            Debug.Log("`YL` setted.");
                            break;

                        default:
                            break;
                    } // switch
                } // for j
            } // for i

            // 機体の重量と慣性モーメント - 6
            Debug.Log("mass: " + mass); // 重量[kg]
            Debug.Log("centerOfMass: " + centerOfMass); // 重心位置[m]
            Debug.Log("inertiaTensor: " + inertiaTensor); // 慣性モーメント[kg*m^2]
            Debug.Log("inertiaTensorRotation: " + inertiaTensorRotation); // 慣性モーメントの回転
            Debug.Log("massAircraft: " + massAircraft); // 機体のみ全重量[kg]
            Debug.Log("centerOfMassAircraft: " + centerOfMassAircraft); // 機体のみ全重心(パイロットなし,ピッチのみ)[m]

            // 巡航時 - 5
            Debug.Log("Airspeed0: " + Airspeed0);// Magnitude of ground speed [m/s]
            Debug.Log("alpha0: " + alpha0); // Angle of attack [deg]
            Debug.Log("CDp0: " + CDp0); // Parasitic drag [-]
            Debug.Log("Cmw0: " + Cmw0); // Pitching momentum [-]
            Debug.Log("CLMAX: " + CLMAX); // Maximum lift coefficient [-]

            // 主翼 - 7
            Debug.Log("Sw: " + Sw); // Wing area of wing [m^2]
            Debug.Log("bw: " + bw); // Wing span [m]
            Debug.Log("cMAC: " + cMAC); // Mean aerodynamic chord [m]
            Debug.Log("aw: " + aw); // Wing Lift Slope [1/deg]
            Debug.Log("hw: " + hw); // Length between Wing a.c. and c.g.
            Debug.Log("ew: " + ew); // Wing efficiency
            Debug.Log("AR: " + AR); // Aspect Ratio

            // 水平尾翼 - 7
            Debug.Log("Downwash: " + Downwash); // Conventional Tail: True, T-Tail: False
            Debug.Log("St: " + St); // Wing area of tail
            Debug.Log("at: " + at); // Tail Lift Slope [1/deg]
            Debug.Log("lt: " + lt); // Length between Tail a.c. and c.g.
            Debug.Log("deMAX: " + deMAX); // Maximum elevator angle
            Debug.Log("tau: " + tau); // Control surface angle of attack effectiveness [-]
            Debug.Log("VH: " + VH); // Tail Volume

            // 垂直尾翼 - 1
            Debug.Log("drMAX: " + drMAX); // Maximum rudder angle

            // 地面効果 - 1
            Debug.Log("CGEMIN: " + CGEMIN); // Minimum Ground Effect Coefficient [-]

            // 安定微係数 - 12
            Debug.Log("Cyb: " + Cyb); // [1/deg]
            Debug.Log("Cyp: " + Cyp); // [1/rad]
            Debug.Log("Cyr: " + Cyr); // [1/rad]
            Debug.Log("Cydr: " + Cydr); // [1/deg]
            Debug.Log("Clb: " + Clb); // [1/deg]
            Debug.Log("Clp: " + Clp); // [1/rad]
            Debug.Log("Clr: " + Clr); // [1/rad]
            Debug.Log("Cldr: " + Cldr); // [1/deg]
            Debug.Log("Cnb: " + Cnb); // [1/deg]
            Debug.Log("Cnp: " + Cnp); // [1/rad]
            Debug.Log("Cnr: " + Cnr); // [1/rad]
            Debug.Log("Cndr: " + Cndr); // [1/deg]

            // 離陸 - 1
            Debug.Log("YL: " + YL); // 機体中心から翼持ち棒までの長さ[m]

            for (int i = 0; i < 40; i++)
            {
                if (!isSetted[i] && i != 39) // 翼持ちのデータはオプションとする.
                {
                    throw new Exception($"Parameter at index {i} is not setted.");
                }
            }

            return true;
        } // try
        catch (Exception e)
        {
            Debug.LogWarning("CsvIO error" + e);

            return false;
        } // catch
    } // void Load()

    // ===== 機体データをCSVファイルに書き出す ===========================================================
    private static void Export()
    {
        int recordCount = 50;
        int fieldCount = 20;

        using CsvIO csv = new(recordCount, fieldCount);

        csv.Write(1, 1, "機体の重量と慣性モーメント");
        csv.Write(2, 1, "mass");
        csv.Write(2, 2, mass.ToString());
        csv.Write(2, 3, "[kg] : 重量");
        csv.Write(3, 1, "centerOfMass");
        csv.Write(3, 2, centerOfMass.x.ToString());
        csv.Write(3, 3, centerOfMass.y.ToString());
        csv.Write(3, 4, centerOfMass.z.ToString());
        csv.Write(3, 5, "[m] : 重心位置");
        csv.Write(4, 1, "inertiaTensor");
        csv.Write(4, 2, inertiaTensor.x.ToString());
        csv.Write(4, 3, inertiaTensor.y.ToString());
        csv.Write(4, 4, inertiaTensor.z.ToString());
        csv.Write(4, 5, "[kg*m^2] : 慣性モーメント");
        csv.Write(5, 1, "inertiaTensorRotation");
        csv.Write(5, 2, inertiaTensorRotation.eulerAngles.z.ToString());
        csv.Write(5, 3, "[deg] : 慣性モーメントの回転");

        csv.Write(7, 1, "巡航時");
        csv.Write(8, 1, "Airspeed0");
        csv.Write(8, 2, Airspeed0.ToString());
        csv.Write(8, 3, "[m/s] : Magnitude of ground speed");
        csv.Write(9, 1, "alpha0");
        csv.Write(9, 2, alpha0.ToString());
        csv.Write(9, 3, "[deg] : Angle of attack");
        csv.Write(10, 1, "CDp0");
        csv.Write(10, 2, CDp0.ToString());
        csv.Write(10, 3, "[-] : Parasitic drag");
        csv.Write(11, 1, "Cmw0");
        csv.Write(11, 2, Cmw0.ToString());
        csv.Write(11, 3, "[-] : Pitching momentum");
        csv.Write(12, 1, "CLMAX");
        csv.Write(12, 2, CLMAX.ToString());
        csv.Write(12, 3, "[-] : Maximum lift coefficient");

        csv.Write(14, 1, "主翼");
        csv.Write(15, 1, "Sw");
        csv.Write(15, 2, Sw.ToString());
        csv.Write(15, 3, "[m^2] : Wing area of wing");
        csv.Write(16, 1, "bw");
        csv.Write(16, 2, bw.ToString());
        csv.Write(16, 3, "[m] : Wing span");
        csv.Write(17, 1, "cMAC");
        csv.Write(17, 2, cMAC.ToString());
        csv.Write(17, 3, "[m] : Mean aerodynamic chord");
        csv.Write(18, 1, "aw");
        csv.Write(18, 2, aw.ToString());
        csv.Write(18, 3, "[1/deg] : Wing Lift Slope");
        csv.Write(19, 1, "hw");
        csv.Write(19, 2, hw.ToString());
        csv.Write(19, 3, "[m] : Length between Wing a.c. and c.g.");
        csv.Write(20, 1, "ew");
        csv.Write(20, 2, ew.ToString());
        csv.Write(20, 3, "[-] : Wing efficiency");
        csv.Write(21, 1, "AR");
        csv.Write(21, 2, AR.ToString());
        csv.Write(21, 3, "[-] : Aspect Ratio");

        csv.Write(23, 1, "水平尾翼");
        csv.Write(24, 1, "Downwash");
        csv.Write(24, 2, Downwash.ToString());
        csv.Write(24, 3, "Conventional Tail: True, T-Tail: False");
        csv.Write(25, 1, "St");
        csv.Write(25, 2, St.ToString());
        csv.Write(25, 3, "[m^2] : Wing area of tail");
        csv.Write(26, 1, "at");
        csv.Write(26, 2, at.ToString());
        csv.Write(26, 3, "[1/deg] : Tail Lift Slope");
        csv.Write(27, 1, "lt");
        csv.Write(27, 2, lt.ToString());
        csv.Write(27, 3, "[m] : Length between Tail a.c. and c.g.");
        csv.Write(28, 1, "deMAX");
        csv.Write(28, 2, deMAX.ToString());
        csv.Write(28, 3, "[deg] : Maximum elevator angle");
        csv.Write(29, 1, "tau");
        csv.Write(29, 2, tau.ToString());
        csv.Write(29, 3, "[-] : Control surface angle of attack effectiveness");
        csv.Write(30, 1, "VH");
        csv.Write(30, 2, VH.ToString());
        csv.Write(30, 3, "[-] : Tail Volume");

        csv.Write(32, 1, "垂直尾翼");
        csv.Write(33, 1, "drMAX");
        csv.Write(33, 2, drMAX.ToString());
        csv.Write(33, 3, "[deg] : Maximum rudder angle");

        csv.Write(4, 6, "地面効果");
        csv.Write(5, 6, "CGEMIN");
        csv.Write(5, 7, CGEMIN.ToString());
        csv.Write(5, 8, "[-] : Minimum Ground Effect Coefficient");

        csv.Write(7, 6, "安定微係数");
        csv.Write(8, 6, "Cyb");
        csv.Write(8, 7, Cyb.ToString());
        csv.Write(8, 8, "[1/deg]");
        csv.Write(9, 6, "Cyp");
        csv.Write(9, 7, Cyp.ToString());
        csv.Write(9, 8, "[1/rad]");
        csv.Write(10, 6, "Cyr");
        csv.Write(10, 7, Cyr.ToString());
        csv.Write(10, 8, "[1/rad]");
        csv.Write(11, 6, "Cydr");
        csv.Write(11, 7, Cydr.ToString());
        csv.Write(11, 8, "[1/deg]");
        csv.Write(12, 6, "Clb");
        csv.Write(12, 7, Clb.ToString());
        csv.Write(12, 8, "[1/deg]");
        csv.Write(13, 6, "Clp");
        csv.Write(13, 7, Clp.ToString());
        csv.Write(13, 8, "[1/rad]");
        csv.Write(14, 6, "Clr");
        csv.Write(14, 7, Clr.ToString());
        csv.Write(14, 8, "[1/rad]");
        csv.Write(15, 6, "Cldr");
        csv.Write(15, 7, Cldr.ToString());
        csv.Write(15, 8, "[1/deg]");
        csv.Write(16, 6, "Cnb");
        csv.Write(16, 7, Cnb.ToString());
        csv.Write(16, 8, "[1/deg]");
        csv.Write(17, 6, "Cnp");
        csv.Write(17, 7, Cnp.ToString());
        csv.Write(17, 8, "[1/rad]");
        csv.Write(18, 6, "Cnr");
        csv.Write(18, 7, Cnr.ToString());
        csv.Write(18, 8, "[1/rad]");
        csv.Write(19, 6, "Cndr");
        csv.Write(19, 7, Cndr.ToString());
        csv.Write(19, 8, "[1/deg]");

        csv.Write(21, 6, "離陸");
        csv.Write(22, 6, "YL");
        csv.Write(22, 7, YL.ToString());
        csv.Write(22, 8, "[m] : 機体中心から翼持ち棒までの長さ");

        csv.Flush("Assets/CSV/AircraftData_export.csv");
    } // void Export()

    // ===== 飛行機の名前に応じて機体諸元を適用する ===========================================================
    private static void InputSpecifications(string AircraftName)
    {
        if (AircraftName == "QX-18")
        {
            // Plane
            mass = 93.875f; // [kg]
            centerOfMass = new Vector3(0f, 0.221f, 0f); // [m]
            inertiaTensor = new Vector3(876f, 947f, 76f);
            inertiaTensorRotation = Quaternion.AngleAxis(-4.833f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 9.700f; // Magnitude of ground speed [m/s]
            alpha0 = 1.682f; // Angle of attack [deg]
            CDp0 = 0.018f; // Parasitic drag [-]
            Cmw0 = -0.164f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 18.042f; // Wing area of wing [m^2]
            bw = 25.133f; // Wing span [m]
            cMAC = 0.757f; // Mean aerodynamic chord [m]
            aw = 0.108f; // Wing Lift Slope [1/deg]
            hw = (0.323f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.949f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.375f; // Wing area of tail
            at = 0.076f; // Tail Lift Slope [1/deg]
            lt = 4.200f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 10.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.215f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.002410f; // [1/deg]
            Cyp = -0.228437f; // [1/rad]
            Cyr = 0.090542f; // [1/rad]
            Cydr = 0.001908f; // [1/deg]
            Clb = -0.002002f; // [1/deg]
            Clp = -0.877559f; // [1/rad]
            Clr = 0.237651f; // [1/rad]
            Cldr = 0.000052f; // [1/deg]
            Cnb = -0.000059f; // [1/deg]
            Cnp = -0.142441f; // [1/rad]
            Cnr = -0.000491f; // [1/rad]
            Cndr = -0.000262f; // [1/deg]
        }
        else if (AircraftName == "QX-19")
        {
            // Plane
            mass = 96.631f;
            centerOfMass = new Vector3(0f, 0.294f, 0f);
            inertiaTensor = new Vector3(991f, 1032f, 60f);
            inertiaTensorRotation = Quaternion.AngleAxis(-9.134f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 8.800f; // Magnitude of ground speed [m/s]
            alpha0 = 1.554f; // Angle of attack [deg]
            CDp0 = 0.019f; // Parasitic drag [-]
            Cmw0 = -0.170f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 18.275f; // Wing area of wing [m^2]
            bw = 26.418f; // Wing span [m]
            cMAC = 0.736f; // Mean aerodynamic chord [m]
            aw = 0.105f; // Wing Lift Slope [1/deg]
            hw = (0.323f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 1.010f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.548f; // Wing area of tail
            at = 0.082f; // Tail Lift Slope [1/deg]
            lt = 3.200f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 10.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.361f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.005300f; // [1/deg]
            Cyp = -0.567798f; // [1/rad]
            Cyr = 0.225280f; // [1/rad]
            Cydr = 0.001721f; // [1/deg]
            Clb = -0.005118f; // [1/deg]
            Clp = -0.827488f; // [1/rad]
            Clr = 0.296796f; // [1/rad]
            Cldr = 0.000050f; // [1/deg]
            Cnb = -0.000808f; // [1/deg]
            Cnp = -0.165533f; // [1/rad]
            Cnr = 0.001675f; // [1/rad]
            Cndr = -0.000208f; // [1/deg]
        }
        else if (AircraftName == "QX-20")
        {
            // Plane
            mass = 98.797f;
            centerOfMass = new Vector3(0f, 0.29f, 0f);
            inertiaTensor = new Vector3(1003f, 1045f, 58f);
            inertiaTensorRotation = Quaternion.AngleAxis(-9.112f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 9.600f; // Magnitude of ground speed [m/s]
            alpha0 = 1.459f; // Angle of attack [deg]
            CDp0 = 0.016f; // Parasitic drag [-]
            Cmw0 = -0.114f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 18.816f; // Wing area of wing [m^2]
            bw = 26.679f; // Wing span [m]
            cMAC = 0.755f; // Mean aerodynamic chord [m]
            aw = 0.108f; // Wing Lift Slope [1/deg]
            hw = (0.323f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = false; // Conventional Tail: True, T-Tail: False
            St = 1.526f; // Wing area of tail
            at = 0.088f; // Tail Lift Slope [1/deg]
            lt = 3.200f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 15.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.293f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003555f; // [1/deg]
            Cyp = -0.455493f; // [1/rad]
            Cyr = 0.143466f; // [1/rad]
            Cydr = 0.000888f; // [1/deg]
            Clb = -0.004049f; // [1/deg]
            Clp = -0.829690f; // [1/rad]
            Clr = 0.227736f; // [1/rad]
            Cldr = 0.000016f; // [1/deg]
            Cnb = -0.000500f; // [1/deg]
            Cnp = -0.132307f; // [1/rad]
            Cnr = 0.000942f; // [1/rad]
            Cndr = -0.000106f; // [1/deg]
        }
        else if (AircraftName == "ARG-2")
        {
            // Plane
            mass = 103.100f;
            centerOfMass = new Vector3(0f, -0.019f, 0f);
            inertiaTensor = new Vector3(961f, 1024f, 80f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-3.929f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 10.500f; // Magnitude of ground speed [m/s]
            alpha0 = 1.407f; // Angle of attack [deg]
            CDp0 = 0.014f; // Parasitic drag [-]
            Cmw0 = -0.165f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 18.009f; // Wing area of wing [m^2]
            bw = 23.350f; // Wing span [m]
            cMAC = 0.813f; // Mean aerodynamic chord [m]
            aw = 0.103f; // Wing Lift Slope [1/deg]
            hw = (0.3375f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.651f; // Wing area of tail
            at = 0.074f; // Tail Lift Slope [1/deg]
            lt = 3.200f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 15.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.215f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003764f; // [1/deg]
            Cyp = -0.411848f; // [1/rad]
            Cyr = 0.141631f; // [1/rad]
            Cydr = 0.001846f; // [1/deg]
            Clb = -0.003656f; // [1/deg]
            Clp = -0.816226f; // [1/rad]
            Clr = 0.219104f; // [1/rad]
            Cldr = 0.000032f; // [1/deg]
            Cnb = -0.000245f; // [1/deg]
            Cnp = -0.127263f; // [1/rad]
            Cnr = -0.002745f; // [1/rad]
            Cndr = -0.000308f; // [1/deg]
        }
        else if (AircraftName == "UL01B")
        {
            // Plane
            mass = 87.000f;
            centerOfMass = new Vector3(0f, 0.290f, 0f);
            inertiaTensor = new Vector3(886f, 975f, 113f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-5.581f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 8.500f; // Magnitude of ground speed [m/s]
            alpha0 = 1.521f; // Angle of attack [deg]
            CDp0 = 0.015f; // Parasitic drag [-]
            Cmw0 = -0.122f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 21.873f; // Wing area of wing [m^2]
            bw = 25.200f; // Wing span [m]
            cMAC = 0.911f; // Mean aerodynamic chord [m]
            aw = 0.103f; // Wing Lift Slope [1/deg]
            hw = (0.330f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 2.160f; // Wing area of tail
            at = 0.074f; // Tail Lift Slope [1/deg]
            lt = 4.500f; // Length between Tail a.c. and c.g.
            deMAX = 12.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 18.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.360f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.010768f; // [1/deg]
            Cyp = -0.642834f; // [1/rad]
            Cyr = 0.320123f; // [1/rad]
            Cydr = 0.003872f; // [1/deg]
            Clb = -0.006073f; // [1/deg]
            Clp = -0.776507f; // [1/rad]
            Clr = 0.249355f; // [1/rad]
            Cldr = 0.000061f; // [1/deg]
            Cnb = -0.000336f; // [1/deg]
            Cnp = -0.135587f; // [1/rad]
            Cnr = -0.016244f; // [1/rad]
            Cndr = -0.000817f; // [1/deg]
        }
        else if (AircraftName == "ORCA18")
        {
            // Plane
            mass = 96.000f;
            centerOfMass = new Vector3(0f, 0.009f, 0f);
            inertiaTensor = new Vector3(858f, 949f, 107f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-2.972f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 8.000f; // Magnitude of ground speed [m/s]
            alpha0 = 1.500f; // Angle of attack [deg]
            CDp0 = 0.015f; // Parasitic drag [-]
            Cmw0 = -0.108f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 20.257f; // Wing area of wing [m^2]
            bw = 24.100f; // Wing span [m]
            cMAC = 0.900f; // Mean aerodynamic chord [m]
            aw = 0.103f; // Wing Lift Slope [1/deg]
            hw = (0.329f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.950f; // Wing area of tail
            at = 0.077f; // Tail Lift Slope [1/deg]
            lt = 3.900f; // Length between Tail a.c. and c.g.
            deMAX = 12.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 20.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.290f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003716f; // [1/deg]
            Cyp = -0.375654f; // [1/rad]
            Cyr = 0.187645f; // [1/rad]
            Cydr = 0.001276f; // [1/deg]
            Clb = -0.003325f; // [1/deg]
            Clp = -0.792170f; // [1/rad]
            Clr = 0.302277f; // [1/rad]
            Cldr = 0.000000f; // [1/deg]
            Cnb = -0.000324f; // [1/deg]
            Cnp = -0.169856f; // [1/rad]
            Cnr = -0.003154f; // [1/rad]
            Cndr = -0.000233f; // [1/deg]
        }
        else if (AircraftName == "ORCA22")
        {
            // Plane
            mass = 95.000f;
            centerOfMass = new Vector3(0f, 0.014f, 0f);
            inertiaTensor = new Vector3(904f, 1004f, 113f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-3.015f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 8.400f; // Magnitude of ground speed [m/s]
            alpha0 = 1.395f; // Angle of attack [deg]
            CDp0 = 0.016f; // Parasitic drag [-]
            Cmw0 = -0.105f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 18.560f; // Wing area of wing [m^2]
            bw = 25.400f; // Wing span [m]
            cMAC = 0.797f; // Mean aerodynamic chord [m]
            aw = 0.104f; // Wing Lift Slope [1/deg]
            hw = (0.329f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.392f; // Wing area of tail
            at = 0.074f; // Tail Lift Slope [1/deg]
            lt = 3.900f; // Length between Tail a.c. and c.g.
            deMAX = 12.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 20.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.290f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003515f; // [1/deg]
            Cyp = -0.307586f; // [1/rad]
            Cyr = 0.155767f; // [1/rad]
            Cydr = 0.001392f; // [1/deg]
            Clb = -0.002719f; // [1/deg]
            Clp = -0.756397f; // [1/rad]
            Clr = 0.274225f; // [1/rad]
            Cldr = 0.000000f; // [1/deg]
            Cnb = -0.000148f; // [1/deg]
            Cnp = -0.155515f; // [1/rad]
            Cnr = -0.003774f; // [1/rad]
            Cndr = -0.000241f; // [1/deg]
        }
        else if (AircraftName == "Gardenia")
        {
            // Plane
            mass = 104.700f;
            centerOfMass = new Vector3(0f, 0.011f, 0f);
            inertiaTensor = new Vector3(1118f, 1161f, 63.790f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-6.083f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 10.300f; // Magnitude of ground speed [m/s]
            alpha0 = 1.378f; // Angle of attack [deg]
            CDp0 = 0.014f; // Parasitic drag [-]
            Cmw0 = -0.150f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 18.592f; // Wing area of wing [m^2]
            bw = 25.833f; // Wing span [m]
            cMAC = 0.758f; // Mean aerodynamic chord [m]
            aw = 0.104f; // Wing Lift Slope [1/deg]
            hw = (0.350f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.604f; // Wing area of tail
            at = 0.084f; // Tail Lift Slope [1/deg]
            lt = 3.030f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 10.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.210f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003350f; // [1/deg]
            Cyp = -0.323739f; // [1/rad]
            Cyr = 0.125542f; // [1/rad]
            Cydr = 0.002195f; // [1/deg]
            Clb = -0.002857f; // [1/deg]
            Clp = -0.827828f; // [1/rad]
            Clr = 0.236597f; // [1/rad]
            Cldr = 0.000042f; // [1/deg]
            Cnb = -0.000158f; // [1/deg]
            Cnp = -0.136255f; // [1/rad]
            Cnr = -0.001478f; // [1/rad]
            Cndr = -0.000306f; // [1/deg]
        }
        else if (AircraftName == "Aria")
        {
            // Plane
            mass = 122.000f;
            centerOfMass = new Vector3(0f, 0.007f, 0f);
            inertiaTensor = new Vector3(1646f, 1698f, 67f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-5.487f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 10.300f; // Magnitude of ground speed [m/s]
            alpha0 = 1.225f; // Angle of attack [deg]
            CDp0 = 0.013f; // Parasitic drag [-]
            Cmw0 = -0.133f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 20.754f; // Wing area of wing [m^2]
            bw = 29.021f; // Wing span [m]
            cMAC = 0.808f; // Mean aerodynamic chord [m]
            aw = 0.105f; // Wing Lift Slope [1/deg]
            hw = (0.350f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.832f; // Wing area of tail
            at = 0.083f; // Tail Lift Slope [1/deg]
            lt = 3.030f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 10.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.210f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003069f; // [1/deg]
            Cyp = -0.228176f; // [1/rad]
            Cyr = 0.095274f; // [1/rad]
            Cydr = 0.002427f; // [1/deg]
            Clb = -0.002005f; // [1/deg]
            Clp = -0.741574f; // [1/rad]
            Clr = 0.206900f; // [1/rad]
            Cldr = 0.000042f; // [1/deg]
            Cnb = -0.000012f; // [1/deg]
            Cnp = -0.120109f; // [1/rad]
            Cnr = -0.001328f; // [1/rad]
            Cndr = -0.000301f; // [1/deg]
        }
        else if (AircraftName == "Camellia")
        {
            // Plane
            mass = 109.800f;
            centerOfMass = new Vector3(0f, 0.001f, 0f);
            inertiaTensor = new Vector3(1486.608f, 1529.392f, 59.860f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-5.492f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 10.400f; // Magnitude of ground speed [m/s]
            alpha0 = 1.355f; // Angle of attack [deg]
            CDp0 = 0.013f; // Parasitic drag [-]
            Cmw0 = -0.128f; // Pitching momentum [-]
            CLMAX = 1.700f;
            // Wing
            Sw = 19.653f; // Wing area of wing [m^2]
            bw = 25.697f; // Wing span [m]
            cMAC = 0.795f; // Mean aerodynamic chord [m]
            aw = 0.104f; // Wing Lift Slope [1/deg]
            hw = (0.350f - 0.250f); // Length between Wing a.c. and c.g.
            ew = 0.986f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.719f; // Wing area of tail
            at = 0.083f; // Tail Lift Slope [1/deg]
            lt = 3.030f; // Length between Tail a.c. and c.g.
            deMAX = 10.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 10.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.210f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.003104f; // [1/deg]
            Cyp = -0.317546f; // [1/rad]
            Cyr = 0.112886f; // [1/rad]
            Cydr = 0.002013f; // [1/deg]
            Clb = -0.002799f; // [1/deg]
            Clp = -0.850998f; // [1/rad]
            Clr = 0.224309f; // [1/rad]
            Cldr = 0.000039f; // [1/deg]
            Cnb = -0.000132f; // [1/deg]
            Cnp = -0.129671f; // [1/rad]
            Cnr = -0.001558f; // [1/rad]
            Cndr = -0.000282f; // [1/deg]
        }
        else if (AircraftName == "Gaillardia")
        {
            // Plane
            mass = 100.000f;
            centerOfMass = new Vector3(0f, -0.203f, 0f);
            inertiaTensor = new Vector3(1396.421f, 1441.085f, 51.491f); //Ixx, Izz, Iyy
            inertiaTensorRotation = Quaternion.AngleAxis(-8.519f, Vector3.forward);
            // Specification At Cruise without Ground Effect
            Airspeed0 = 9.8f; // Magnitude of ground speed [m/s]
            alpha0 = 0f; // Angle of attack [deg]
            CDp0 = 0.014f; // Parasitic drag [-]
            Cmw0 = -0.1257f; // Pitching momentum [-]
            CLMAX = 1.600f;
            // Wing
            Sw = 20.4620f; // Wing area of wing [m^2]
            bw = 24.0898f; // Wing span [m]
            cMAC = 0.8643f; // Mean aerodynamic chord [m]
            aw = 0.1024f; // Wing Lift Slope [1/deg]
            hw = 0.08909f; // Length between Wing a.c. and c.g.
            ew = 0.97f; // Wing efficiency
            AR = (bw * bw) / Sw; // Aspect Ratio
            // Tail
            Downwash = true; // Conventional Tail: True, T-Tail: False
            St = 1.876f; // Wing area of tail
            at = 0.07203f; // Tail Lift Slope [1/deg]
            lt = 3.19f; // Length between Tail a.c. and c.g.
            deMAX = 5.000f; // Maximum elevator angle
            tau = 1.000f; // Control surface angle of attack effectiveness [-]
            VH = (St * lt) / (Sw * cMAC); // Tail Volume
            // Fin
            drMAX = 15.000f; // Maximum rudder angle
            // Ground Effect
            CGEMIN = 0.250f; // Minimum Ground Effect Coefficient [-]
            // Stability derivatives
            Cyb = -0.004278f; // [1/deg]
            Cyp = -0.591095f; // [1/rad]
            Cyr = 0.170201f; // [1/rad]
            Cydr = 0.001295f; // [1/deg]
            Clb = -0.005158f; // [1/deg]
            Clp = -1.076598f; // [1/rad]
            Clr = 0.259924f; // [1/rad]
            Cldr = 0.000013f; // [1/deg]
            Cnb = -0.000427f; // [1/deg]
            Cnp = -0.129588f; // [1/rad]
            Cnr = -0.006558f; // [1/rad]
            Cndr = -0.00020f; // [1/deg]
        }
    } // void InputSpecification(string AircraftName)
} // class AircraftData