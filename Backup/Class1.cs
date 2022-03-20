using System;
using System.Threading;
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static Class2 b;
		public void writethread()
		{
			lock(this)
			{
				if(new Random().Next(4)==0||new Random().Next(8)==6)
				{
					Console.WriteLine("\nThe deadlock it will happen now......");
					Thread.Sleep(100);
					Console.WriteLine("\n.....DEADLOCK.....");
				}
				lock(b)
				{
					Console.WriteLine("\n"+Thread.CurrentThread.Name);
				}
			}
		}
		[STAThread]
		static void Main(string[] args)
		{
			Random r=new Random();
			Class1 c=new Class1();
			b=new Class2(c);
			for(int i=0;i<10;i++)
			{
				Thread t1=new Thread(new ThreadStart(c.writethread));
				t1.Name="THREAD.1";
				Thread t2=new Thread(new ThreadStart(b.writethread));
				t2.Name="THREAD.2";
				t1.Start();
				t2.Start();
			}			
		}
	}

class Class2
{
	Class1 c;
	public Class2(Class1 c)
	{
		this.c=c;
	}
	public void writethread()
	{
		lock(this)
		{			
			lock(c)
			{
				Console.WriteLine(Thread.CurrentThread.Name);
			}
		}
             
	}
}