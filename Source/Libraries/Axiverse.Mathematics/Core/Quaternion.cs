﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Axiverse
{
    // References:
    // http://www.technologicalutopia.com/sourcecode/xnageometry/quaternion.cs.htm

    /// <summary>
    /// Represents a three dimensional rotation.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IEnumerable<float>
    {
        #region Members

        /// <summary>The X component of the quaternion.</summary>
        public float X;

        /// <summary>The Y component of the quaternion.</summary>
        public float Y;

        /// <summary>The Z component of the quaternion.</summary>
        public float Z;

        /// <summary>The W component of the quaternion.</summary>
        public float W;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the component at the given index.
        /// </summary>
        /// <param name="index">The index of the component.</param>
        /// <returns></returns>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    case 3: return W;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    case 3: W = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a quaternion with all components set to the given value.
        /// </summary>
        /// <param name="value">The value to set all components.</param>
        public Quaternion(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        /// <summary>
        /// Constructs a quaternion from a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The vector to set the components from.</param>
        public Quaternion(Vector4 value)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = value.W;
        }

        /// <summary>
        /// Constructs a quaternion from a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value">The vector to set the components from.</param>
        /// <param name="w">The value to set the W component.</param>
        public Quaternion(Vector3 value, float w)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = w;
        }

        /// <summary>
        /// Constructs a quaternion from the given values.
        /// </summary>
        /// <param name="x">The value to set the X component.</param>
        /// <param name="y">The value to set the Y component.</param>
        /// <param name="z">The value to set the Z component.</param>
        /// <param name="w">The value to set the W component.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        #endregion

        #region Instance Methods

        #region Instance Methods - Setters

        /// <summary>
        /// Sets all components of the quaternion to the specified value.
        /// </summary>
        /// <param name="value"></param>
        public void Set(float value)
        {
            X = Y = Z = W = value;
        }

        /// <summary>
        /// Sets the components of the quaternion to the specified values.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="w"></param>
        public void Set(float axis, float w)
        {
            X = Y = Z = axis;
            W = w;
        }

        /// <summary>
        /// Sets the components of the quaternion to the specified values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void Set(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        #endregion

        #region Instance Methods - Arithmatic

        /// <summary>
        /// Adds the given quaternion to this quaternion.
        /// </summary>
        /// <param name="value"></param>
        public void Add(Quaternion value)
        {
            Add(out this, ref this, ref value);
        }

        /// <summary>
        /// Substracts the given quaternion from this quaternion.
        /// </summary>
        /// <param name="value"></param>
        public void Subtract(Quaternion value)
        {
            Subtract(out this, ref this, ref value);
        }

        /// <summary>
        /// Multiplies this quaternion by the given quaternion.
        /// </summary>
        /// <param name="value"></param>
        public void Multiply(Quaternion value)
        {
            Multiply(out this, ref this, ref value);
        }

        /// <summary>
        /// Normalizes the quaternion.
        /// </summary>
        public void Normalize()
        {
            this /= Length();
        }

        #endregion

        #region Instance Methods - Mathematics

        /// <summary>
        /// Computes the conjugate of the quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion Conjugate()
        {
            return Conjugate(this);
        }

        /// <summary>
        /// Computes the normal of the quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion Normal()
        {
            return this / Length();
        }

        /// <summary>
        /// Computes the inverse of the quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion Inverse()
        {
            return Conjugate() / LengthSquared();
        }

        /// <summary>
        /// Computes the length of the quaternion.
        /// </summary>
        /// <returns></returns>
        public float Length()
        {
            return (float)Math.Sqrt(LengthSquared());
        }

        /// <summary>
        /// Computes the square of the length of the quaternion. This is cheaper than computing
        /// the length and is preferred when comparing the length of two quaternions.
        /// </summary>
        /// <returns></returns>
        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        #endregion

        #region Instance Methods - Overrides

        public override bool Equals(object obj)
        {
            var v = obj as Quaternion? ?? Quaternion.NaN;

            return (v.X == X && v.Y == Y && v.Z == Z && W == v.W);
        }

        public IEnumerator<float> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
            yield return W;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
        }

        public override string ToString()
        {
            return $"[ {X}, {Y}, {Z}, {W} ]";
        }

        public string ToString(int digits)
        {
            return $"[ {Math.Round(X, digits)}, {Math.Round(Y, digits)}, {Math.Round(Z, digits)}, {Math.Round(W, digits)} ]";
        }

        #endregion

        #endregion

        #region Static Methods

        #region Static Methods - Arithmatic

        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            Add(out var result, ref left, ref right);
            return result;
        }

        public static void Add(out Quaternion result, ref Quaternion left, ref Quaternion right)
        {
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;
            result.Z = left.Z + right.Z;
            result.W = left.W + right.W;
        }

        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            Subtract(out var result, ref left, ref right);
            return result;
        }

        public static void Subtract(out Quaternion result, ref Quaternion left, ref Quaternion right)
        {
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;
            result.Z = left.Z - right.Z;
            result.W = left.W - right.W;
        }

        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            Multiply(out var result, ref left, ref right);
            return result;
        }

        public static void Multiply(out Quaternion result, ref Quaternion left, ref Quaternion right)
        {
            result = new Quaternion(
                left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y,
                left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z,
                left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X,
                left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z);
        }

        public static Quaternion Multiply(Quaternion q, Vector3 v)
        {
            return new Quaternion(
                +q.W * v.X + q.Y * v.Z - q.Z * v.Y,
                +q.W * v.Y + q.Z * v.X - q.X * v.Z,
                +q.W * v.Z + q.X * v.Y - q.Y * v.X,
                -q.X * v.X - q.Y * v.Y - q.Z * v.Z);
        }

        public static Quaternion Multiply(Quaternion vector, float scalar)
        {
            Multiply(out var result, ref vector, ref scalar);
            return result;
        }

        public static Quaternion Multiply(float scalar, Quaternion vector)
        {
            Multiply(out var result, ref vector, ref scalar);
            return result;
        }

        public static void Multiply(out Quaternion result, ref Quaternion vector, ref float scalar)
        {
            Multiply(out result, ref scalar, ref vector);
        }

        public static void Multiply(out Quaternion result, ref float scalar, ref Quaternion vector)
        {
            result.X = vector.X * scalar;
            result.Y = vector.Y * scalar;
            result.Z = vector.Z * scalar;
            result.W = vector.W * scalar;
        }

        // not sure what this means, same as multiply except w is zero?
        public static Quaternion Multiply(Vector3 v, Quaternion q)
        {
            Multiply(out var result, ref v, ref q);
            return result;
        }

        public static void Multiply(out Quaternion r, ref Vector3 v, ref Quaternion q)
        {
            r = new Quaternion(
                +v.X * q.W + v.Y * q.Z - v.Z * q.Y,
                +v.Y * q.W + v.Z * q.X - v.X * q.Z,
                +v.Z * q.W + v.X * q.Y - v.Y * q.X,
                -v.X * q.X - v.Y * q.Y - v.Z * q.Z);
        }

        public static Quaternion Divide(Quaternion left, Quaternion right)
        {
            Divide(out var result, ref left, ref right);
            return result;
        }

        public static void Divide(out Quaternion result, ref Quaternion left, ref Quaternion right)
        {
            Quaternion inverse = right.Inverse();
            Multiply(out result, ref left, ref inverse);
        }

        public static Quaternion Divide(Quaternion left, float right)
        {
            Divide(out var result, ref left, ref right);
            return result;
        }

        public static Quaternion Divide(float left, Quaternion right)
        {
            Divide(out var result, ref left, ref right);
            return result;
        }

        public static void Divide(out Quaternion result, ref Quaternion left, ref float right)
        {
            result.X = left.X / right;
            result.Y = left.Y / right;
            result.Z = left.Z / right;
            result.W = left.W / right;
        }

        public static void Divide(out Quaternion result, ref float left, ref Quaternion right)
        {
            result.X = left / right.X;
            result.Y = left / right.Y;
            result.Z = left / right.Z;
            result.W = left / right.W;
        }

        #endregion

        #region Static Methods - Mathematics

        /// <summary>
        /// Computes the conjugate of the quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion Conjugate(Quaternion q)
        {
            return new Quaternion(-q.X, -q.Y, -q.Z, q.W);
        }

        /// <summary>
        /// Computes the conjugate of the quaternion.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="quaternion"></param>
        public static void Conjugate(out Quaternion result, ref Quaternion quaternion)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = quaternion.W;
        }

        /// <summary>
        /// Gets the angular distance between two quaternions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>The angle in radians.</returns>
        public static float Distance(Quaternion left, Quaternion right)
        {
            // https://math.stackexchange.com/questions/90081/quaternion-distance
            float inner = left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
            inner = Functions.Clamp(inner, -1, 1);
            return Functions.Acos(2 * inner * inner - 1);
        }

        /// <summary>
        /// Rotates a direction vector by the rotation represented by this quaternion.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 Transform(Vector3 direction)
        {
            var vector = new Vector3(X, Y, Z);
            var scalar = W;

            return 2.0f * Vector3.Dot(ref vector, ref direction) * vector
                + (scalar * scalar - Vector3.Dot(ref vector, ref vector)) * direction
                + 2.0f * scalar * Vector3.Cross(vector, direction);

            ////Quaternion r = new Quaternion(direction, 0);
            ////return (this * r * Conjugate()).ToVector3();

            //float x = direction.X, y = direction.Y, z = direction.Z;
            //float qx = X, qy = Y, qz = Z, qw = W;

            //// calculate quat * vector

            //var ix = qw * x + qy * z - qz * y;
            //var iy = qw * y + qz * x - qx * z;
            //var iz = qw * z + qx * y - qy * x;
            //var iw = -qx * x - qy * y - qz * z;

            //// calculate result * inverse quat

            //return new Vector3(
            //    ix * qw + iw * -qx + iy * -qz - iz * -qy,
            //    iy * qw + iw * -qy + iz * -qx - ix * -qz,
            //    iz * qw + iw * -qz + ix * -qy - iy * -qx);
        }

        /// <summary>
        /// Rotates a direction vector by the inverse rotation represented by this quaternion.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector3 InverseTransform(Vector3 direction)
        {
            return Inverse().Transform(direction);
            //Quaternion r = new Quaternion(direction, 0);
            //return (Inverse() * r * Conjugate()).ToVector3();
        }

        /// <summary>
        /// Creates a quaternion which looks in the specified direction with the given up vector.
        /// </summary>
        /// <param name="lookAt"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        public static Quaternion LookAt(Vector3 lookAt, Vector3 up)
        {
            lookAt.Normalize();

            //compute rotation axis
            Vector3 rotAxis = Vector3.Cross(Vector3.ForwardLH, lookAt).Normal();
            if (rotAxis.LengthSquared() == 0)
                rotAxis = up;

            //find the angle around rotation axis
            float dot = Vector3.ForwardLH.Dot(lookAt);
            float ang = Functions.Acos(dot);

            //convert axis angle to quaternion
            return FromAxisAngle(rotAxis, ang);
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Quaternion FromDirections(Vector3 source, Vector3 target)
        {
            float w = Functions.Sqrt(source.LengthSquared() * target.LengthSquared()) + Vector3.Dot(source, target);
            return new Quaternion(source % target, w).Normal();
        }

        /// <summary>
        /// Creates a quaternion from a rotation matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Quaternion FromMatrix(Matrix3 matrix)
        {
            Quaternion result;
            float diagonal = matrix.M11 + matrix.M22 + matrix.M33;
            if (diagonal > 0f)
            {
                float num = (float)Math.Sqrt((double)(diagonal + 1f));
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix.M23 - matrix.M32) * num;
                result.Y = (matrix.M31 - matrix.M13) * num;
                result.Z = (matrix.M12 - matrix.M21) * num;
            }
            else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                float num7 = (float)Math.Sqrt((double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                float num4 = 0.5f / num7;
                result.X = 0.5f * num7;
                result.Y = (matrix.M12 + matrix.M21) * num4;
                result.Z = (matrix.M13 + matrix.M31) * num4;
                result.W = (matrix.M23 - matrix.M32) * num4;
            }
            else if (matrix.M22 > matrix.M33)
            {
                float num6 = (float)Math.Sqrt((double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                float num3 = 0.5f / num6;
                result.X = (matrix.M21 + matrix.M12) * num3;
                result.Y = 0.5f * num6;
                result.Z = (matrix.M32 + matrix.M23) * num3;
                result.W = (matrix.M31 - matrix.M13) * num3;
            }
            else
            {
                float num5 = (float)Math.Sqrt((double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
                float num2 = 0.5f / num5;
                result.X = (matrix.M31 + matrix.M13) * num2;
                result.Y = (matrix.M32 + matrix.M23) * num2;
                result.Z = 0.5f * num5;
                result.W = (matrix.M12 - matrix.M21) * num2;
            }
            return result;
        }

        /// <summary>
        /// Creates a quaternion from an axis and rotation angle.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Quaternion FromAxisAngle(Vector3 axis, float angle)
        {
            Vector3 direction = axis.Normal();
            float half = angle * 0.5f;
            //direction = direction * angle;
            return new Quaternion(direction * Functions.Sin(half), Functions.Cos(half));
        }

        /// <summary>
        /// Constructs a quaternion from a euler rotation.
        /// </summary>
        /// <param name="vector">The euler angles in radians in the order of roll, pitch, yaw.</param>
        /// <returns></returns>
        public static Quaternion FromEuler(Vector3 vector)
        {
            return FromEuler(vector.Z, vector.Y, vector.X);
        }

        /// <summary>
        /// From Euler in degrees
        /// </summary>
        /// <param name="yaw">The yaw in radians.</param>
        /// <param name="pitch">The pitch in radians.</param>
        /// <param name="roll">The roll in radians.</param>
        /// <returns></returns>
        public static Quaternion FromEuler(float yaw, float pitch, float roll)
        {
            float yawOver2 = yaw * 0.5f;
            float cosYawOver2 = Functions.Cos(yawOver2);
            float sinYawOver2 = Functions.Sin(yawOver2);
            float pitchOver2 = pitch * 0.5f;
            float cosPitchOver2 = Functions.Cos(pitchOver2);
            float sinPitchOver2 = Functions.Sin(pitchOver2);
            float rollOver2 = roll * 0.5f;
            float cosRollOver2 = Functions.Cos(rollOver2);
            float sinRollOver2 = Functions.Sin(rollOver2);
            Quaternion result;
            result.W = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.X = sinYawOver2 * cosPitchOver2 * cosRollOver2 + cosYawOver2 * sinPitchOver2 * sinRollOver2;
            result.Y = cosYawOver2 * sinPitchOver2 * cosRollOver2 - sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.Z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;

            return result;
        }

        /// <summary>
        /// Converts an quaternion to an axis and rotation angle.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <param name="quaternion"></param>
        public static void ToAxisAngle(out Vector3 axis, out float angle, ref Quaternion quaternion)
        {
            // if w>1 acos and sqrt will produce errors, this cant happen if quaternion is normalised
            Quaternion normalized = (quaternion.W > 1) ? quaternion.Normal() : quaternion;

            angle = 2 * (float)Math.Acos(normalized.W);
            float s = Functions.Sqrt(1 - normalized.W * normalized.W); // assuming quaternion normalised then w is less than 1, so term always positive.
            if (s < 0.001)
            { // test to avoid divide by zero, s is always positive due to sqrt
              // if s close to zero then direction of axis not important
                axis.X = normalized.X; // if it is important that axis is normalised then replace with x=1; y=z=0;
                axis.Y = normalized.Y;
                axis.Z = normalized.Z;
            }
            else
            {
                axis.X = normalized.X / s; // normalise axis
                axis.Y = normalized.Y / s;
                axis.Z = normalized.Z / s;
            }
        }

        static float Dot(Quaternion a, Quaternion b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z + b.Z + a.W + b.W;
        }

        static Vector3 NormalizeAngles(Vector3 angles)
        {
            angles.X = NormalizeAngle(angles.X);
            angles.Y = NormalizeAngle(angles.Y);
            angles.Z = NormalizeAngle(angles.Z);
            return angles;
        }

        static float NormalizeAngle(float angle)
        {
            while (angle > Math.PI)
                angle -= 2 * (float)Math.PI;
            while (angle < -Math.PI)
                angle += 2 * (float)Math.PI;
            return angle;
        }

        public static Quaternion Average(IEnumerable<Quaternion> quaternions)
        {
            int count = quaternions.Count();
            float fraction = 1f / count;

            Quaternion sum = Zero;

            foreach (var q in quaternions)
            {
                if (Dot(sum, q) > 0)
                {
                    sum -= q * fraction;
                }
                else
                {
                    sum += q * fraction;
                }
            }

            return sum.Normal();

        }

        #endregion

        #region Static Methods - Operators

        /// <summary>
        /// The quaternion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Quaternion operator +(Quaternion value)
        {
            return value;
        }

        /// <summary>
        /// Adds two quaternions together.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Conjugate of the quaternion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Quaternion operator -(Quaternion value)
        {
            return value.Conjugate();
        }

        /// <summary>
        /// Substracts two quaternions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            return Subtract(left, right);
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return Multiply(left, right);
        }

        /// <summary>
        /// Scales the quaternion.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Quaternion operator *(float a, Quaternion b)
        {
            return Multiply(a, b);
        }

        /// <summary>
        /// Scales the quaternion.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion b, float a)
        {
            return Multiply(a, b);
        }

        /// <summary>
        /// Divides the quaternion by a scalar.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Quaternion operator /(Quaternion v, float s)
        {
            return Divide(v, s);
        }

        /// <summary>
        /// Divides a scalar by a quaternion.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Quaternion operator /(float s, Quaternion v)
        {
            return Divide(s, v);
        }

        /// <summary>
        /// Divides two quaternions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator /(Quaternion left, Quaternion right)
        {
            return Divide(left, right);
        }

        /// <summary>
        /// Gets the conjugate of the quaternion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Quaternion operator ~(Quaternion value)
        {
            return value.Conjugate();
        }

        /// <summary>
        /// Computes the inverse of the quaternion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Quaternion operator !(Quaternion value)
        {
            return value.Inverse();
        }

        #endregion 

        #region Static Methods - Conversion

        /// <summary>
        /// Parses a string representation of a quaternion.
        /// </summary>
        /// <param name="s">The string representation to parse.</param>
        public static Quaternion Parse(string s)
        {
            var pattern = @"^[\(\{\[\s]*([0-9\.Ee+-]+)[\s,]+([0-9\.Ee+-]+)[\s,]+([0-9\.Ee+-]+)[\s,]+([0-9\.Ee+-]+)[\)\}\]\s]*$";
            var match = Regex.Match(s, pattern, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                throw new ArgumentException($"Cannot parse {s} as quaterion.");
            }

            var value = new Quaternion();
            value.X = float.Parse(match.Groups[0].Value);
            value.Y = float.Parse(match.Groups[1].Value);
            value.Z = float.Parse(match.Groups[2].Value);
            value.W = float.Parse(match.Groups[3].Value);

            return value;
        }

        /// <summary>
        /// Gets the X, Y, and Z, components of the quaternion as a vector.
        /// </summary>
        /// <returns></returns>
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        /// <summary>
        /// Computes the euler representation of the rotation represented in the quaternion.
        /// </summary>
        /// <param name="q1"></param>
        /// <returns>The euler angles in radians in the order of roll, pitch, yaw</returns>
        public static Vector3 ToEuler(Quaternion q1)
        {
            float sqw = q1.W * q1.W;
            float sqx = q1.X * q1.X;
            float sqy = q1.Y * q1.Y;
            float sqz = q1.Z * q1.Z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q1.X * q1.W - q1.Y * q1.Z;
            Vector3 v;

            if (test > 0.4995f * unit)
            { // singularity at north pole
                v.Y = 2f * (float)Math.Atan2(q1.Y, q1.X);
                v.X = (float)Math.PI / 2;
                v.Z = 0;
                return NormalizeAngles(v);
            }
            if (test < -0.4995f * unit)
            { // singularity at south pole
                v.Y = -2f * (float)Math.Atan2(q1.Y, q1.X);
                v.X = -(float)Math.PI / 2;
                v.Z = 0;
                return NormalizeAngles(v);
            }
            Quaternion q = new Quaternion(q1.W, q1.Z, q1.X, q1.Y);
            var yaw = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (q.Z * q.Z + q.W * q.W));
            var pitch = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));
            var roll = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (q.Y * q.Y + q.Z * q.Z));
            return NormalizeAngles(new Vector3(roll, pitch, yaw));
        }

        public static void Lerp(ref Quaternion former, ref Quaternion latter, float scale, out Quaternion result)
        {
            float inverse = 1.0f - scale;

            if (Dot(former, latter) >= 0.0f)
            {
                result = (inverse * former) + (scale * latter);
            }
            else
            {
                result = (inverse * former) - (scale * latter);
            }

            result.Normalize();
        }

        public static Quaternion Lerp(Quaternion former, Quaternion latter, float scale)
        {
            Quaternion result;
            Lerp(ref former, ref latter, scale, out result);
            return result;
        }

        public static void Slerp(ref Quaternion former, ref Quaternion latter, float scale, out Quaternion result)
        {
            float opposite;
            float inverse;
            float dot = Dot(former, latter);

            if (Math.Abs(dot) > 1.0f - Functions.ZeroEpsilon)
            {
                inverse = 1.0f - scale;
                opposite = scale * Math.Sign(dot);
            }
            else
            {
                float acos = (float)Math.Acos(Math.Abs(dot));
                float invSin = (float)(1.0 / Math.Sin(acos));

                inverse = Functions.Sin((1.0f - scale) * acos) * invSin;
                opposite = Functions.Sin(scale * acos) * invSin * Math.Sign(dot);
            }

            result = (inverse * former) + (opposite * latter);
        }

        public static Quaternion Slerp(Quaternion former, Quaternion latter, float scale)
        {
            Quaternion result;
            Slerp(ref former, ref latter, scale, out result);
            return result;
        }

        #endregion

        #endregion

        /// <summary>The zero quaternion.</summary>
        public static readonly Quaternion Zero = new Quaternion();
        /// <summary>The one quaternion.</summary>
        public static readonly Quaternion One = new Quaternion(1);
        /// <summary>The identity quaternion.</summary>
        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        /// <summary>The not-a-number quaternion.</summary>
        public static readonly Quaternion NaN = new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);
        /// <summary>The negative infinity quaternion.</summary>
        public static readonly Quaternion NegativeInfinity = new Quaternion(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        /// <summary>The positive infinity quaternion.</summary>
        public static readonly Quaternion PositiveInfinity = new Quaternion(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
    }
}