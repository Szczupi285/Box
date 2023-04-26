using System;
using System.Reflection.Metadata.Ecma335;
using BoxLibrary;
using static BoxLibrary.Box;

Box p1 = new Box();

Box p2 = new Box(0.1, unit: UnitOfMeasure.centimeter);
Box p3 = new Box(1000, 245, 780, UnitOfMeasure.milimeter);
Box p4 = new Box(1, 4, 8, UnitOfMeasure.meter);
Box p5 = new Box(130, 83, 21, UnitOfMeasure.centimeter);
Box p6 = new Box(8, 4, 1, UnitOfMeasure.meter);

Box p7 = new Box(2, 4, 5, UnitOfMeasure.meter);
Box p8 = new Box(3, 5, 7, UnitOfMeasure.meter);
Box p9 = new Box(2, 2, 2, UnitOfMeasure.meter);



Console.WriteLine(p8 + p9);

Console.WriteLine(p7.Equals(null));
Console.WriteLine(p4.Equals(p6));
Console.WriteLine(p4 == p6);
Console.WriteLine(p4 == p5);
Console.WriteLine(p4 != p5);


Console.WriteLine(p4._A);



Console.WriteLine(p4.Volume);
Console.WriteLine(p5.Volume);
Console.WriteLine(p6.Volume);
Console.WriteLine();






List<Box> Boxes = new List<Box> { p1,p2,p3,p4,p5,p6};


foreach (Box box in Boxes)
{
    Console.WriteLine(box);


}


Comparison<Box> sort = (p1, p2) =>
{
    if (p1.Volume > p2.Volume) return 1;
    if (p1.Volume < p2.Volume) return -1;
    else
    {
        if (p1.Area > p2.Area) return 1;
        if (p1.Area < p2.Area) return -1;
        else
        {
            if (p1.A + p1.B + p1.C > p2.A + p2.B + p2.C) return 1;
            else if (p1.A + p1.B + p1.C < p2.A + p2.B + p2.C) return -1;
            else return 0;
        }
    }
};

Boxes.Sort(sort);

foreach (Box box in Boxes)
{
    Console.WriteLine(box);


}

Console.WriteLine();

foreach (double x in p2)
{
    Console.WriteLine(x);
}
Console.WriteLine();

foreach (double x in p1)
{
    Console.WriteLine(x);
}






