namespace NewApp.Models;

public class Person
{
    public string FullName {get;set}
    public string Address{get;set}
    public int Age {get;set;}
    public void EnterData(){
    System.Cosnsole.Write("Full name=");
    FullName= Console.ReadLine();
    System.Cosnsole.Write("Full name=");
    Address= Console.ReadLine();

}
public void Display(){
    System.Cosnsole.WriteLine("{0}-{1}",FullName, Address);
}
}
