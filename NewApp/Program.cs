namespace Programa{
public class Program {
    private static void Main(string[]args)
    class ToanTu{
    {
        int a,b; 
    system.Console.Write("a=");
     a= Convert.ToInt32(Console.ReadLine());
    system.Console.Write("b=");
     b= Convert.ToInt32(Console.ReadLine());
     //in tổng a, b
int tong = a + b;
        int hieu = a -b;
        int tich = a * b;

        Console.WriteLine($"Tong {a} + {b} = {tong}");
        Console.WriteLine($"Hieu {a} - {b} = {hieu}");
        Console.WriteLine($"Tich {a} * {b} = {tich}");

        if (b != 0)
        {
            float thuong = (float)a /b;
            int du =a % b;
            Console.WriteLine($"Thuong {a} / {b} = {thuong}");
            Console.WriteLine($" Phan du {a} % {b} = {du}");
        }else
        {
        Console.WriteLine("Khong thuc hien duoc phep chia va phep lay du");
    }
     


     }
     }
     }
     }
     
    

    

   
    
    



