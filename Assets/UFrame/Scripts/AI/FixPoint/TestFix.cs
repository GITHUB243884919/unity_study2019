using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixedPoint;
using FixMath;
using UFrame.AI;
using UFrame.ResourceManagement;

public class TestFix : MonoBehaviour {

    void Start()
    {
        TestFCS_P();
        //TestTurn();
        //FixedPointCSTest_Matrix2();
        //FixedPointCSTest_Matrix();
        //FixedPointCSTest_Move();
        //FixPointCSTest();
        //FixPointTest();
        //FIntTest();
        //Fix64Test();



    }

    void TestFCS_P()
    {
        MoveObject obj = new MoveObject();
        obj.SetPos(F64Vec3.Zero);
        obj.SetDir(new F64Vec3(0, 0, 1));
        obj.moveData.detectionLen = new F64(5);
        obj.moveData.detectionWidth = new F64(0.8);

        GameObjectGetter tankGetter = ResHelper.LoadGameObject("prefabs/tank");
        GameObject tankGo = tankGetter.Get();
        tankGo.transform.position = obj.GetPos().ToUnityVector3();
        tankGo.transform.forward = obj.GetDir().ToUnityVector3();
        GameObject.Instantiate<GameObject>(tankGo);

        for (int i = 0; i < 3; i++)
        {
            F64Vec3 newDir = F64Vec3.RotateY(obj.GetDir(), new F64(30));
            obj.SetDir(F64Vec3.Normalize(newDir));
            obj.SetPos(obj.GetPos() + (newDir * new F64(5)));
            tankGo.transform.position = obj.GetPos().ToUnityVector3();
            tankGo.transform.forward = obj.GetDir().ToUnityVector3();
            GameObject.Instantiate<GameObject>(tankGo);
        }
        Debug.LogError("Tank " + obj.GetPos() + " " + obj.GetPos().ToUnityVector3());

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        F64 radius = F64.Half;
        //F64 foffsetX = F64.One;
        F64 foffsetX = F64.Zero;
        //F64 foffsetZ = obj.moveData.detectionWidth + radius + new F64(-0.1);
        F64 foffsetZ = F64.FromFloat(1.4f);
        F64Vec3 fcubePos = new F64Vec3(obj.GetPos().X + foffsetX, obj.GetPos().Y, obj.GetPos().Z + foffsetZ);
        sphere.transform.position = fcubePos.ToUnityVector3();
        F64Vec3 flocalCube = F64Vec3.PointToLocalSpace2D(fcubePos,
            obj.forward, obj.left, obj.GetPos());

        Debug.LogError("Sphere pos in tank's local space " + flocalCube + " " + flocalCube.ToUnityVector3());

    }

    void TestTurn()
    {
        Debug.LogError(new F64(1.3d) + new F64(0.1d));
        Debug.LogError(new F64(0d));

        MoveObject obj = new MoveObject();
        obj.SetPos(F64Vec3.Zero);
        obj.SetDir(new F64Vec3(0, 0, 1));
        obj.moveData.detectionLen = new F64(5);
        obj.moveData.detectionWidth = new F64(0.8);

        GameObjectGetter tankGetter = ResHelper.LoadGameObject("prefabs/tank");
        GameObject tankGo = tankGetter.Get();
        tankGo.transform.position = obj.GetPos().ToUnityVector3();
        tankGo.transform.forward = obj.GetDir().ToUnityVector3();
        GameObject.Instantiate<GameObject>(tankGo);

        for(int i = 0; i < 3; i++)
        {
            F64Vec3 newDir = F64Vec3.RotateY(obj.GetDir(), new F64(30));
            obj.SetDir(F64Vec3.Normalize(newDir));
            obj.SetPos(obj.GetPos() + (newDir * new F64(5)));
            tankGo.transform.position = obj.GetPos().ToUnityVector3();
            tankGo.transform.forward = obj.GetDir().ToUnityVector3();
            GameObject.Instantiate<GameObject>(tankGo);
        }

        Vector3 av = new Vector3(100, 0, 20);
        F64Vec3 localAv = F64Vec3.PointToLocalSpace2D(F64Vec3.FromUnityVector3(av),
            obj.forward, obj.left, obj.GetPos());
        Debug.LogError("world obj=" + obj.GetPos().ToUnityVector3() + "world av=" + av + "loc av " + localAv.ToUnityVector3());

        F64Matrix3x3 m = new F64Matrix3x3();
        //F64Vec2 localAv2 = localAv.ToF64Vec2();
        //F64Vec2 forward2 = obj.forward.ToF64Vec2();
        //F64Vec2 left2 = obj.left.ToF64Vec2();
        F64Vec2 localAv2 = F64Vec2.FromFloat(88.2f, 13.2f);
        F64Vec2 forward2 = F64Vec2.FromFloat(1f, 0f);
        F64Vec2 left2 = F64Vec2.FromFloat(0f, 1f);
        F64Vec2 worldAv2 = m.VectorToWorldSpace(localAv2, forward2, left2);
        F64Vec3 worldAv3 = F64Vec3.FromF64Vec2(worldAv2);
        Debug.LogError("m convt to world " + worldAv3.ToUnityVector3() + " " + worldAv3);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        F64 radius = F64.Half;
        F64 foffsetX = F64.One;
        F64 foffsetZ = obj.moveData.detectionWidth + radius + new F64(-0.1);
        F64Vec3 fcubePos = new F64Vec3(obj.GetPos().X + foffsetX, obj.GetPos().Y, obj.GetPos().Z + foffsetZ);
        cube.transform.position = fcubePos.ToUnityVector3();
        F64Vec3 flocalCube = F64Vec3.PointToLocalSpace2D(fcubePos,
            obj.forward, obj.left, obj.GetPos());
        Debug.LogError("world obj=" + obj.GetPos().ToUnityVector3() + "world cube=" + fcubePos + "loc cubePos " + flocalCube.ToUnityVector3());
        Debug.LogError("world obj=" + obj.GetPos().ToUnityVector3() + "world cube=" + fcubePos + "loc cubePos " + flocalCube);
        fff(obj, fcubePos, F64.Half);

        //Mathf.Approximately()

    }

    void fff(MoveObject obj, F64Vec3 pos, F64 radius)
    {
        F64 sqrDis = F64Vec3.LengthSqr(obj.GetPos() - pos);
        F64 sqrDis2 = obj.moveData.detectionLen + radius;
        sqrDis2 *= sqrDis2;
        if (sqrDis <= (sqrDis2))
        {
            //在后面
            var local = F64Vec3.PointToLocalSpace2D(pos, obj.forward, obj.left, obj.GetPos());
            Debug.LogError("范围内 WORLD " + pos.ToUnityVector3() + "LOCAL " + local.ToUnityVector3());
            Debug.LogError(local);
            if (local.X >= F64.Zero)
            {
                Debug.LogError("在前面 WORLD " + pos + "LOCAL " + local);
                F64 absZ = F64.Abs(local.Z);
                Debug.LogError("碰到q abs=" + absZ + " " + (radius + obj.moveData.detectionWidth) +
    " radius=" + radius + " w=" + obj.moveData.detectionWidth);
                if (absZ <= (radius + obj.moveData.detectionWidth))
                {
                    Debug.LogError("碰到 abs=" + absZ + " " + (radius + obj.moveData.detectionWidth) +
                        " radius=" + radius + " w=" + obj.moveData.detectionWidth);
                }
            }
        }
    }
    void FixedPointCSTest_Matrix()
    {
        //MoveObject obj = new MoveObject();
        F64Vec2 pos = F64Vec2.Zero;
        F64Vec2 heading = F64Vec2.Right; //1,0
        F64Vec2 side = F64Vec2.Up; //0,1

        F64Matrix3x3 m = new F64Matrix3x3();
        F64Vec2 point = F64Vec2.FromFloat(2, 3);
        F64Vec2 point2 = m.PointToLocalSpace(point,
                                heading,
                                side,
                                pos);
        Debug.LogError(point2.X.Float + " " +  point2.Y.Float);

        F64Vec3 forward = new F64Vec3(1, 0, 0);
        F64Vec3 right = F64Vec3.RotateY(forward, new FixMath.F64(90));
        Debug.LogError(forward.ToUnityVector3() + " " + F64Vec3.Normalize(right).ToUnityVector3());
        Debug.LogError(F64Vec3.Normalize(F64Vec3.Cross(forward, right)).ToUnityVector3());

        MoveObject obj = new MoveObject();
        obj.SetPos(F64Vec3.Zero);
        obj.SetDir(new F64Vec3(1, 0, 0));
        var f = F64Vec3.RotateY(obj.forward, new FixMath.F64(90));
        Debug.LogError(obj.forward + " " + F64Vec3.Normalize(f).ToUnityVector3());

        F64Vec3 check = F64Vec3.FromFloat(2, 0, 3);
        //var r = obj.PointToLocalSpace2D(check);
        F64Vec2 check2 = m.PointToLocalSpace(new F64Vec2(check.X, check.Z) ,
                        new F64Vec2(obj.forward.X, obj.forward.Z),
                        new F64Vec2(f.X, f.Z),
                        new F64Vec2(obj.GetPos().X, obj.GetPos().Y) );
        Debug.LogError(check2.X.Float + " " + check2.Y.Float);

        //Debug.LogError(check2.X.Float + " " + check2.Y.Float);

    }

    void FixedPointCSTest_Matrix2()
    {
        //MoveObject obj = new MoveObject();
        //F64Vec2 pos = F64Vec2.Zero;
        //F64Vec2 heading = F64Vec2.Right; //1,0
        //F64Vec2 side = F64Vec2.Up; //0,1

        F64Matrix3x3 m = new F64Matrix3x3();
        //F64Vec2 point = F64Vec2.FromFloat(2, 3);
        //F64Vec2 point2 = m.PointToLocalSpace(point,
        //                        heading,
        //                        side,
        //                        pos);
        //Debug.LogError(point2.X.Float + " " + point2.Y.Float);

        MoveObject obj = new MoveObject();
        obj.SetPos(F64Vec3.Zero);
        obj.SetDir(new F64Vec3(1, 0, 0));
        //var f = F64Vec3.RotateY(obj.forward, new FixMath.F64(-90));
        //Debug.LogError(obj.forward + " " + F64Vec3.Normalize(f).ToUnityVector3());

        F64Vec3 check = new F64Vec3(2, 0, 3);
        var r3 = F64Vec3.PointToLocalSpace2D(check, obj.forward, obj.left, obj.GetPos());

        //Debug.LogError(check2.X.Float + " " + check2.Y.Float);
        Debug.LogError(r3);

    }

    void FixedPointCSTest_Move()
    {

        F64Vec3 TTT = F64Vec3.RotateY(new F64Vec3(0, 0, 1), new F64(-90));
        Debug.LogError("TTT " + TTT.ToUnityVector3());

        float speed = 10;
        float turnSpeed = 90;
        int deltaTimeMS = 1000;

        Vector3 oldPos = new Vector3(0, 0, 0);
        Vector3 dir = new Vector3(0, 0, 1);
        float delta = speed * deltaTimeMS / 1000;
        Vector3 newPos = oldPos + delta * dir;
        //moveObject.SetPos(moveObject.GetPos() + (float)delta * moveObject.GetDir());
        Debug.LogError("UNITY NEWPOS " + newPos);
        
        float angle = turnSpeed * deltaTimeMS / 1000;
        Vector3 newDir = Quaternion.AngleAxis(angle, Vector3.up) * dir;
        newDir.Normalize();
        Debug.LogError("UNITY NEWDIR " + newDir);

        F64 fspeed = new F64(speed);
        F64 fdeltaTime = new F64(deltaTimeMS);
        F64 f1000 = F64.F1000;
        F64 fdelta = fspeed * fdeltaTime / f1000;
        F64Vec3 fOldPos = F64Vec3.FromUnityVector3(oldPos);
        F64Vec3 fDir = F64Vec3.FromUnityVector3(dir);
        F64Vec3 fNewPos = fOldPos + fdelta * fDir;
        Debug.LogError("FIXED NEW POS " + fNewPos.ToUnityVector3());
        F64 fTurnSpeed = new F64(turnSpeed);
        F64 fAngle = fTurnSpeed * fdeltaTime / f1000;
        F64Quat rot = F64Quat.FromAxisAngle(F64Vec3.Up, new F64(0));
        F64Quat rotNor = F64Quat.Normalize(rot);
        F64Vec3 newFdir = F64Quat.RotateVector(rotNor, new F64Vec3(0, 0, 1));
        F64Vec3 newNfdir = F64Vec3.Normalize(newFdir);
        Debug.LogError("FIXED NEWDIR " + newNfdir.ToUnityVector3());

    }
    void FixPointCSTest()
    {
        FixMath.F64 angle  = new FixMath.F64(30);
        FixMath.F64 angle2 = new FixMath.F64(180);
        
        FixMath.F64 angle3 = angle * FixMath.F64.Pi / angle2;

        //public static F64 DegToRad(F64 a) { return FromRaw(Fixed64.Mul(a.Raw, 74961320)); }     // F64.Pi / 180
        FixMath.F64 angle4 = FixMath.F64.DegToRad(angle);
        FixMath.F64 angle5 = FixMath.F64.DegToRad2(angle);
        Debug.LogError(FixMath.F64.Sin(angle3));
        Debug.LogError(FixMath.F64.Sin(angle4));
        Debug.LogError(FixMath.F64.Sin(angle5));


        //
        F64Vec3 FA = new F64Vec3(0f, 0f, 1f);
        F64Vec3 FB = new F64Vec3(1f, 0f, 0f);
        F64 Fangle = F64Vec3.Angle(FA, FB);
        Debug.LogError(Fangle.Float);
        F64Vec3 cross = F64Vec3.Cross(FA, FB);
        Debug.LogError(cross.ToUnityVector3());


        Vector3 A = new Vector3(0f, 0f, 1f);
        Vector3 B = new Vector3(1f, 0f, 0f);
        float angle6 = Vector3.Angle(A, B);
        Debug.LogError(angle6);

    }

    void FixPointTest()
    {
        FixPoint fp1 = FixPoint.HalfPi / new FixPoint(3);
        FixPoint fp2 = FixPoint.RadianPerDegree * (FixPoint)30;
        FixPoint fp3 =  (FixPoint)30 * FixPoint.Pi / (FixPoint)180;
        FixPoint sin30 = FixPoint.Sin(fp3);
        Debug.LogError("FixPoint sin = " + sin30);
    }

    void FIntTest()
    {
        Debug.LogError("float sin = " + Mathf.Sin(30 * Mathf.Deg2Rad));


        FInt fixAngle = FInt.PIOver180F * FInt.Create(30d);

        FInt fixAngle2 = FInt.PI * FInt.Create(30d) / FInt.Create(180);
        //FInt fixAngle = FInt.Create(30d);
        Debug.LogError("Fix sin = " + FInt.Sin(fixAngle2).ToDouble());

    }
	void Fix64Test()
    {
        //Debug.LogError(fix1.ToString());
        FixVector3 FA = new FixVector3(0f, 0f, 1f);
        FixVector3 FB = new FixVector3(1f, 0f, 0f);
        Fix64 Fangle =  FixVector3.Angle(FA, FB);
        Debug.LogError(Fangle);
        Debug.LogError(FixVector3.Cross(FA, FB));
        Debug.LogError(FixVector3.Rotate(FA, (Fix64)90));

        Fix64 angleToH = (Fix64)2 * (Fix64)3.1415927 / (Fix64)360;
        Fix64 FangleSin = (Fix64)30 * angleToH;
        Debug.LogError("Fix64 sin = " + Fix64.Sin(FangleSin));
        Debug.LogError("float sin = " + Mathf.Sin(30 * Mathf.Deg2Rad));

        Vector3 A = new Vector3(0f, 0f, 1f);
        Vector3 B = new Vector3(1f, 0f, 0f);
        float angle = Vector3.Angle(A, B);
        Debug.LogError(angle);
        Debug.LogError(Vector3.Cross(A, B));

        float dot = Vector3.Dot(A, B);
        Debug.LogError(Mathf.Acos(dot) + " " + Mathf.Acos(dot) * Mathf.Rad2Deg);

        Debug.LogError(Quaternion.AngleAxis(90, Vector3.up) * A);



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
