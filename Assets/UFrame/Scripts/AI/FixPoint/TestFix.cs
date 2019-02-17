using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixedPoint;
public class TestFix : MonoBehaviour {

    void Start()
    {
        FixPointTest();
        FIntTest();
        //Fix64Test();


    }
    void FixPointTest()
    {
        FixPoint fp1 = FixPoint.HalfPi / new FixPoint(3);
        FixPoint fp2 = FixPoint.RadianPerDegree * (FixPoint)30;

        FixPoint sin30 = FixPoint.Sin(fp2);
        Debug.LogError("FixPoint sin = " + sin30);
    }

    void FIntTest()
    {
        Debug.LogError("float sin = " + Mathf.Sin(30 * Mathf.Deg2Rad));


        FInt fixAngle = FInt.PIOver180F * FInt.Create(30d);
        //FInt fixAngle = FInt.Create(30d);
        Debug.LogError("Fix sin = " + FInt.Sin(fixAngle).ToDouble());

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
