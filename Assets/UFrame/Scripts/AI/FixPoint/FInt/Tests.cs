using System;
using System.Diagnostics;
using NUnit.Framework;

namespace FixedPoint
{
	[TestFixture] class Tests
	{
		[Test] public void CanCreateNewFixedInt()
		{
			var fInt = FInt.Create(3);
			Assert.IsNotNull(fInt);
		}

		[Test] public void Equal()
		{
			var fInt1 = FInt.Create(1);
			var fInt2 = FInt.Create(1);
			var eq = fInt1.Equals(fInt2);
			Assert.IsTrue(eq);
		}

		[Test] public void Subtraction()
		{
			var fInt1 = FInt.Create(3);
			var fInt2 = FInt.Create(2);
			var fInt3 = FInt.Create(1);

			var sub = fInt1 - fInt2;
			var eq = fInt3.Equals(sub);
			Assert.IsTrue(eq);
		}

		[Test] public void Multiplication()
		{
			var fInt1 = FInt.Create(3);
			var fInt2 = FInt.Create(2);
			var fInt3 = FInt.Create(6);

			var fInt4 = fInt1*fInt2;
			var eq = fInt4.Equals(fInt3);
			Assert.IsTrue(eq);
		}

		[Test] public void Divison()
		{
			var fInt1 = FInt.Create(4);
			var fInt2 = FInt.Create(2);

			var fInt3 = fInt1 / fInt2;
			var eq = fInt3.Equals(fInt2);
			Assert.IsTrue(eq);
		}

		[Test] public void FasterDivisionThanInteger()
		{
			/*Basically this implies not to use fixed integers!*/

			var stopWatch1 = new Stopwatch();
			var stopWatch2 = new Stopwatch();

			var fInt1 = FInt.Create(10246);
			var fInt2 = FInt.Create(11);

			stopWatch1.Start();
			var fInt3 = fInt1 / fInt2;
			stopWatch1.Start();

			stopWatch2.Start();
			var intDivider = 10246 / 11;
			stopWatch2.Stop();

			var equality = stopWatch1.ElapsedMilliseconds > stopWatch2.ElapsedMilliseconds;
			Assert.IsTrue(equality);

		}

		[Test] public void CanComputefIntPi()
		{
			var pi = FInt.PI;
			Assert.IsNotNull(pi);
		}

		[Test]public void CanComputePiFaster()
		{
			var stopWatch1 = new Stopwatch();
			var stopWatch2 = new Stopwatch();

			stopWatch1.Start();
			var fInt3 = FInt.PI;
			stopWatch1.Start();

			stopWatch2.Start();
			var pie = Math.PI;
			stopWatch2.Stop();

			var equality = stopWatch1.ElapsedMilliseconds < stopWatch2.ElapsedMilliseconds;
			Assert.IsTrue(equality);
		}

		[Test] public void CanComputeFInt180Pi()
		{
			var pi = FInt.PIOver180F;
			Assert.IsNotNull(pi);
		}

		[Test] public void CanCreateNewFixedPoint()
		{
			var fInt1 = FInt.Create(1);
			var fInt2 = FInt.Create(1);

			var point = FPoint.Create(fInt1, fInt2);
			Assert.IsNotNull(point);
		}

		[Test] public void CanSubtractPoints()
		{
			var fInt1 = FInt.Create(1);
			var fInt2 = FInt.Create(2);

			var point1 = FPoint.Create(fInt1, fInt2);
			var point2 = FPoint.Create(fInt2, fInt1);
			var point3 = FPoint.VectorSubtract(point1, point2);
			Assert.IsNotNull(point3);
		}
	}
}
