using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Box
{


    namespace BoxLibrary
    {

        public sealed class Box : IEquatable<Box>, IEnumerable<double>
        {

            public enum UnitOfMeasure
            {
                milimeter = 1,
                centimeter = 10,
                meter = 1000,
            }

            public readonly double _A = 10;
            public readonly double _B = 10;
            public readonly double _C = 10;
            public readonly double _Objetosc = 0;
            public readonly double _Pole = 0;


            public double Volume
            {
                get
                {

                    double tempVolume = A * B * C;

                    return Math.Round(tempVolume, 9);

                }

            }
            public double Area
            {
                get
                {
                    // area value is converted to meters
                    double tempArea = (2 * A * B) + (2 * B * C) + (2 * A * C);

                    return Math.Round(tempArea, 6);
                }
            }
            public double A
            {
                get
                {
                    if (_unitOfMeasure == UnitOfMeasure.milimeter)
                        return _A / 1000.0;
                    else if (_unitOfMeasure == UnitOfMeasure.centimeter)
                        return _A * 10.0 / 1000.0;

                    else
                        return _A;
                }

            }
            public double B
            {
                get
                {
                    if (_unitOfMeasure == UnitOfMeasure.milimeter)
                        return _B / 1000.0;
                    else if (_unitOfMeasure == UnitOfMeasure.centimeter)
                        return _B * 10.0 / 1000.0;

                    else
                        return _B;
                }

            }

            public double C

            {
                get
                {
                    if (_unitOfMeasure == UnitOfMeasure.milimeter)
                        return _C / 1000.0;
                    else if (_unitOfMeasure == UnitOfMeasure.centimeter)
                        return _C * 10.0 / 1000.0;

                    else
                        return _C;
                }

            }

            public UnitOfMeasure _unitOfMeasure;


            public Box()
            {
                _A = 10;
                _B = 10;
                _C = 10;
                _unitOfMeasure = UnitOfMeasure.centimeter;
            }
            // constructor default values are set to null only for sake of logic 
            // null values can't be passed on later because of the logic on body of the constructor

            public Box(double? a = default, double? b = default, double? c = default, UnitOfMeasure unit = UnitOfMeasure.meter)
            {

                double?[] values = { a, b, c };
                double[] temp = { 0, 0, 0 };

                // for every double? argument it checks if the value is set, if not it assings the value of 10 cm in any given unit of measure to temp variable.
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == default)
                    {

                        if (unit == UnitOfMeasure.milimeter)
                        {
                            temp[i] = 100;

                        }
                        else if (unit == UnitOfMeasure.meter)
                        {
                            temp[i] = 0.10;

                        }
                        else if (unit == UnitOfMeasure.centimeter)
                        {
                            temp[i] = 10;

                        }
                    }
                }



                // if the value was default it assings the value of 10 cm in any given unit of measure to the properties. if it wasn't default value it assigns user input.
                _A = (temp[0] != 0) ? (double)temp[0] : (double)values[0]!;
                _B = (temp[1] != 0) ? (double)temp[1] : (double)values[1]!;
                _C = (temp[2] != 0) ? (double)temp[2] : (double)values[2]!;


                _unitOfMeasure = unit;

                // exception handler
                if (a <= 0 || b <= 0 || c <= 0)
                    throw new ArgumentOutOfRangeException();
                else if (_unitOfMeasure == UnitOfMeasure.meter && (a > 10 || b > 10 || c > 10))
                    throw new ArgumentOutOfRangeException();

                else if (_unitOfMeasure == UnitOfMeasure.centimeter && (a > 1000 || b > 1000 || c > 1000))
                    throw new ArgumentOutOfRangeException();

                else if (_unitOfMeasure == UnitOfMeasure.milimeter && (a > 10000 || b > 10000 || c > 10000))
                    throw new ArgumentOutOfRangeException();
                else if (unit == UnitOfMeasure.centimeter && (_A < 0.1 || _B < 0.1 || _C < 0.1))
                    throw new ArgumentOutOfRangeException("");
                else if (unit == UnitOfMeasure.milimeter && (_A <= 0.1 || _B <= 0.1 || _C <= 0.1))
                    throw new ArgumentOutOfRangeException("");
            }


            public override string ToString()
            {

                return String.Format("{0:0.000}", A) + " m" + " × " + String.Format("{0:0.000}", B) + " m" + " × " + String.Format("{0:0.000}", C) + " m";
            }
            public string ToString(string format)
            {
                if (format == "m")
                    return String.Format("{0:0.000}", A) + " m" + " × " + String.Format("{0:0.000}", B) + " m" + " × " + String.Format("{0:0.000}", C) + " m";
                else if (format == "cm")
                {
                    double tempA = A * 100;
                    double tempB = B * 100;
                    double tempC = C * 100;
                    return String.Format("{0:0.0}", tempA) + " cm" + " × " + String.Format("{0:0.0}", tempB) + " cm" + " × " + String.Format("{0:0.0}", tempC) + " cm";
                }

                else if (format == "mm")
                {
                    double tempA = A * 1000;
                    double tempB = B * 1000;
                    double tempC = C * 1000;
                    return $"{tempA} mm × {tempB} mm × {tempC} mm";
                }
                else if (format == null)
                {
                    return String.Format("{0:0.000}", A) + " m" + " × " + String.Format("{0:0.000}", B) + " m" + " × " + String.Format("{0:0.000}", C) + " m";

                }
                else
                {
                    throw new FormatException();
                }
            }

            public bool Equals(Box? other)
            {

                if (ReferenceEquals(this, other))
                    return true;
                if (this is null || other is null)
                    return false;

                if (new[] { this.A, this.B, this.C }.OrderBy(x => x).SequenceEqual(new[] { other.A, other.B, other.C }.OrderBy(x => x)))
                    return true;
                else
                    return false;

            }
            public override bool Equals(object? obj)
            {
                if (obj is Box)
                    return Equals((Box)obj);
                else
                    return false;
            }

            public override int GetHashCode() => (_A, _B, _C, _unitOfMeasure).GetHashCode();
            public static bool operator ==(Box p1, Box p2)
            {

                if (ReferenceEquals(p1, p2))
                {
                    return true;
                }
                else if (p1 is null || p2 is null)
                {
                    return false;
                }

                return p1.Equals(p2);

            }

            public static bool operator !=(Box p1, Box p2) => !(p1 == p2);

            // finds lowest possible volume of box that can contain two other boxes taking in consideration that both of them can be rotated in every dimension, method assume that boxes walls can stack on eachother.
            public static Box operator +(Box p1, Box p2)
            {
                List<double> list1 = new List<double>() { p1.A, p1.B, p1.C };
                List<double> list2 = new List<double>() { p2.A, p2.B, p2.C };

                double higest;
                double lowest;
                double maxDiff = list2.Max() - list1.Min();

                if (list1.Max() - list2.Min() > maxDiff)
                {

                    higest = list1.Max();
                    lowest = list2.Min();

                    list1.Remove(list1.Max());
                    list2.Remove(list2.Min());
                }
                else
                {
                    higest = list2.Max();
                    lowest = list1.Min();
                    list1.Remove(list1.Min());
                    list2.Remove(list2.Max());
                }
                Box obj = new Box(higest + lowest, Math.Max(list1.First(), list2.First()), Math.Max(list1.Last(), list2.Last()), UnitOfMeasure.milimeter);
                return obj;
            }

            public static explicit operator double[](Box p)
            {
                double[] list = new double[3];

                list[0] = p.A;
                list[1] = p.B;
                list[2] = p.C;
                return list;

            }

            public static implicit operator Box(ValueTuple<int, int, int> p)
            {
                Box obj = new Box((double)p.Item1, (double)p.Item2, (double)p.Item3, UnitOfMeasure.milimeter);
                return obj;
            }

            public double this[int i]
            {
                get
                {
                    double[] tempForIter = (double[])this;
                    return tempForIter[i];
                }
            }

            public static Box Parse(string s)
            {

                string[] temp = s.Split(" ");
                if (temp[1] == "m" && temp[4] == "m" && temp[7] == "m")
                {
                    Box result = new Box(Convert.ToDouble(temp[0].Replace('.', ',')), Convert.ToDouble(temp[3].Replace('.', ',')), Convert.ToDouble(temp[6].Replace('.', ',')), UnitOfMeasure.meter);
                    return result;
                }
                else if (temp[1] == "cm" && temp[4] == "cm" && temp[7] == "cm")
                {
                    Box result = new Box(Convert.ToDouble(temp[0].Replace('.', ',')), Convert.ToDouble(temp[3].Replace('.', ',')), Convert.ToDouble(temp[6].Replace('.', ',')), UnitOfMeasure.centimeter);
                    return result;
                }
                else if (temp[1] == "mm" && temp[4] == "mm" && temp[7] == "mm")
                {
                    Box result = new Box(Convert.ToDouble(temp[0].Replace('.', ',')), Convert.ToDouble(temp[3].Replace('.', ',')), Convert.ToDouble(temp[6].Replace('.', ',')), UnitOfMeasure.milimeter);
                    return result;
                }
                else
                {
                    throw new ArgumentException("wrong length measures, you can only input 'mm', 'cm', 'm' ");
                }


            }

            public IEnumerator<double> GetEnumerator()
            {
                yield return _A;
                yield return _B;
                yield return _C;
                yield break;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }


        }


    }

}
